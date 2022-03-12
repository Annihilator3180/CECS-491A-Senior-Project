<template>
  <div class="hello">
    <h1>
      CreateRecords
    </h1>
    <form method = "get" id = "form1">
      <label for = "Record Name"> Record Name: </label><br>
      <input type = "text" id = "Record Name" name = "recordName" placeholder="Enter Record Name"> <br>

      <label for = "Record Description"> Record Description: </label><br>
      <input type = "text" id = "Record Description" name = "Record Description" placeholder="Enter Record Description"><br>
      </form>

      <input type="submit" value="Create Record">
  </div>
</template>

<script>
    export default {
        name : 'CreateRecords',
        data() {
        return {
            formData :{ 
                userId: '',
            },
        }
    },
    methods:{
        records(){
            const requestOptions = {
                method: "GET",
                credentials: 'include',
            };
            fetch('https://localhost:7011/Records/CreateRecords' + this.formData.userId ,requestOptions)
                .then(response => console.log(response))
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
