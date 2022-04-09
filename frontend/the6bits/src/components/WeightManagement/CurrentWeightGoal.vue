<template>
    <div class="row my-custom-class">
        <h1>Current Weight Goal</h1>
        <p>Goal Weight : {{this.currentGoal.goalWeight}}</p>
        <p>Goal Date : {{this.currentGoal.goalDate}}</p>
        <p>Calories Burned Per Day : {{this.currentGoal.exerciseLevel}}</p>
        <button>Edit</button>
    </div> 
</template>

<script>
    import { ReadGoal }  from './WeightManagement'
    export default {
        name : 'CurrentWeightGoal',
        data() {
        return {
            currentGoal : {
            },
        }
    },
    methods:{
        ReadGoal,
        LoadGoal(){
            //IS THIS TRASH?
            this.ReadGoal().then(value => {
                this.currentGoal = value
                this.$emit('read-data', value);
                })

        },
    },

    beforeMount(){
        this.LoadGoal();
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
