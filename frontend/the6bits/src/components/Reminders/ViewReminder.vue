<template>
    <div  class="form">
        <form  @submit.prevent="TrackerPost">
            <div >
                <label   for="userId">Tracker </label>
            </div>
            <button>View Reminder</button>
                            {{message}}
        </form>
    </div>
</template>

<script>
    export default {
        name : 'ViewReminder',
        data() {
        return {
            formData :{ 
                userId: '',
            },
            message : '',
        }
    },
    methods:{
        TrackerPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            };
            fetch('https://localhost:7011/Reminder/ViewReminder', requestOptions)
                .then(response =>  response.text())
                .then(body => this.message = body)
            
        }
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }

</style>