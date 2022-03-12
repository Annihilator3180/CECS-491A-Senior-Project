<template>
    <div  class="form">
        <form  @submit.prevent="TrackerPost">
            <div >
                <label   for="userId">racker </label>
            </div>
            <button>Send OTP</button>
                            {{message}}
        </form>
    </div>
</template>

<script>
    export default {
        name : 'TrackingLog',
        data() {
        return {
            formData :{ 
                userId: '',
            },
            message : '',
        }
    },
    methods:{
        otpPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            };
            fetch('https://localhost:7011/Account/OTP?Username=' + this.formData.userId ,requestOptions)
                .then(response =>  response.text())
                .then(body => this.message = body)
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