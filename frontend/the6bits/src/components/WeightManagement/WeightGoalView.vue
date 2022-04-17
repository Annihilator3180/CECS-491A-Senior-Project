<template>
    <div  class="form">
        <CurrentWeightGoal @read-data="onChildLoad" />
        <CreateGoal />
        <UpdateGoal/>

    </div>
</template>

<script>

import CurrentWeightGoal from './CurrentWeightGoal.vue';
import { GoalRequest }  from './WeightManagement'
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
        }
    },
    components: {
        CurrentWeightGoal,
        CreateGoal,
        UpdateGoal
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
