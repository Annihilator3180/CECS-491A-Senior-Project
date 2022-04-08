<template>
            
    <div class="form">

        <tr v-for="f in formData.favoriteDrugsList" :key="f">
         <td>{{f.generic_name}}</td>
         <td>{{f.product_id}}</td>
         <td>{{f.brand_name}}</td>
         <button @click = "FavoriteDrugListPost">View Drug</button>
         
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
                favoriteDrugsList: [],
                generic_name: '',
                product_id: '',
                brand_name: '',
                lowestprice: 0,
                lowestPriceLocation: ''
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
                .then(body => this.formData.favoriteDrugsList = JSON.parse(body))
                .catch((error) =>{
                    this.message="Error retrieving favorite list"
                });
        }
    }
}
</script>