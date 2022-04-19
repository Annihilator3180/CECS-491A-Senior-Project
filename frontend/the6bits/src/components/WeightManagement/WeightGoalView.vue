<template>
    <div  class="form">
            <div  v-if="hasSavedWeightGoal" class=" my-custom-class">        
                <CurrentWeightGoal @read-data="onChildLoad">
                </CurrentWeightGoal>
                <button @click="editWeight = !editWeight">Edit</button>
            </div>
        <CreateGoal v-if="!hasSavedWeightGoal" />
        <UpdateGoal v-if="editWeight"></UpdateGoal> 

        

        <button @click="activeTab = 'SaveFoodLog'"  class="ano">Custom</button>
        <button @click="activeTab = 'SearchFood'"  class="ano">Search</button>
                <button   v-on:click="ExportFood">Export</button>
        <LoadProfile />

        <component :is="activeTab"/>
        <FoodLogs @food-logs="onLogsLoad"  class="ano"/>
    </div>
</template>






<script>

import CurrentWeightGoal from './CurrentWeightGoal.vue';
import { GoalRequest,JsonLogToCSVString }  from './WeightManagement'
import CreateGoal from './CreateWeightGoal.vue'
import UpdateGoal from './UpdateWeightGoal.vue'
import SearchFood from './SearchFoodItem.vue'
import SaveFoodLog from './SaveFoodLog.vue'
import LoadProfile from './WMComponents/CountingCalories.vue'
import FoodLogs from './WMComponents/FoodLogRow.vue'
    export default {
        name : 'GoalView',
        data() {
        return {
            activeTab:'SaveFoodLog',
            res :{},
            formData :{ 
                goalWeight: 0,
                goalDate: '',
                exerciseLevel: 0,
            },
            message : '',
            editWeight:false,
            hasSavedWeightGoal:true,
            foodLogs:[],
        }
    },
    components: {
        CurrentWeightGoal,
        CreateGoal,
        UpdateGoal,
        SaveFoodLog,
        SearchFood,
        FoodLogs,
        LoadProfile
    },
    methods:{
        JsonLogToCSVString,
        getCookie() {
             console.log(document.cookie);
    },
        onChildLoad(value){
            if(JSON.stringify(value)!='{}'){
                this.hasSavedWeightGoal = true;
            }
            else{
                this.hasSavedWeightGoal = false;
            }
        },

        onLogsLoad(logs){
            this.foodLogs = logs;

        },
        async ExportFood()
        {
            console.log(this.foodLogs)
            var logString = await JsonLogToCSVString(this.foodLogs)
            var encodedUri =  encodeURI(logString);
            window.open(encodedUri);
        }
    
    }
    
    }


</script>

<style>   
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

  button {
  color:rgb(57, 100, 180);
  display: inline;
  margin: 30px;
  padding: 7px 35px;
  font: 300 150% langdon;
  border: 3px solid black;
  cursor: pointer;
} 

button:hover {
  background: #f7f7f7;
  border: 1px solid #8b8b8b;
}

button:active {
  background: #2e2e2e;
  border: 1px solid black;
  color: rgb(255, 0, 0);
  }
</style>