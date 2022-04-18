<template>
     <div>

  </div>


</template>
<script>

    export default {
        name: 'logOut',
        created(){
            this.logout()
        },
    methods:{
        logout(){
            sessionStorage.removeItem('token')
        },
}
    }
</script>
