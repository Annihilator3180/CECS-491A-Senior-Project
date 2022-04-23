<template>
    <div class="form">
        <form>
            <div>
                <p> Select a Timing: </p>
                <select id="MealType" v-model="filters.mealType" @change="filterResults()">
                    <option value=""> All </option>
                    <option value="breakfast"> Breakfast </option>
                    <option value="lunch"> Lunch </option>
                    <option value="dinner"> Dinner </option>
                    <option value="snack"> Snack </option>
                    <option value="teatime"> Teatime </option>
                </select>
            </div>
            <div>
                <p> Select a Meal Type: </p>
                <select id="Health" v-model="filters.health" @change="filterResults()">
                    <option value=""> All </option>
                    <option value="alcohol-free"> alcohol-free  </option>
                    <option value="dairy-free"> dairy-free  </option>
                    <option value="egg-free"> egg-free  </option>
                    <option value="fish-free"> fish-free  </option>
                    <option value="gluten-free"> gluten-free  </option>
                    <option value="keto-friendly"> keto-friendly  </option>
                    <option value="kosher"> kosher  </option>
                    <option value="low-sugar"> low-sugar  </option>
                    <option value="paleo"> paleo  </option>
                    <option value="peanut-free"> peanut-free  </option>
                    <option value="pescatarian"> pescatarian  </option>
                    <option value="pork-free"> pork-free  </option>
                    <option value="red-meat-free"> red-meat-free  </option>
                    <option value="shellfish-free"> shellfish-free  </option>
                    <option value="sugar-conscious"> sugar-conscious  </option>
                    <option value="vegan"> vegan  </option>
                    <option value="vegetarian"> vegetarian  </option>
                </select>
            </div>
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
            <div>
                <button @click="DietRecommendation" type="button">Search</button>
            </div>

            <div id="DietRecommendation">
                <template v-for="(values,index) in loadedRecepies" :key="index">
                    <div class="DietRecommendation">
                        <div v-html="values.label.link(values.url)"></div>
                        <img v-bind:src="values.image" />
                        <p> Ingredients: </p>
                        <p> {{values.ingredientLines}}</p>
                        <p> {{values.healthLabels}} </p>
                        <p> {{values.mealType}} </p>
                        <p> Calories: </p>
                        <p> {{values.calories}}</p>
                        <template v-if="!values.favorite">
                            <button type="button" @click="addFavorite(values.recipeId, index)">add to favorite</button>
                        </template>
                        <template v-else-if="values.favorite">
                            <button type="button" @click="deleteFavorite(values.recipeId, index)">Delete favorite</button>
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
    var skip = 0;
    var take = 5;

    export default {
        name: "DietRecommendation",
        data() {
            return {
                filters: {
                    mealType: "",
                    health: ""
                },
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
                    totalTime: 0,

                },
                message: "",
                allRecipes: [],
                loadedRecepies: [],
                favRecipes: []
            };
        },
        methods: {
            filterResults() {
                this.loadedRecepies = [];
                if (this.filters.mealType == "" && this.filters.health == "") {
                    this.allRecipes.splice(0, skip).forEach(x => {
                        this.loadedRecepies.push(x);
                    });
                } else if (this.filters.mealType != "" && this.filters.health == "") {
                    this.allRecipes.filter(x => x.mealType.filter(mt => mt.toLowerCase().indexOf(this.filters.mealType) !== -1).length != 0).forEach(x => {
                        this.loadedRecepies.push(x);
                    });
                } else if (this.filters.mealType == "" && this.filters.health != "") {
                    this.allRecipes.filter(x => x.healthLabels.filter(h => h.toLowerCase().indexOf(this.filters.health) !== -1).length != 0).forEach(x => {
                        this.loadedRecepies.push(x);
                    });
                } else if (this.filters.mealType != "" && this.filters.health != "") {
                    this.allRecipes.filter(x => x.mealType.filter(mt => mt.toLowerCase().indexOf(this.filters.mealType) !== -1).length != 0).forEach(x => {
                        this.loadedRecepies.push(x);
                    });
                    this.allRecipes.filter(x => x.healthLabels.filter(h => h.toLowerCase().indexOf(this.filters.health) !== -1).length != 0).forEach(x => {
                        this.loadedRecepies.push(x);
                    });
                }
            },
            onLoadMore() {
                if (this.allRecipes.length === 0 || skip > this.allRecipes.length) {
                    return;
                }
                var recipes = [...this.allRecipes];
                recipes.splice(skip, take).forEach(x => {
                    this.loadedRecepies.push(x);
                });
                skip += take;
            },
            DietRecommendation() {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                    headers: { "Authorization": `Bearer ${sessionStorage.getItem('token')}` }

                };
                fetch(process.env.VUE_APP_BACKEND+'DietRecommendation/GetFavorites',requestOptions)
                    .then((favData) => {
                        return favData.json();
                    })
                    .then((res) => {
                        console.log(res);
                        this.favRecipes = res;


                        fetch(
                            process.env.VUE_APP_BACKEND+`DietRecommendation/Create?Q=${this.formData.q}&Diet=${this.formData.diet}&Health=${this.formData.health}&Ingr=${this.formData.ingr}&DishType=${this.formData.dishType}&Calories=${this.formData.calories}&CuisineType=${this.formData.cuisineType}&Excluded=${this.formData.excluded}&MealType=${this.formData.mealType}&TotalTime=${this.formData.totalTime}`, requestOptions)
                            .then((data) => {
                                return data.json();
                            })
                            .then((completedata) => {
                                if (completedata.success === undefined) {
                                    skip = 0;
                                    this.loadedRecepies = [];
                                    completedata.forEach(x => {
                                        x.recipeId = x.uri.split('_')[1];
                                        x.favorite = res.filter(r => r === x.recipeId).length > 0;
                                    });
                                    this.allRecipes = completedata;
                                    var recipes = [...this.allRecipes];
                                    recipes.splice(skip, take).forEach(x => {
                                        this.loadedRecepies.push(x);
                                    });
                                    skip += take;
                                } else {
                                    this.message = completedata.message;
                                }
                            });

                    }).catch((error) => {
                        this.message = "Error getting favorite list"
                    });
                
            },
            addFavorite(recipeId, index) {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                    headers: { "Authorization": `Bearer ${sessionStorage.getItem('token')}` }

                };
                fetch(
                    process.env.VUE_APP_BACKEND + 'DietRecommendation/AddFavorite?recipeId=' + this.recipeId,
                    requestOptions
                ).then((data) => {
                    return data.json();
                }).then((data) => {
                    console.log(data);
                    if (data.success) {
                        this.loadedRecepies[index].favorite = true;
                    } else {
                        this.message = data.message;
                    }
                })
            },
            deleteFavorite(recipeId, index) {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                    headers: { "Authorization": `Bearer ${sessionStorage.getItem('token')}` }

                };
                fetch(
                    process.env.VUE_APP_BACKEND + 'DietRecommendation/DeleteFavorite?recipeId=' + this.recipeId,
                    requestOptions
                ).then((data) => {
                    return data.json();
                }).then((data) => {
                    if (data.success) {
                        this.loadedRecepies[index].favorite = false;
                    } else {
                        this.message = data.message;
                    }
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