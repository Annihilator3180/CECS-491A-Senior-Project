<template>
  <div class="form">
    <h1>
      Account Recovery
    </h1>
    <form method = "get" id = "form1">
      <label for = "Email"> Email Address: </label><br>
      <input type = "text" id = "Email" v-model="formData.email" placeholder="Enter Email Address"> <br>

      <label for = "Username"> Username: </label><br>
      <input type = "text" id = "Username" v-model="formData.username" placeholder="Enter Username"><br>
      </form>

      <button @click = "AccountRecovery"> Recover Account </button>
  </div>
</template>

<script>
  export default{
    name : 'AccountRecovery',
    data(){
    return {
      formData : {
        email: '',
        username: '',
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



