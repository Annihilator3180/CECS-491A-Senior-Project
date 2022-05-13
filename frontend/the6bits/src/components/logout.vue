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
        
},
beforeMount() {
    if (localStorage.getItem('reloaded')) {
        // The page was just reloaded. Clear the value from local storage
        // so that it will reload the next time this page is visited.
        localStorage.removeItem('reloaded');
    } else {
        // Set a flag so that we know not to reload the page twice.
        localStorage.setItem('reloaded', '1');
        location.reload();
    }
}

    }
</script>
