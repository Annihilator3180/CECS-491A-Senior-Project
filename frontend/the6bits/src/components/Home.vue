<template>
  <div class="home">
    {{testing}}
    {{mode}}
    <h1>Home</h1>
  </div>
</template>

<script>
export default {
  name: 'HomeHome',
  data () {
    return {
      msg: 'Welcome to Your Vue.js App',
      testing:process.env.VUE_APP_TITLE,
      mode:process.env.NODE_ENV
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>