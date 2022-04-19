<template>
    <div class="bottomleft" >
        <li v-for="FoodItem in FoodLogs" :key="FoodItem">
            {{ FoodItem.foodName }} | {{ FoodItem.calories }} | {{ FoodItem.description }} | {{ FoodItem.foodLogDate }}
            <button v-on:click="DeleteFoodLog(FoodItem)">Delete</button>
        </li>
    </div> 
</template>




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
                console.log(value);
                this.$emit('food-logs', value)
                })
        },
        DeleteFoodLog(foodItem){
            const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
            };
            fetch(process.env.VUE_APP_BACKEND+'WeightManagement/DeleteFoodLog?id='+foodItem.id,requestOptions)
                .then(response =>  response.text())
                .then(body => this.Profile = JSON.parse(body))
                        window.location.reload();
        },
    },
    beforeMount(){
        this.LoadGoal();
    },
        
    }
</script>

<style scoped>   

.bottomleft {
position: absolute;
left:    0;
bottom:   0;
    padding-bottom:200px;

}


</style>