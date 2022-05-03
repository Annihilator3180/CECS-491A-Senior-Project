<template>
    <div class="form">
                <div >
                <label   for="userId">Edit Reminder</label>
                </div>
                <div >
                <label for="reminderID">Index </label>
                <input type="int" id="userId" v-model ="reminderID" placeholder="index"/>
                </div>
                <div >
                <label for="name">name </label>
                <input type="text" id="userId" v-model ="name" placeholder="name"/>
                </div>
                <div >
                <label for="description">description </label>
                <input type="text" id="userId" v-model ="description" placeholder="description"/>
                </div>
                <div >
                <label for="date">date </label>
                <input type="text" id="userId" v-model ="date" placeholder="date"/>
                </div>
                <div >
                <label for="time">time </label>
                <input type="text" id="userId" v-model ="time" placeholder="time"/>
                </div>
                <div >
                <label for="repeat">repeat </label>
                <input type="text" id="userId" v-model ="repeat" placeholder="repeat"/>
                </div>
                <div >
                <button @click = "TrackerPost">Edit</button>
            </div>
            
            
                            {{message}}
    </div>
    
</template>

<script>
    export default {
        name : 'EditReminder',
        data() {
        return {
            formData :{ 
                userId: '',
            },
            reminderID: '',
            message: '',
            name: '',
            description: '',
            date: '',
            time: '',
            repeat: ''
        }
    },
    methods:{
        TrackerPost(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers:{"Authorization" :`Bearer ${sessionStorage.getItem('token')}`}
            };
            fetch(process.env.VUE_APP_BACKEND+'Reminder/EditReminder?reminderID='+this.reminderID+
            '&name='+this.name+'&description='+this.description+'&date='+this.date+
            '&time='+this.time+'&repeat='+this.repeat,requestOptions)
                .then(response =>  response.text())
                .then(body => this.message = body)
           
            
        }
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }

</style>