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
            fetch('https://localhost:7011/Account/Login',requestOptions)
                .then(response => response.text())
                .then(data=>localStorage.setItem('token', data))

        }
    }
}
</script>

<style scoped>
.form { width:300px; margin:0 auto }

</style>