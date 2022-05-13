<template>
    <div  class="form">
            <div >
                <H1>Delete/Enable/Disable</H1>
                <label for="Username">Username: </label>
                <input type="text" id="username" v-model ="formData.username" />
            </div>
            <button @click = "UMDelete">Send Delete User</button>
            <button @click = "UMEnable">Send Enable User</button>
            <button @click = "UMDisable">Send Disable User</button>


    </div>
</template>

<script>
    export default {
        name : 'UMUsername',
        data() {
        return {
            formData :{ 
                username: '',
            },
        }
    },
    methods:{
        UMDelete(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers:{
                      "Authorization" : `Bearer ${sessionStorage.getItem('token')}`
                    },
            
            };
            fetch(process.env.VUE_APP_BACKEND+'UM/DeleteAccount?username=' +this.formData.username ,requestOptions)
                .then(response => console.log(response))
        },
        UMEnable(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers:{
                      "Authorization" : `Bearer ${sessionStorage.getItem('token')}`
                    },
            
            };
            fetch(process.env.VUE_APP_BACKEND+'UM/EnableAccount?username=' +this.formData.username ,requestOptions)
                .then(response => console.log(response))
        },
        UMDisable(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers:{
                      "Authorization" : `Bearer ${sessionStorage.getItem('token')}`
                    },
            };
            fetch(process.env.VUE_APP_BACKEND+'UM/DisableAccount?username=' +this.formData.username ,requestOptions)
                .then(response => console.log(response))
        }
        
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }
label {
    /* Other styling... */
    text-align: right;
    clear: both;
    float:left;
    margin-right:15px;
}
</style>