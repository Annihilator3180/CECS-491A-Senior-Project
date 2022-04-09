<template>
    <div class="form">
        <form @submit.prevent="DeleteAccount">
            <div>
                <p> Delete Account</p>
            </div>
            <button @click="Delete">Send Delete</button>
        </form>
    </div>

</template>
<script>
    export default {
        name: 'DeleteAccount',
        methods: {
            Delete() {
                const requestOptions = {
                    method: "POST",
                    credentials: 'include',
                
                };
                fetch('https://localhost:7011/Account/Delete', requestOptions)
                    .then(response => console.log(response))
                
            }
        }
    }
</script>
<style scoped>
    .form {
        width: 300px;
        margin: 0 auto
    }
</style>