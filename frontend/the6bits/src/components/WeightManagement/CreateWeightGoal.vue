<template>
    <div  class="form">
            <div >
                <H1>Create Goal</H1>

                <label for="lbs">Current Weight in Lbs: </label>
                <input type="int" id="username" v-model ="formData.currentWeight" />

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
            <button @click = "CreateGoal">Save</button>
    </div>
</template>

<script>

import { GoalRequest }  from './WeightManagement'


    export default {
        name : 'CreateGoal',
        data() {
        return {
            res :{},
            formData :{ 
                currentWeight:0,
                goalWeight: 0,
                goalDate: '',
                exerciseLevel: 0,
            },
            message : '',
        }
    },
    methods:{
        CreateGoal(){
            GoalRequest(this.formData,"CreateGoal")
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

        onChildLoad (val) {
            this.res=val
            this.formData = val
            console.log(val) // someValue
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

</style>
