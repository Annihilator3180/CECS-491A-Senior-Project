<template>
    <div  class="form">
            <div  v-if="hasSavedWeightGoal" class=" my-custom-class">        
                <CurrentWeightGoal @read-data="onChildLoad">
                </CurrentWeightGoal>
                <button @click="editWeight = !editWeight">Edit</button>
            </div>
        <CreateGoal v-if="!hasSavedWeightGoal" />
        <UpdateGoal v-if="editWeight"></UpdateGoal> 

        
        <button @click="activeTab = 'SaveFoodLog'">Custom</button>
        <button @click="activeTab = 'SearchFood'">Search</button>
        <component :is="activeTab"/>
        <button @click="ExportLogsAsCsv">Export</button>
        
        <FoodLogs @AllFoodLogs="updateMyVar"/>
    </div>
</template>






<script>

import CurrentWeightGoal from './CurrentWeightGoal.vue';
import { GoalRequest, JsonLogToCSVString }  from './WeightManagement'
import CreateGoal from './CreateWeightGoal.vue'
import UpdateGoal from './UpdateWeightGoal.vue'
import SearchFood from './SearchFoodItem.vue'
import SaveFoodLog from './SaveFoodLog.vue'
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
            allFoodLogs : null,
        }
    },
    components: {
        CurrentWeightGoal,
        CreateGoal,
        UpdateGoal,
        SaveFoodLog,
        SearchFood,
        FoodLogs
    },
    methods:{
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
        updateMyVar(val){
          this.allFoodLogs = val;
        },
        ExportLogsAsCsv(){
            var csvContent = JsonLogToCSVString(this.allFoodLogs)
            var encodedUri = encodeURI(csvContent);
            window.open(encodedUri);
            // create a new handle
            //const newHandle = await window.showSaveFilePicker();

            // create a FileSystemWritableFileStream to write to
            //const writableStream = await newHandle.createWritable();

            // write our file
            //await writableStream.write(imgBlob);

            // close the file and write the contents to disk.
            //await writableStream.close();
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
</style>
