<template>
            
    <div class="form">
         {{formData}}
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
                generic_name: '',
                product_id: '',
                brand_name: '',
                lowestprice: 0,
                lowestPriceLocation: ""
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
                .then(body => this.message = body)
        }
    }
}
</script>