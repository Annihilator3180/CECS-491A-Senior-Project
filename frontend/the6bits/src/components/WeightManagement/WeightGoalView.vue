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
        <FoodLogs/>
    </div>
</template>






<script>

import CurrentWeightGoal from './CurrentWeightGoal.vue';
import { GoalRequest }  from './WeightManagement'
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
