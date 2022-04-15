<template>
    <div id="drugName">
        {{drugName}}
    </div>
           
    <div class="accordion">
    <div v-if="formData.drugInfoResponse.data.isFavorited">
        <input type="checkbox" id="isFavorited" class="accordion__input">
        <label for="isFavorited" class="accordion__label">Favorite Information</label>
        <div class="accordion__content">
            Reported price: {{formData.drugInfoResponse.data.favoriteDrug.lowestprice}}
            Purchase Location: {{formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation}}
            
            <button @click = "RemoveFavorite(formData.drugInfoResponse.data.openfda)">Add to Favorite</button>
        </div>
    </div>
   <div v-if="formData.drugInfoResponse.data.boxed_warning?.[0]">
    <input type="checkbox" id="boxed_warning" class="accordion__input">
    <label for="boxed_warning" class="accordion__label">boxed warnings</label>
    <div class="accordion__content">
        {{formData.drugInfoResponse.data.boxed_warning?.[0]}}
    </div>
  </div>
     <div v-if="formData.drugInfoResponse.data.pregnancy?.[0]">
    <input type="checkbox" id="pregnancy" class="accordion__input">
    <label for="pregnancy" class="accordion__label">pregnancy</label>
    <div class="accordion__content">
        {{formData.drugInfoResponse.data.pregnancy?.[0]}}
    </div>
  </div>
    <div v-if="formData.drugInfoResponse.data.indications_and_usage?.[0]">
    <input type="checkbox" id="indications_and_usage" class="accordion__input">
    <label for="indications_and_usage" class="accordion__label">indications and usage</label>
    <div class="accordion__content">
        {{formData.drugInfoResponse.data.indications_and_usage?.[0]}}
    </div>
  </div>
    <div v-if="formData.drugInfoResponse.data.inactive_ingredient?.[0]">
    <input type="checkbox" id="inactive_ingredient" class="accordion__input">
    <label for="inactive_ingredient" class="accordion__label">inactive ingredient</label>
    <div class="accordion__content">
        {{formData.drugInfoResponse.data.inactive_ingredient?.[0]}}
    </div>
  </div>
      <div v-if="formData.drugInfoResponse.data.information_for_patients?.[0]">
    <input type="checkbox" id="information_for_patients?" class="accordion__input">
    <label for="information_for_patients?" class="accordion__label">information for patients</label>
    <div class="accordion__content">
        {{formData.drugInfoResponse.data.information_for_patients?.[0]}}
    </div>
  </div>

    </div>
    <div id="addfavorite" v-if="!formData.drugInfoResponse.data.isFavorited">
        <div id="dada">
        <button @click = "addFavorite">Add to Favorite</button>
        </div>
    </div>
  


</template>
<script>

    export default {
        name: 'ViewDrug',
        created(){
            this.ViewDrug()
        },
        data() {
        return {
            drugName:this.$route.params.id,
            formData :{
                drugInfo: [],
                drugInfoResponse: []
            },
            
             message : '',
        }
    },
    methods:{
        accordian(){
            console.log('hello')
            this.classList.toggle("active");
            var panel = this.nextElementSibling;
            if (panel.style.maxHeight) {
            panel.style.maxHeight = null;
            } else {
            panel.style.maxHeight = panel.scrollHeight + "px";
            }
            
    },
        ViewDrug(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({generic_name : this.drugName })

            };
            fetch('https://localhost:7011/Medication/viewDrug?generic_name='+this.drugName,requestOptions)
                .then(response => response.text())
                .then(body => this.formData.drugInfoResponse = JSON.parse(body))
        },
        addFavorite(){
        const requestOptions = {
            method: "post",
            credentials: 'include',
            headers: { "Content-Type": "application/json"},
            /**body: JSON.stringify({generic_name : this.formData.drugInfo.openfda?.generic_name?.[0], 
            brand_name: this.formData.drugInfo.openfda?.brand_name?.[0], productID: this.formData.drugInfo.openfda?.product_ndc?.[0] })**/

        };
        fetch('https://localhost:7011/Medication/FavoriteAdd?genericName='+this.formData.drugInfoResponse.data.openfda?.generic_name?.[0]
        + '&brandName=' + this.formData.drugInfoResponse.data.openfda?.brand_name?.[0]+'&productID='+
        this.formData.drugInfoResponse.data.openfda?.product_ndc?.[0],requestOptions)
            .then(response => response.text())
            .then(body => this.message = body)
    },
    RemoveFavorite(product_id){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
               
            };
            const response= fetch('https://localhost:7011/Medication/DeleteFavorite?product_id='
            +product_id,requestOptions)                
                .then(response =>  response.text())
                .then(body => this.message = body)
                .then(body=>this.formData.drugInfoResponse = JSON.parse(body))
                .catch((error) =>{
                    this.message="Error retrieving favorite list"
                });
    }
}
    }
</script>
<style scoped>
.form {
        width: 100%;
        margin: 0 auto
    }
.accordion{
    max-width: 90%;
    overflow: hidden;
    background-color: #1768AC
;
}

.accordion__label{
    padding: 3%;
    display: block;
    color: white;
    font-weight: bold;
    cursor: pointer;
    position: relative;
    transition: background-color 0.1s;
}
.accordion__content{
    background: white;
    line-height: 1.6;
    height: 0px;
    opacity: 0;
    font-size: 0.85em;
}
.accordion__input{
    display: none;
}
.accordion__input:checked ~ .accordion__content{
    height: 100%;
    opacity: 1;
    background: white;
}
.drugName{
    margin: 0 auto;

}


</style>s
