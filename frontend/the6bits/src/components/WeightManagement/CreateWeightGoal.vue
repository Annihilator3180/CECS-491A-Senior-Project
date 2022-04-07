<template>
    <div  class="form">
            <div >
                <H1>Save Weight Goal</H1>
                <label for="lbs">Weight Goal in Lbs: </label>
                <input type="text" id="username" v-model ="formData.weight" />


                <label for="lbs">Goal Date:</label>
                <input type="date" id="start" name="goal-date"  min=Date.now() v-model ="formData.goaldate">

                <label for="lbs">Exercise Level: </label>
                <select name="cars" id="cars" @change="checkvalue($event)"   v-model="key">
                    <option value=1800>No Exercise : 1800 Calories burned per day</option>
                    <option value=2000>Low : 2000 Calories burned per day</option>
                    <option value=2500>Medium : 2500 Calories burned per day</option>
                    <option value=3000>High : 3000 Calories burned per day</option>
                    <option value="others">Custom </option>
                </select>

                <input type="text" name="custom-exercise" id="custom-exercise" style='display:none'/>

                {{message}}
            </div>
            <button @click = "CreateGoal">Save</button>


    </div>
</template>

<script>
    export default {
        name : 'CreateGoal',
        data() {
        return {
            formData :{ 
                weight: '',
                goaldate: Date.now(),
            },
            message : '',
            key: "",
        }
    },
    methods:{
        CreateGoal(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
            
            };
            fetch('https://localhost:7011/WeightManagement/CreateGoal?goalNum=' +this.formData.weight ,requestOptions)
                .then(response =>  response.text())
                .then(body => this.message = body)
        },
        
        checkvalue:function(event){
            if(event.target.value==="others")
                document.getElementById('custom-exercise').style.display='block';
            else
                document.getElementById('custom-exercise').style.display='none'; 
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
