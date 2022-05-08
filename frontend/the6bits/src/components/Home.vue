<template>
  <div class="home">

    <h1>Home</h1>
        <div v-if="sessionStorage">
      you are logged in
        </div>
        <div v-else>
        please log in
        </div>
  </div>
</template>

<script>
export default {
  name: 'homeShit',
  created(){
        if (!sessionStorage.reloaded) {
        sessionStorage.setItem("reloaded", "true");
        window.location.reload(true);
    }
    else{
      sessionStorage.removeItem("reloaded")
    }

        },
  data () {
    return {
      msg: 'Welcome to Your Vue.js App',
      sessionStorage:sessionStorage.getItem('token'),
      mode:process.env.NODE_ENV
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>