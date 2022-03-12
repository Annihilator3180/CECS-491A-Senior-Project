<template>
  <div class="form">
    <h1>
      Password Reset
    </h1>
    <form method = "get" id = "form1">
      <label for = "Password"> Password: </label><br>
      <input type = "text" id = "Password" v-model="formData.password" placeholder="New Password"> <br>

      <label for = "Password2"> Verify Password: </label><br>
      <input type = "text" id = "Password2" v-model="formData.password2" placeholder="Re-Enter Password"><br>
      </form>

      <button @click = "PasswordReset"> Reset Password </button>
  </div>
</template>

<script>
  export default{
    name : 'PasswordReset',
    data(){
    return {
      formData : {
        password: '',
        password2: '',
      },
    }
  },
  methods:{
    AccountRecovery(){
      const requestOptions = {
        method: "POST",
        credentials : 'include',
        headers: {"Content-Type": "application/json",
        'Accept': 'application/json'},
        body: JSON.stringify(
        { username : this.formData.username,
          email : this.formData.email
          })
      };
      fetch('https://localhost:7011/Account/Recovery',requestOptions)
      
                .then((response) => {
                   return response.json();
                })
                .then ((myJson) => {
                  console.log(myJson)
                  window.alert(myJson)
                });
    }
  }
}
</script>



