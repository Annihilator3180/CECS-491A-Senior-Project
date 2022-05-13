<template>
    <div  class="form">
    



        <button @click="activeTab = 'SaveFoodLog'"  class="ano">Custom</button>
        <button @click="activeTab = 'SearchFood'"  class="ano">Search</button>
                <button   v-on:click="ExportFood">Export</button>
        <component :is="activeTab"/>
        <FoodLogs @food-logs="onLogsLoad"  class="ano"/>
    </div>
</template>






<script>

import { JsonLogToCSVString }  from './WeightManagement'
import SearchFood from './SearchFoodItem.vue'
import SaveFoodLog from './SaveFoodLog.vue'
import FoodLogs from './WMComponents/FoodLogRow.vue'
    export default {
        name : 'FoodLogView',
        created(){
            window.setInterval(()=>{this.timer+=1}, 1000)
        },
        beforeUnmount(){
            this.TimerTime(),
            this.timer=0
        },
        data() {
        return {
            activeTab:'SaveFoodLog',
            res :{},
            message : '',
            editWeight:false,
            hasSavedWeightGoal:true,
            foodLogs:[],
            timer:0,
        }
    },
    components: {
        SaveFoodLog,
        SearchFood,
        FoodLogs,
    },
    methods:{
        JsonLogToCSVString,
        getCookie() {
             console.log(document.cookie);
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
        },
        TimerTime(){
        const requestOptions = {
          method:"post",
          headers: {"Content-Type": "application/json"},
        };
        fetch(process.ENV.VUE_APP_BACKEND+'Account/ViewTime?time='+this.timer+'&view=Food+Log+View', requestOptions)
      }
    
    }
    
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