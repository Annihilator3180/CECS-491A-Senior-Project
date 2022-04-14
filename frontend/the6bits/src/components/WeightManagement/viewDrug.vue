<template>
    <div class="form">
        <div v-if="formData.drugInfoResponse.isSuccess==false">
            {{formData.drugInfoResponse.error}}
        </div>
         <div v-else>
             {{formData.drugInfoResponse.data.boxed_warning?.[0]}}
             {{formData.drugInfoResponse.data.pregnancy?.[0]}}
             {{formData.drugInfoResponse.data.indications_and_usage?.[0]}}
             {{formData.drugInfoResponse.data.warnings_and_cautions?.[0]}}
             {{formData.drugInfoResponse.data.inactive_ingredient?.[0]}}
             {{formData.drugInfoResponse.data.information_for_patients?.[0]}}
             {{formData.drugInfoResponse.data.isFavorited}}
             {{formData.drugInfoResponse.data.openfda?.generic_name?.[0]}}
             {{formData.drugInfoResponse.data.openfda?.brand_name?.[0]}}
            
             <div v-if="formData.drugInfoResponse.data.isFavorited==false">
                <button @click = "addFavorite">Add to Favorite</button>
                {{message}}
             </div>
             <div v-else>
                    {{formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation}}
                    {{formData.drugInfoResponse.data.favoriteDrug.lowestprice}}
                    <button @click = "RemoveFavorite(formData.drugInfo.openfda?.product_ndc?.[0])">Delete Favorite</button>
</div>
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
        fetch('https://localhost:7011/Medication/FavoriteAdd?genericName='+this.formData.drugInfo.openfda?.generic_name?.[0]+ '&brandName='
        + this.formData.drugInfo.openfda?.brand_name?.[0]+'&productID='+this.formData.drugInfo.openfda?.product_ndc?.[0],requestOptions)
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
        width: 300px;
        margin: 0 auto
    }
</style>
