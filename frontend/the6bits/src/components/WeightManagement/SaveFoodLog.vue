<template>
    <div  class="form">
            <div >
                <H1>Custom Food</H1>
                <label>Food name:
                    <input type="text" id="food" v-model ="foodData.foodName"  required/>
                </label>
                <label>Description: 
                    <input type="text" id="food" v-model ="foodData.description" required />
                </label>
                <label>Calories: 
                    <input type="text" id="food" v-model ="foodData.calories" required/>
                </label>
                <label>Date: 
                    <input type="date" id="start" name="goal-date"  min=Date.now() v-model ="foodData.foodLogDate" required >
                </label>
                <label>Carbs: 
                    <input type="text" id="food" v-model ="foodData.carbs" />
                </label>
                <label>Protein:
                    <input type="text" id="food" v-model ="foodData.protein" />
                </label>
                <label>Fat: 
                    <input type="text" id="food" v-model ="foodData.fat" />
                    <button class="buttoncss"  @click = "SaveFood" :disabled="!foodData.foodName || !foodData.description || !foodData.calories" >Save</button>

                </label>
            </div>
    </div>
    
</template>

<script>

import { SaveFoodItem } from './WeightManagement' 
    export default {
        name : 'SaveFoodLog',
        data() {
        return {
            foodData :{ 
                foodName: null,
                description: null,
                calories: null,
                foodLogDate: null,
                carbs: null,
                protein: null,
                fat: null,
            },
        }
    },
    methods:{
        SaveFoodItem,
        SaveFood(){
            SaveFoodItem(this.foodData)     
        }

        
        
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