<template>
    <div class="form">
        <form >
            <div>
                <label for="userId">Username </label>
                <input type="text" id="userId" v-model ="formData.userId" placeholder="username"/>
            </div>
            <div>
                <label for="password">Password </label>
                <input type="text" id="password" v-model ="formData.password" />
            </div>
            <div v-if="passGood">
                <label for="otp">OTP </label>
                <input type="text" id="otp" v-model ="formData.otp" />
                <button type="button" @click="loginPost">Submit OTP</button>
            </div>
            <button type="button" @click="otp">Request OTP</button>
            {{message}}
            {{otpMessage}}
        </form>
    </div>
</template>

<script>
    export default {
        name : 'LoginPost',
        created(){
            window.setInterval(()=>{this.timer+=1}, 1000)
        },
        beforeUnmount(){
            this.TimerTime(),
            this.timer=0
        },
        data() {
        return {
            formData :{ 
                userId: '',
                password: '',
                otp: '',
            },
            passGood:false,
            message:'',
            otpMessage:'',
            timer:0,
        }
    },
    methods:{
        loginPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({Username : this.formData.userId, Code: this.formData.otp   })
            };
            fetch(process.env.VUE_APP_BACKEND+'Account/Login',requestOptions)
                .then(response => response.text())
                .then(data=> {
                    if(data.split('.').length == 3){
                        var obj = this.parseJwt(data)
                        sessionStorage.setItem('IsAdmin', obj.IsAdmin)
                        sessionStorage.setItem('token', data)
                        this.$router.push({name:"home"})
                        this.message = "Logged In."
                    }
                    })

        },
        otp(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
            };
            fetch(process.env.VUE_APP_BACKEND+'Account/OTP?Username='+ this.formData.userId
            +"&Password="+this.formData.password,requestOptions)
                .then(response => response.text())
                .then(data=> {
                    console.log(data)
                    this.passGood = true
                    this.otpMessage = data

                    })
        },
        TimerTime(){
        const requestOptions = {
          method:"post",
          headers: {"Content-Type": "application/json"},
        };
        fetch(process.env.VUE_APP_BACKEND+'Account/ViewTime?time='+this.timer+'&view=Login', requestOptions)
      },
      parseJwt(token){
        try {
            return JSON.parse(atob(token.split('.')[1]));
        } catch (e) {
            return null;
        }
    }
    
    }
}
</script>

<style scoped>
.form { width:300px; margin:0 auto }

</style>