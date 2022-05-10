<template>
    <div class="form">
                <div >
                <label   for="userId">Delete Reminder</label>
                </div>
                <div >
                <label for="reminderID">Index </label>
                <input type="int" id="userId" v-model ="reminderID" placeholder="index"/>
                </div>
                <div >
                <button @click = "TrackerPost">Delete</button>
            </div>
            
            
                            {{message}}
    </div>
    
</template>

<script>
    export default {
        name : 'DeleteReminder',
        data() {
        return {
            formData :{ 
                userId: '',
            },
            message : '',
            reminderID : ''
        }
    },
    methods:{
        TrackerPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers:{"Authorization" :`Bearer ${sessionStorage.getItem('token')}`}
            };
            fetch(process.env.VUE_APP_BACKEND+'Reminder/DeleteReminder?reminderID='+this.reminderID,requestOptions)
                .then(response =>  response.text())
                .then(body => this.message = body)
           
            
        }
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }

</style>