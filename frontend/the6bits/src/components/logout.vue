<template>
     <div>

  </div>


</template>
<script>

    export default {
        name: 'logOut',
        created(){
            this.logout(),
            this.$router.push({name:'home'})
        },
    methods:{
        logout(){
            sessionStorage.removeItem('token')

            
            
        },
        
}

    }
</script>
