<template>
    <div class="form">
        <form @submit.prevent="loginPost">
            <div>
                <label for="userId">Username </label>
                <input type="text" id="userId" v-model ="formData.userId" placeholder="username"/>
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
            {{message}}
        </form>
    </div>
</template>

<script>
    export default {
        name : 'LoginPost',
        data() {
        return {
            formData :{ 
                userId: '',
                password: '',
                otp: '',
            },
            message:''
        }
    },
    methods:{
        loginPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({Username : this.formData.userId, Password : this.formData.password, Code: this.formData.otp   })
            };
            fetch(process.env.VUE_APP_BACKEND+'Account/Login',requestOptions)
                .then(response => response.text())
                .then(data=> {
                    if(data.split('.').length == 3){
                        sessionStorage.setItem('token', data)
                        this.$router.push({name:'home'})
                        this.message = "Logged In."
                    }
                    })

        }
    }
}
</script>

<style scoped>
.form { width:300px; margin:0 auto }

</style>