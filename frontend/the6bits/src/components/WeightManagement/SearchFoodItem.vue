<template>
    <div  class="form">
            <div >
                <H1>Search Food</H1>
                <label for="Search Food">Enter Food : </label>
                <input type="text" id="food" v-model ="formData.food" />
                {{message}}
                <tbody>
                    <tr v-for="f in formData.foods" :key="f">
                        <td>{{f.food.label}}</td>
                        <td>Calories : {{f.food.nutrients.enerC_KCAL}}</td>
                        <td>Protein :{{f.food.nutrients.procnt}}</td>
                        <td>Fat :{{f.food.nutrients.fat}}</td>
                        <td>Carbs :{{f.food.nutrients.chocdf}}</td>
                        <button  :disabled="!FoodItem.foodLogDate" @click = "MapFood(f.food)">Save</button>

                    </tr>

                </tbody>
            </div>
            <label>Date: 
                    <input type="date" id="start" name="goal-date"  min=Date.now() v-model ="FoodItem.foodLogDate" required >
            </label>
            <button @click = "SearchFood">Search</button>


    </div>
</template>

<script>
import { SaveFoodItem } from './WeightManagement' 

    export default {
        name : 'SearchFood',
        data() {
        return {
            formData :{ 
                food: '',
                foods: [],
            },
            message : '',
            FoodItem:{
                foodLogDate:null,
                calories:null,
                },
        }
    },
    methods:{
        SearchFood(){
            const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`},
            
            };
            fetch(process.env.VUE_APP_BACKEND+'WeightManagement/SearchFood?queryString=' +this.formData.food ,requestOptions)
                .then(response =>  response.text())
                .then(body => this.formData.foods = JSON.parse(body))
        },
        MapFood(webFood){
            this.FoodItem.foodName = webFood.label;
            this.FoodItem.description = webFood.label;
            this.FoodItem.calories = webFood.nutrients.enerC_KCAL;
            this.FoodItem.protein = webFood.nutrients.procnt;
            this.FoodItem.carbs = webFood.nutrients.chocdf;
            this.FoodItem.carbs = webFood.nutrients.fat;
            SaveFoodItem(this.FoodItem)
        },
        SaveFoodItem
        

        
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }
label {
    /* Other styling... */
    text-align: right;
    clear: both;
    float:left;
    margin-right:15px;
    
}
</style>