<template>
    <div  id="wrapper" class="form">
        
            <div  v-if="hasSavedWeightGoal" class=" my-custom-class">        
                <CurrentWeightGoal @read-data="onChildLoad">
                </CurrentWeightGoal>
                <button @click="editWeight = !editWeight">Edit</button>
            </div>
        <div  id="inner1">
            <CreateGoal v-if="!hasSavedWeightGoal" class="my-custom-class"/>
            <UpdateGoal v-if="editWeight"></UpdateGoal> 
        </div>

        

        <LoadProfile  id="inner2"/>
                    <UploadProgressImage/>

        <LoadWeightImages/>
    </div>
</template>






<script>

import CurrentWeightGoal from './CurrentWeightGoal.vue';
import { GoalRequest,JsonLogToCSVString }  from './WeightManagement'
import CreateGoal from './CreateWeightGoal.vue'
import UpdateGoal from './UpdateWeightGoal.vue'
import LoadProfile from './WMComponents/CountingCalories.vue'
import UploadProgressImage from './WMComponents/UploadProgressImage.vue'
import LoadWeightImages from './WMComponents/LoadWeightImages.vue'
    export default {
        name : 'GoalView',
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
            formData :{ 
                goalWeight: 0,
                goalDate: '',
                exerciseLevel: 0,
            },
            message : '',
            editWeight:false,
            hasSavedWeightGoal:true,
            timer:0
        }
    },
    components: {
        CurrentWeightGoal,
        CreateGoal,
        UpdateGoal,
        LoadProfile,
        UploadProgressImage,
        LoadWeightImages
    },
    methods:{
        JsonLogToCSVString,
        getCookie() {
             console.log(document.cookie);
        },
        onChildLoad(value){
            if(value.goalWeight!=null){
                this.hasSavedWeightGoal = true;
            }
            else{
                this.hasSavedWeightGoal = false;
            }
        },
      TimerTime(){
        const requestOptions = {
          method:"post",
          headers: {"Content-Type": "application/json"},
        };
        fetch(process.ENV.VUE_APP_BACKEND+'Account/ViewTime?time='+this.timer+'&view=Weight+Goal+View', requestOptions)
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
  width: 400px;
  
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


#wrapper{
        margin-left:auto;
        margin-right:auto;
        height:auto; 
        width:auto;
    }
#inner1 {
   float:left; 
}

#inner2{
   float:left; 
   clear: left;
}
</style>