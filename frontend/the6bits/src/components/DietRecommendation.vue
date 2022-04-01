<template>
    <div class="form">
        <form @submit.prevent="DietRecommendation">
            <div>
                <p> Answer the following questions for recipes</p>
            </div>
            <div>
                <p> What would you like to eat?</p>

                <input type="text" id="q" v-model="formData.q" />

            </div>
            <div>
                <p> What type of meal would you like? </p>
                <p>ex: (Breakfast, Lunch) </p>
                <input type="text" id="mealType" v-model="formData.mealType" />
            </div>
            <div>
                <p> What type of Dish would you like? </p>
                <p>ex: (Salad, Soup) </p>
                <input type="text" id="dishType" v-model="formData.dishType" />
            </div>
            <div>
                <p> What type of diet do you follow? </p>
                <p>ex: (Balanced, Low-card) </p>
                <input type="text" id="diet" v-model="formData.diet" />
            </div>
            <div>
                <p> Do you follow any dietary plans? </p>
                <p>ex: (Vegan, Dairy-free) </p>
                <input type="text" id="health" v-model="formData.health" />
            </div>
            <div>
                <p> What ingredients would you like to include?</p>
                <input type="text" id="ingr" v-model="formData.ingr" />
            </div>
            <div>
                <p> What cuisine would you like? </p>
                <p>ex: (Italian, American) </p>
                <input type="text" id="cuisineType" v-model="formData.cuisineType" />
            </div>
            <div>
                <p> How many calories?  </p>
                <input type="text" id="calories" v-model="formData.calories" />
            </div>
            <div>
                <p> Do you have any alergies? </p>
                <p>ex: (Banana, Shrimp) </p>

                <input type="text" id="excluded" v-model="formData.excluded" />
            </div>
            <button @click="DietRecommendation">Search</button>
            <div id="DietRecommendation">
            </div>

        </form>
    </div>

</template>

<script>
    export default {
        name: 'DietRecommendation',
        data() {
        return {
            formData :{
               q: '',
               dishType:'',
               diet: '',
               ingr: 0,
               cuisineType: '',
               calories: 0,
               excluded: '',
               health: '',
               mealType : '',
            },
            message: ''
        }
    },
        methods: {
            DietRecommendation() {
                const requestOptions = {
                    method: "get",
                    credentials: 'include',
                };
                fetch(`https://localhost:7011/DietRecommendation/Create?Q=${this.formData.q}&Diet=${this.formData.diet}&Health=${this.formData.health}&Ingr=${this.formData.ingr}&DishType=${this.formData.dishType}&Calories=${this.formData.calories}&CuisineType=${this.formData.cuisineType}&Excluded=${this.formData.excluded}&MealType=${this.formData.mealType}`, requestOptions)
                    .then((data) => { return data.json();}).then((completedata) => {
                        //console.log(completedata);
                        let data1 = "";
                        completedata.map((values) => {
                            data1+=
                            `<div class = "DietRecommendation">
                            <p> Recipe Name: ${values.label}</p>
                            <img src =${values.image}
                            <p> Ingredients: </p>
                            <p> ${values.ingredientLines}</p>
                            <p> Calories: </p>
                            <p> ${values.calories}</p>
                            </div>`;
                        });
                        document.getElementById("DietRecommendation").innerHTML = data1;

                    })           
        }
        }

    }

</script>
<style scoped>
    .form {
        width: 300px;
        margin: 0 auto
    }
</style>