<template>
    <div >
        <li v-for="FoodItem in FoodLogs" :key="FoodItem">
            {{ FoodItem.foodName }} | {{ FoodItem.calories }} | {{ FoodItem.description }} | {{ FoodItem.foodLogDate }}
        </li>
    </div> 
</template>
//TODO: ADD Dropdown and Delete Button
<script>
    import { GetAllFoodLogs }  from '@/components/WeightManagement/WeightManagement'
    export default {
        name : 'GetFoodLogs',
        data() {
        return {
            FoodLogs : [],
        }
    },
    methods:{
        GetAllFoodLogs,
        LoadGoal(){
            //IS THIS TRASH?
            this.GetAllFoodLogs().then(value => {
                this.FoodLogs = value;
                this.$emit('AllFoodLogs',value)
                console.log(value);
                })

        },
    },

    beforeMount(){
        this.LoadGoal();
    },

        
    }


</script>
