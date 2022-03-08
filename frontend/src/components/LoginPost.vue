<template>
    <div>
        <form @submit.prevent="loginPost">
            <div>
                <label for="userId">Username </label>
                <input type="text" id="userId" v-model ="formData.userId" />
            </div>
            <div>
                <label for="password">Password </label>
                <input type="text" id="password" v-model ="formData.password" />
            </div>
            <div>
                <label for="otp">OTP </label>
                <input type="text" id="otp" v-model ="formData.otp" />
            </div>
            <button>Login</button>
        </form>
    </div>
</template>

<script>
import axios from 'axios'
    export default {
        name : 'LoginPost',
        data() {
        return {
            formData :{ 
                userId: '',
                password: '',
                otp: '',
            },
        }
    },
    methods:{
        loginPost(){
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json",   withCredentials: true  },
                body: JSON.stringify({Username : this.formData.userId, Password : this.formData.password, Code: this.formData.otp   })
            };
            fetch('https://localhost:7011/Account/Login',requestOptions)
                .then(response => console.log(response))
            // axios
            // .post('https://localhost:7011/Account/Login',{Username : this.formData.userId, Password : this.formData.password, Code:this.formData.otp})
            // .then(response => console.log(response))
            // .catch(error => console.log(error))
        }
    }
}
</script>

<style scoped>

</style>