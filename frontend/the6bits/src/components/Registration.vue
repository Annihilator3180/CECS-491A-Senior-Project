<template>
    <div class="form">
            <div>
                <p> Enter a username <br>Must be 7-15 characters<br>may contain <br>lowercase letters <br>uppercase letters <br>numbers <br>special characters . , @ !  </p>
                <label for="Enter a username">Username </label>
                <input type="text" id="userId" v-model="formData.userId" />
            </div>
             <div>
                <label for="First name">First name </label>
                <input type="text" id="FirstName" v-model="formData.FirstName" />
            </div>
            <div>
                <label for="Enter Last Name">last name </label>
                <input type="text" id="LastName" v-model="formData.LastName" />
            </div>
            <div>
                <p> Enter a password <br>Must be 8-30 characters<br>must contain one of each<br>lowercase letters <br>uppercase letters <br>numbers <br>special characters . , @ !  </p>
                <label for="password">Password </label>
                <input type="text" id="password" v-model="formData.password" />
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
        RegistrationGet(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({Username : this.formData.userId, password : this.formData.password, 
                Email: this.formData.Email, PrivOption: this.formData.PrivOption,isEnabled:0,isAdmin:0,FirstName:this.formData.FirstName, 
                LastName: this.formData.LastName  })
            };
            fetch('https://localhost:7011/Account/Register',requestOptions)
                .then(response => console.log(response))
        }
    }
}
</script>

<style scoped>
    .form {
        width: 300px;
        margin: 0 auto
    }
</style>