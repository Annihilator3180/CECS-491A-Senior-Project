<template>
    <div class="form">
        <form>
            <div>
                <p>Answer the following questions for recipes</p>
            </div>
            <div>
                <p>What would you like to eat?</p>

                <input type="text" id="q" v-model="formData.q" />
            </div>
            <div>
                <p>What type of meal would you like?</p>
                <p>ex: (Breakfast, Lunch)</p>
                <input type="text" id="mealType" v-model="formData.mealType" />
            </div>
            <div>
                <p>What type of Dish would you like?</p>
                <p>ex: (Salad, Soup)</p>
                <input type="text" id="dishType" v-model="formData.dishType" />
            </div>
            <div>
                <p>What type of diet do you follow?</p>
                <p>ex: (Balanced, Low-card)</p>
                <input type="text" id="diet" v-model="formData.diet" />
            </div>

            <div>
                <p>Do you follow any dietary plans?</p>
                <p>ex: (Vegan, Dairy-free)</p>
                <input type="text" id="health" v-model="formData.health" />
            </div>
            <div>
                <p>What ingredients would you like to include?</p>
                <input type="text" id="ingr" v-model="formData.ingr" />
            </div>
            <div>
                <p>What cuisine would you like?</p>
                <p>ex: (Italian, American)</p>
                <input type="text" id="cuisineType" v-model="formData.cuisineType" />
            </div>
            <div>
                <p>How many calories?</p>
                <input type="text" id="calories" v-model="formData.calories" />
            </div>
            <div>
                <p>Do you have any alergies?</p>
                <p>ex: (Banana, Shrimp)</p>

                <input type="text" id="excluded" v-model="formData.excluded" />
            </div>
            <button @click="DietRecommendation" type="button">Search</button>
            <div id="DietRecommendation">
                <template v-for="(values,index) in loadedRecepies" :key="index">
                    <div class="DietRecommendation">
                        <p> {{values.label.link(values.url)}} </p>
                        <img v-bind:src="values.image" />
                        <p> Ingredients: </p>
                        <p> {{values.ingredientLines}}</p>
                        <p> Calories: </p>
                        <p> {{values.calories}}</p>
                        <p> {{values.recipeId}}</p>
                        <template v-if="!values.favorite">
                            <button type="button" @click="addFavorite(values.recipeId)">add to favorite</button>
                        </template>
                        <template v-else-if="values.favorite">
                            <button type="button" @click="deleteFavorite(values.recipeId)">Delete favorite</button>
                        </template>
                        <p> For more recipe details please click on the link. </p>
                    </div>
                </template>
            </div>
            <button @click="onLoadMore" type="button">Load more</button>
        </form>
    </div>



</template>

<script>
    var allData = [];
    var skip = 0;
    var take = 5;


    export default {
        name: "DietRecommendation",
        data() {
            return {
                formData: {
                    q: "pizza",
                    dishType: "Main course",
                    diet: "balanced",
                    ingr: 5,
                    cuisineType: "Italian",
                    calories: 500,
                    excluded: "almonds",
                    health: "peanut-free",
                    mealType: "Lunch",
                },
                message: "",
                loadedRecepies: [],
                favRecipes: []
            };
        },
        methods: {
            onLoadMore() {
                console.log('load more clicked');
                if (allData.length === 0 || skip > allData.length) {
                    return;
                }
                for (let i = skip; i < skip + take; i++) {
                    var values = allData[i];
                    values.recipeId = values.uri.split('_')[1];
                    this.loadedRecepies.push(values);
                }
                skip += 5;
            },
            DietRecommendation() {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                };
                var pagination = 0;
                var setpag = 0;
                fetch(
                    `https://localhost:7011/DietRecommendation/GetFavorites`,
                    requestOptions
                )
                    .then((favData) => {
                        return favData.json();
                    })
                    .then((res) => {
                        console.log(res);
                        this.favRecipes = res;


                        fetch(
                            `https://localhost:7011/DietRecommendation/Create?Q=${this.formData.q}&Diet=${this.formData.diet}&Health=${this.formData.health}&Ingr=${this.formData.ingr}&DishType=${this.formData.dishType}&Calories=${this.formData.calories}&CuisineType=${this.formData.cuisineType}&Excluded=${this.formData.excluded}&MealType=${this.formData.mealType}`,
                            requestOptions
                        )
                            .then((data) => {
                                return data.json();
                            })
                            .then((completedata) => {
                                this.loadedRecepies = [];
                                allData = completedata;
                                for (let i = skip; i < skip + take; i++) {
                                    var values = allData[i];
                                    values.recipeId = values.uri.split('_')[1];
                                    this.loadedRecepies.push(values);
                                }
                                skip += 5;
                            });

                    }).catch((error) => {
                        this.message = "Error getting favorite list"
                    });
                
            },
            addFavorite(recipeId) {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                };
                fetch(
                    `https://localhost:7011/DietRecommendation/AddFavorite?recipeId=${recipeId}`,
                    requestOptions
                ).then((data) => {
                    return data.json();
                }).then((data) => {
                    console.log(data).catch((error) => {
                        this.message = "Error adding recipe to favorite list"
                    });
                })
            },
            deleteFavorite(recipeId) {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                };
                fetch(
                    `https://localhost:7011/DietRecommendation/DeleteFavorite?recipeId=${recipeId}`,
                    requestOptions
                ).then((data) => {
                    return data.json();
                }).then((data) => {
                    console.log(data).catch((error) => {
                        this.message = "Error deleting item from favorite list"
                    });
                })
            },
        },
    };
</script>
<style scoped>
    .form {
        width: 300px;
        margin: 0 auto;
    }
</style>