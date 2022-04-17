<template>
     <div v-if="formData.drugInfoResponse.isSuccess==true">
        {{drugName}}
    
           
    <div class="accordion">
    <div v-if="formData.drugInfoResponse.data?.isFavorited">
        <input type="checkbox" id="isFavorited" class="checkbox">
        <label for="isFavorited" class="label">Favorite Information</label>
        <div class="information">
            Reported price: {{formData.drugInfoResponse.data.favoriteDrug?.lowestprice}}
            Purchase Location: {{formData.drugInfoResponse.data.favoriteDrug?.lowestPriceLocation}}
        </div>
    </div>
   <div v-if="formData.drugInfoResponse.data?.boxed_warning?.[0]">
    <input type="checkbox" id="boxed_warning" class="checkbox">
    <label for="boxed_warning" class="label">boxed warnings</label>
    <div class="information">
        {{formData.drugInfoResponse.data?.boxed_warning?.[0]}}
    </div>
  </div>
     <div v-if="formData.drugInfoResponse.data?.pregnancy?.[0]">
    <input type="checkbox" id="pregnancy" class="checkbox">
    <label for="pregnancy" class="label">pregnancy</label>
    <div class="information">
        {{formData.drugInfoResponse.data?.pregnancy?.[0]}}
    </div>
  </div>
    <div v-if="formData.drugInfoResponse.data?.indications_and_usage?.[0]">
    <input type="checkbox" id="indications_and_usage" class="checkbox">
    <label for="indications_and_usage" class="label">indications and usage</label>
    <div class="information">
        {{formData.drugInfoResponse.data?.indications_and_usage?.[0]}}
    </div>
  </div>
    <div v-if="formData.drugInfoResponse.data?.inactive_ingredient?.[0]">
    <input type="checkbox" id="inactive_ingredient" class="checkbox">
    <label for="inactive_ingredient" class="label">inactive ingredient</label>
    <div class="information">
        {{formData.drugInfoResponse.data?.inactive_ingredient?.[0]}}
    </div>
  </div>
      <div v-if="formData.drugInfoResponse.data?.information_for_patients?.[0]">
    <input type="checkbox" id="information_for_patients?" class="checkbox">
    <label for="information_for_patients?" class="label">information for patients</label>
    <div class="information">
        {{formData.drugInfoResponse.data?.information_for_patients?.[0]}}
    </div>
  </div>

    </div>
    <div id="addfavorite" v-if="!formData.drugInfoResponse.data?.isFavorited">
        <button @click = "addFavorite">Add to Favorite</button> {{this.message}} {{this.formData.drugInfoResponse.data.isFavorited}}
    </div>
  </div>
  <div v-else>
      {{formData.drugInfoResponse.error}}
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
                drugInfoResponse: [],
                isHidden: false
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
            fetch(process.env.VUE_APP_BACKEND+'Medication/viewDrug?generic_name='+this.drugName,requestOptions)
                .then(response => response.json())
                .then(body => this.formData.drugInfoResponse= body)
        },
        addFavorite(){
        const requestOptions = {
            method: "post",
            credentials: 'include',
            headers: { "Content-Type": "application/json"},
            

        };
        console.log(this.formData.drugInfoResponse.data.isFavorited)
        if(this.formData.drugInfoResponse.data.isFavorited==false){
        
        fetch(process.env.VUE_APP_BACKEND+'Medication/FavoriteAdd?genericName='+
        this.formData.drugInfoResponse.data.openfda?.generic_name?.[0]
        + '&brandName=' + this.formData.drugInfoResponse.data.openfda?.brand_name?.[0]+'&productID='+
        this.formData.drugInfoResponse.data.openfda?.product_ndc?.[0],requestOptions)
            .then(response => response.text())
            .then(body => this.message = body)
        this.formData.drugInfoResponse.data.isFavorited=true
        }

        else{
            this.message="already favorited"
        }
    }
}
    }
</script>
<style scoped>

.accordion{
    max-width:100%;
    background-color: #1768AC
    
;
}

.label{
    color: white;
    cursor: pointer;
}
.label:hover{
      background-color: darkblue;

}

.information{
    background: white;
    line-height: 1.6;
    height: 0px;
    opacity: 0;
    font-size: 0.85em;
}
.checkbox{
    display: none;
}
.checkbox:checked ~ .information{
    height: 100%;
    opacity: 1;
    background: white;
}



</style>
