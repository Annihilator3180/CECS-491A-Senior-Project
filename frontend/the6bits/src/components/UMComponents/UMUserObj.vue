<template>
    <div  class="form">
            <div >
                <H1>Create/Update</H1>

                <div>
                <label for="Usernmae">Username: </label>
                <input type="text" id="username" v-model ="formData.username" />
                <label for="password">Password: </label>
                <input type="text" id="password" v-model ="formData.password" />
                <label for="email">Email: </label>
                <input type="text" id="email" v-model ="formData.email" />
                <label for="firstName">First Name: </label>
                <input type="text" id="firstName" v-model ="formData.firstName" />
                <label for="lastName">Last Name: </label>
                <input type="text" id="lastName" v-model ="formData.lastName" />
                <label   for="IsAdmin">IsAdmin: </label>
                </div>
                <div>
                    <select v-model ="formData.isAdmin">
                        <option value="">---</option>
                        <option value=1>Yes</option>
                        <option value=0>No</option>
                    </select>
                </div>
                <div>
                <label   for="IsEnabled">IsEnabled: </label>
                    <select v-model ="formData.isEnabled">
                        <option value="">---</option>
                        <option value=1>Yes</option>
                        <option value=0>No</option>
                    </select>
                </div>
                <div>
                    <label   for="privOption">privOption: </label>
                    <select v-model ="formData.privOption">
                        <option value="">---</option>
                        <option value=1>Yes</option>
                        <option value=0>No</option>
                    </select>
                </div>
            </div>
            <button @click = "UMCreate">Send Create User</button >
            <button @click = "UMUpdate">Send Update User</button >
    </div>
</template>

<script>
    
        export default {
        name : 'UMUserObj',
         
        data() {
        return {
            
            formData :{ 
                username: '',
                password: '',
                email: '',
                firstName: '',
                lastName: '',
                isAdmin: 0,
                isEnabled: 0,
                privOption: 0,
            },
        }
    },
    methods:{
        UMCreate(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify(
                    {Username : this.formData.username,
                     Password : this.formData.password,
                     Email: this.formData.email,
                     FirstName: this.formData.firstName,
                     LastName: this.formData.lastName,
                     isAdmin: Number(this.formData.isAdmin),
                     isEnabled: Number(this.formData.isEnabled),
                     privOption: Number(this.formData.privOption),
                     }),
            
            };
            fetch('https://localhost:7011/UM/CreateAccount' ,requestOptions)
                .then(response => console.log(response))
        },
        UMUpdate(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify(
                    {Username : this.formData.username,
                     Password : this.formData.password,
                     Email: this.formData.email,
                     FirstName: this.formData.firstName,
                     LastName: this.formData.lastName,
                     isAdmin: Number(this.formData.isAdmin),
                     isEnabled: Number(this.formData.isEnabled),
                     privOption: Number(this.formData.privOption),
                     }),
            
            };
            fetch('https://localhost:7011/UM/UpdateAccount' ,requestOptions)
                .then(response => console.log(response))
        }
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }
label {
    /* Other styling... */
    text-align: right;
    clear: both;
    float:left;
    margin-right:15px;
}
</style>