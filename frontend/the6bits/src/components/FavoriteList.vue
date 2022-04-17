<template>
            
    <div class="form">
        <div v-if="formData.ViewFavoriteRequest.isSuccess==false">
            {{formData.ViewFavoriteRequest.error}}
        </div>
         <div v-else>
        <tr>
        <th>brand Name</th>
        <th>product ndc</th>
        </tr>
        <tr v-for="f in formData.ViewFavoriteRequest.data" :key="f">
         <td>{{f.brand_name}}</td>
         <td>{{f.product_id}}</td>
        <button @click = "$router.push({name:'ViewDrug',params:{id: f.brand_name}})">View Drug</button>
        <button @click = "RemoveFavorite(f.product_id)">Remove Favorite</button>
        <button @click = "$router.push({name:'EditDrug',params:{id: f.brand_name}})">Edit Favorite</button>
        </tr> 
        {{formData.ViewFavoriteRequest.data.length}}/100
          </div>
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
                favoriteDrugsList: [],
                ViewFavoriteRequest: [],
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
            const response= fetch(process.env.VUE_APP_BACKEND+'Medication/FavoriteView',requestOptions)                
                .then(response =>  response.text())
                .then(body => this.message =body)
                .then(body=>this.formData.ViewFavoriteRequest= JSON.parse(body))
                .catch((error) =>{
                    this.message="Error retrieving favorite list"
                });

        },
        RemoveFavorite(product_id){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
               
            };
            const response= fetch(process.env.VUE_APP_BACKEND+'Medication/DeleteFavorite?product_id='
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
<style scoped>
    .form {
        width: 100%;
        margin: 0 auto
    }
td:nth-child(odd){
    padding: 20px;
}

tr:nth-child(even) {
    background-color: lightgrey;
    color: black
}
tr:nth-child(odd) {
    background-color: white;
    color: black
}
td {

    padding: 10px;
}

th {
    padding: 10px;
    background-color: #696969;
    color: #fff;
}


</style>