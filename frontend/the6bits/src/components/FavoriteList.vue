<template>
            
    <div class="form">
        <button @click = "FavoriteListPost">Favorites List</button>
         {{message}}
    </div>

</template>
<script>
    export default {
        name: 'FavoriteListPost',
        data() {
        return {
            formData :{
                userId: '',
                password: '',
                Email: '',
                PrivOption: 0,
                isEnabled: 0,
                isAdmin: 0,
                FirstName: "",
                LastName: ""
            },
            message : '',
        }
    },
    methods:{
         FavoriteListPost(){
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