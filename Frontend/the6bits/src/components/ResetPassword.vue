<template>
  <div class="form">
    <h1>
      Password Reset
    </h1>
    <form method = "get" id = "form1">
      <label for = "Password"> Password: </label><br>
      <input type = "text" name = "Password" v-model="formData.password" placeholder="New Password"> <br>

      <label for = "Password2"> Verify Password: </label><br>
      <input type = "text" name = "Password2" v-model="formData.password2" placeholder="Re-Enter Password"><br>
      </form>

      <button @click = "PasswordReset"> Reset Password </button>
      {{message}}
  </div>
</template>

<script>

  export default{
    name : 'PasswordReset',
    data(){
    return {
      formData : {
        password: '',
        username: this.$route.query.username,
        randomString: this.$route.query.randomString
      },
      message:"",
    }
  },
  methods:{
    PasswordReset(){
      const requestOptions = {
        method: "POST",
        credentials : 'include',
      };
      fetch('https://localhost:7011/Account/ResetPassword?randomString=' + this.formData.randomString + '&username=' + this.formData.username + '&password=' +  this.formData.password,requestOptions)
      
              .then(response =>  response.text())
              .then(body => this.message = body) 
                 }
  
  }
}
</script>



