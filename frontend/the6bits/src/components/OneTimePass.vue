<template>
    <div  class="form">
        <form  @submit.prevent="otpPost">
            <div >
                <label   for="userId">Username </label>
                <input type="text" id="userId" v-model ="formData.userId" />
            </div>
            <button>Send OTP</button>
        </form>
    </div>
</template>

<script>
    export default {
        name : 'OTPPost',
        data() {
        return {
            formData :{ 
                userId: '',
            },
        }
    },
    methods:{
        otpPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            };
            fetch('https://localhost:7011/Account/OTP?Username=' + this.formData.userId ,requestOptions)
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
.form { width:300px; margin:0 auto }

</style>