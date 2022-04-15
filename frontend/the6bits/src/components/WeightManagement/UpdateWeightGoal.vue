<template>
    <div  class="form">

            <div >
                <H1>Update Weight Goal</H1>
                <label for="lbs">Weight Goal in Lbs: </label>
                <input type="int" id="username" v-model ="formData.weight" />


                <label for="lbs">Goal Date:</label>
                <input type="date" id="start" name="goal-date"  min=Date.now() v-model ="formData.goaldate">

                <label for="lbs">Exercise Level: </label>
                <select name="cars" id="cars" @change="checkvalue($event)" v-model="formData.calories">
                    <option value=1800>No Exercise : 1800 Calories burned per day</option>
                    <option value=2000>Low : 2000 Calories burned per day</option>
                    <option value=2500>Medium : 2500 Calories burned per day</option>
                    <option value=3000>High : 3000 Calories burned per day</option>
                    <option value=0>Custom </option>
                </select>

                <input type="int" name="custom-exercise" id="custom-exercise" style='display:none' v-model="formData.calories"/>

                {{message}}
            </div>
            <button @click = "UpdateGoal">Save</button>


    </div>
</template>

<script>

import { GoalRequest }  from './WeightManagement'


    export default {
        name : 'UpdateGoal',
        data() {
        return {
            formData :{ 
                weight: 0,
                goaldate: '',
                calories: 0,
            },
            message : '',
        }
    },
    methods:{
        UpdateGoal(){
            GoalRequest(this.formData,"UpdateGoal")
                .then(value => this.message = value)
        },
        GoalRequest,
        
        checkvalue:function(event){
            console.log(event.target.value)
            if(event.target.value===0)
                document.getElementById('custom-exercise').style.display='block';
            else
                document.getElementById('custom-exercise').style.display='none'; 
        },


    },
    
    }


</script>

<style scoped>   
label {
    /* Other styling... */
    text-align: right;
    clear: both;
    float:left;
    margin-right:15px;
}
.my-custom-class {
  position: absolute;
  right: 0;
  width: 300px;
  border: solid red 2px;
}
</style>
