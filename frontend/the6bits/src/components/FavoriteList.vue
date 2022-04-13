<template>
            
    <div class="form">
        <tr v-for="f in formData.favoriteDrugsList" :key="f">
         <td>{{f.brand_name}}</td>
         <td>{{f.product_id}}</td>
        <button @click = "$router.push({name:'ViewDrug',params:{id: f.generic_name}})">View Drug</button>
        <button @click = "RemoveFavorite(f.product_id)">Remove Favorite</button>
         
        </tr>  
          </div>

</template>
<script>
    export default {
        name: 'FavoriteDrugListPost',
        created(){
            this.FavoriteDrugListPost()
        },
        data() {
        return {
            formData :{
                favoriteDrugsList: []
            },
            message : '',
        }
    },
    methods:{
         FavoriteDrugListPost(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
               
            };
            const response= fetch('https://localhost:7011/Medication/FavoriteView',requestOptions)                
                .then(response =>  response.text())
                .then(body => this.message =body)
                .then(body=>this.formData.favoriteDrugsList= JSON.parse(body))
                .catch((error) =>{
                    this.message="Error retrieving favorite list"
                });
                console.log(this.formData.favoriteDrugsList.brand_name)
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
                .then(body=>this.formData.favoriteDrugsList = JSON.parse(body))
                .catch((error) =>{
                    this.message="Error retrieving favorite list"
                });
        }
    }
}
</script>