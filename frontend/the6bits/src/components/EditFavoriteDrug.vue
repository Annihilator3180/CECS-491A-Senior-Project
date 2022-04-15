<template>
    <div class="form">
        {{drugName}} 
            <div className="price">
                
                <label for="price">Enter a price </label>
                <input type="number" id="price" v-model="formData.lowestprice"/>
            </div>
            <div className="price-description">
                    <p> price must not be negative or greater than 9 million.
                        Lowest previous price: {{formData.drugInfoResponse.data.favoriteDrug.lowestprice}}</p>
            </div>
            <div>
            <label for="Enter location of price">Location of price </label>
                <input type="text" id="price" v-model="formData.lowestpriceLocation"/>
            </div>
            <div className="location-description">
            <p> Must be under 150 characters  Previous Location: {{formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation}}</p>
                
            </div>
            <button @click = "UpdateDrug()">Update Favorite</button> 
            {{formData.message}}
            </div>

    
    

</template>
<script>
    export default {
        name: 'EditFavoriteDrug',
        created(){
            this.ViewDrug()
        },
        data() {
        return {
            drugName:this.$route.params.id,
            formData :{
                drugInfoResponse: [],
                lowestprice: null,
                lowestpriceLocation: null,
                message:""
            },
            
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
                fetch(process.env.VUE_APP_BACKEND+'Medication/viewDrug?generic_name='+this.drugName,requestOptions)
                .then(response => response.text())
                .then(body => this.formData.drugInfoResponse= JSON.parse(body))
        },
        UpdateDrug(){
            if(!this.formData.lowestprice){
               this.formData.lowestprice= this.formData.drugInfoResponse.data.favoriteDrug.lowestprice
            }
            if(!this.formData.lowestpriceLocation){
               this.formData.lowestpriceLocation= this.formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation
            }
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({
                    "generic_name": this.formData.drugInfoResponse.data.favoriteDrug.generic_name,
                    "product_id": this.formData.drugInfoResponse.data.favoriteDrug.product_id,
                    "brand_name": this.formData.drugInfoResponse.data.favoriteDrug.brand_name,
                    "lowestprice": this.formData.lowestprice,
                    "lowestPriceLocation": this.formData.lowestpriceLocation
                })

            };
            fetch(process.env.VUE_APP_BACKEND+'Medication/UpdateFavorite/',requestOptions)
                .then(response => response.text())
                .then(body => this.formData.message=body)
                .catch(()=>{
                    this.formData.message="Error updating"
                });
        },
    }
}
    
</script>
<style scoped>
    .form {
        width: 100%;
        margin: 0 auto
    }
    .price-description{
        font-size: 9px;
        color:grey
    }
    .location-description{
        font-size: 9px;
        color:grey
    }
</style>
