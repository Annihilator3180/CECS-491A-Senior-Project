<template>
    <div class="form">
            <div>

                <label for="Enter a username">Username </label>
                <input type="text" id="userId" v-model="formData.userId" />
            </div>
            <div className="username-description">
                    <p> Enter a usernameMust be 7-15 characters may contain 
                    lowercase letters, uppercase letters, numbers, . , @ !  </p>
            </div>
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
        <button @click = "RegistrationGet">Registration</button>
    </div>
</template>

<script>
    export default {
        name: 'RegistrationGet',
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
        }
    },
    methods:{
        async RegistrationGet(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({Username : this.formData.userId, password : this.formData.password, 
                Email: this.formData.Email, PrivOption: this.formData.PrivOption,isEnabled:0,isAdmin:0,FirstName:this.formData.FirstName, 
                LastName: this.formData.LastName  })
            };
            const response= await fetch('https://localhost:7011/Account/Register',requestOptions)
            const data= await response.json();
            this.totalVuePackages=data.total;
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