<template>
    <div  class="form">
        <CurrentWeightGoal @read-data="onChildLoad" />
        <CreateGoal />
        <UpdateGoal/>

        
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

    export default {
        name : 'GoalView',
        data() {
        return {
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
        UpdateGoal
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
