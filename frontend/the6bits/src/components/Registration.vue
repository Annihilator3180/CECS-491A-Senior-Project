<template>
    <div class="form">
        <form @submit.prevent="Registration">
            <div>
                <p> Enter a username <br>Must be 7-15 characters<br>may contain <br>lowercase letters <br>uppercase letters <br>numbers <br>special characters . , @ !  </p>
                <label for="Enter a username">Username </label>
                <input type="text" id="userId" v-model="formData.userId" />
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
                    <select v-model ="formData.privOption">
                        <option value="">---</option>
                        <option value=1>Yes</option>
                        <option value=0>No</option>
                    </select>
            </div>
            <button>Register</button>
        </form>
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
                email: '',
                priv: 0
            },
        }
    },
    methods:{
        Registration(){
            const requestOptions = {
                method: "get",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify({Username : this.formData.userId, Password : this.formData.password, Email: this.formData.email, PrivOption: this.formData.PrivOption  })
            };
            fetch('https://localhost:7011/Account/CreateAccount',requestOptions)
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