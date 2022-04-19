<template>
    <div >
        <h1>Current Weight Goal</h1>
        <p>Current Weight : {{this.currentGoal.currentWeight}}</p>
        <p>Goal Weight : {{this.currentGoal.goalWeight}}</p>
        <p>Goal Date : {{this.currentGoal.goalDate}}</p>
        <p>Calories Burned Per Day : {{this.currentGoal.exerciseLevel}}</p>
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