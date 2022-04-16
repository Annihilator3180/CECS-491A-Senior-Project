<template>
    <div  class="form">
            <div >
                <H1>Custom Food</H1>
                <label>Enter Food name: </label>
                <input type="text" id="food" v-model ="foodData.foodName" />
                <label>Enter description: </label>
                <input type="text" id="food" v-model ="foodData.description" />
                <label>Calories: </label>
                <input type="text" id="food" v-model ="foodData.calories" />
                <label>Date: </label>
                <input type="date" id="start" name="goal-date"  min=Date.now() v-model ="foodData.foodLogDate">
                <label>Carbs: </label>
                <input type="text" id="food" v-model ="foodData.carbs" />
                <label>Protein: </label>
                <input type="text" id="food" v-model ="foodData.protein" />
                <label>Fat: </label>
                <input type="text" id="food" v-model ="foodData.fat" />
            </div>
            <button @click = "SaveFood">Save</button>


    </div>
</template>

<script>
    export default {
        name : 'SaveFoodLog',
        data() {
        return {
            foodData :{ 
                foodName: '',
                description: '',
                calories: null,
                foodLogDate: null,
                carbs: null,
                protein: null,
                fat: null,
            },
        }
    },
    methods:{
        SaveFood(){
            const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},
                body: JSON.stringify(this.foodData)
            };
            fetch('https://localhost:7011/WeightManagement/SaveFood',requestOptions)
                .then(response =>  response.text())
                .then(body => this.formData.foods = JSON.parse(body))
            window.location.reload();
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