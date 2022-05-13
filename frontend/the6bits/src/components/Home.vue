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
  name: 'HomeHome',
  created(){
    if (!sessionStorage.reloaded) {
      sessionStorage.setItem("reloaded", "true");
      window.location.reload(true);
    }
    else{
      sessionStorage.removeItem("reloaded")
    }
    window.setInterval(() => { this.timer+=1}, 1000)
  },
  beforeUnmount(){
        this.TimerTime(),
        this.timer=0
    },

  data () {
    return {
      TimerTime(){
      const requestOptions = {
          method: "post",
          headers: { "Content-Type": "application/json",}
      };
      fetch(process.env.VUE_APP_BACKEND+'Account/ViewTime?time='+this.timer+'&view=Home',requestOptions)
      },
      msg: 'Welcome to Your Vue.js App',
      sessionStorage:sessionStorage.getItem('token'),
      mode:process.env.NODE_ENV,
      timer : 0
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>