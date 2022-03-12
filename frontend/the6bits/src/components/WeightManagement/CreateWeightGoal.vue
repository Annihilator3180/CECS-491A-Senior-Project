<template>
    <div  class="form">
            <div >
                <H1>Save Weight Goal</H1>
                <label for="Username">Weight Goal in Lbs: </label>
                <input type="text" id="username" v-model ="formData.weight" />
                {{message}}
            </div>
            <button @click = "UMDelete">Save</button>


    </div>
</template>

<script>
    export default {
        name : 'CreateGoal',
        data() {
        return {
            formData :{ 
                weight: '',
            },
            message : '',
        }
    },
    methods:{
        UMDelete(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            
            };
            fetch('https://localhost:7011/WeightManagement/CreateGoal?goalNum=' +this.formData.weight ,requestOptions)
                .then(response => this.message = response)
        }
        
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }
label {
    /* Other styling... */
    text-align: right;
    clear: both;
    float:left;
    margin-right:15px;
}
</style>