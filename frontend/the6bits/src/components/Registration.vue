<template>
               

    <div class="form">
             <div className="user-name">    
                <div class="user-name-labels">
                    <label for="First name">First name </label>
                    <label for="Enter Last Name">last name </label>
                    
                </div>
                <input type="text" id="FirstName" v-model="formData.FirstName" />
                
                <input type="text" id="LastName" v-model="formData.LastName" />
            </div>
            <div className="password">
              
                <label for="password">Password </label>
                <input type="text" id="password" v-model="formData.password" />
            </div>
                                <div className="password-description">
                <p> Enter a usernameMust be 7-15 characters must contain a
                lowercase letter, uppercase letter, a number, and one of following symbols . , @ !  </p>
            </div>
            <div>
                <label for="Enter an email">Email </label>
                <input type="text" id="Email" v-model="formData.Email" />
            </div>
            <div>
                    <label   for="privOption">opt in Data Sharing: </label>
                    <select v-model ="formData.PrivOption">
                        <option value="">---</option>
                        <option value=1>Yes</option>
                        <option value=0>No</option>
                    </select>
        </div>
        <button @click = "RegistrationPost">Registration</button>
         {{message}}
    </div>
</template>

<script>
    export default {
        name: 'RegistrationPost',
        created(){
            window.setInterval(() => { this.timer+=1
            }, 1000)
            },
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
            timer:0
        }
    },
    beforeUnmount(){
            this.TimerTime(),
            this.timer=0
    },
    methods:{
        TimerTime(){
            const requestOptions = {
                method: "post",
                headers: { "Content-Type": "application/json",}
            };
            fetch(process.env.VUE_APP_BACKEND+'Account/ViewTime?time='+this.timer+'&view=Registration',requestOptions)
        },
         RegistrationPost(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({Username : this.formData.userId, password : this.formData.password, 
                Email: this.formData.Email, PrivOption: this.formData.PrivOption,isEnabled:0,isAdmin:0,FirstName:this.formData.FirstName, 
                LastName: this.formData.LastName })
            };
            const response= fetch(process.env.VUE_APP_BACKEND+'Account/Register/',requestOptions)                

                .then(response =>  response.text())
                .then(body => this.message = body)
        }
    }
}
</script>

<style scoped>
    .form 
    {
        width: 300px;
        margin: -1 auto
    }
    .user-name-labels
    {
        width: 70%;
        max-width: 1000px;
        display: flex;
        padding-top: 5%;
        justify-content: space-between;
        
    }
    .username-description
    {
        font-size: 8px;
    
    }
    .password
    {
        padding-top:5%;
        
    }
    .password-description{
    padding-bottom:5%;
    font-size: 8px;
        
    }
</style>