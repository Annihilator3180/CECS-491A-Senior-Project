<template>
    <div class="form">
        {{drugName}} 
            <div>
                
                <label for="price">Enter a price </label>
                <input type="text" id="price" v-model="formData.lowestprice"/>
            </div>
            <div className="price-description">
                    <p> price must not be negative or greater than 9 million.
                        Lowest previous price: </p> {{formData.drugInfo.favoriteDrug?.lowestprice}}
            </div>
            <label for="Enter location of price">Location of price </label>
                <input type="text" id="price" v-model="formData.lowestpriceLocation"/>
            </div>
            <div className="username-description">
            <p> Must be under 150 characters  Previous Location:</p>
                {{formData.drugInfo.favoriteDrug?.lowestPriceLocation}}
            </div>
                 {{formData.drugInfo.favoriteDrug?.[0].generic_name}}
            <button @click = "UpdateDrug()">Update Favorite</button>
            {{message}}
             
    
    

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
                drugInfo: [],
                lowestprice: 0,
                lowestpriceLocation: ""
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
                .then(body => this.formData.drugInfo = JSON.parse(body))
        },
        UpdateDrug(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({
                    "generic_name": this.formData.drugInfo.favoriteDrug?.generic_name,
                    "product_id": this.formData.drugInfo.favoriteDrug?.product_id,
                    "brand_name": this.formData.drugInfo.favoriteDrug?.brand_name,
                    "lowestprice": this.formData.lowestprice,
                    "lowestPriceLocation": this.formData.lowestpriceLocation
                })

            };
            fetch('https://localhost:7011/Medication/UpdateFavorite',requestOptions)
                .then(response => response.text())
                .then(body => this.message=body)
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
                .then(body=>this.message=body)
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
