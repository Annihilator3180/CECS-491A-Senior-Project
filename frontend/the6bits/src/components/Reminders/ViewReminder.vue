<template>
    <div>
            <div >
                <label   for="userId">Reminders </label>
                </div>
                <div >
                <label for="reminderID">Index </label>
                <input type="text" id="userId" v-model ="reminderID" placeholder="index"/>
                <button @click = "TrackerPost">view</button>
            </div>
            
                            {{message}}
    </div>
    
</template>

<script>
    export default {
        name : 'ViewReminder',
        created(){
            this.ViewReminder(),
            window.setInterval(() => { this.timer+=1}
            , 100)
        },
        data() {
        return {
            formData :{ 
                userId: '',
            },
            message : '',
            reminderID : '',
            time:0
        }
    },
    beforeUnmount(){
        this.TimerTime(),
        this.timer = 0
    },
    methods:{
        TimerTime(){
            const requestOptions = {
                method: "post",
                headers: { "Content-Type": "application/json",}
            };
            fetch(process.env.VUE_APP_BACKEND+'Account/ViewTime?time=' + this.timer + '&view=View+Reminder', requestOptions)
        },
        TrackerPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers:{"Authorization" :`Bearer ${sessionStorage.getItem('token')}`}
            };
            fetch(process.env.VUE_APP_BACKEND+'Reminder/ViewReminder?reminderID=' + this.reminderID, requestOptions)
                .then(response =>  response.text())
                .then(body => this.message = body)
            
        }
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }

</style>