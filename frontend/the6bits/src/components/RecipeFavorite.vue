<template>


    <div id="DietRecommendation">
        <template v-for="(values,index) in loadedRecepies" :key="index">
            <div class="DietRecommendation">
                <div v-html="values.label.link(values.url)"></div>
                <img v-bind:src="values.image" />
                <p> Ingredients: </p>
                <p> {{values.ingredientLines}}</p>
                <p> Calories: </p>
                <p> {{values.calories}}</p>
                <button type="button" @click="deleteFavorite(values.recipeId, index)">Delete favorite</button>
                <p> For more recipe details please click on the link. </p>
            </div>
        </template>
    </div>


</template>
<script>
    export default {
        name: "RecipeFavorite",
        data() {
            return {
                message: "",
                loadedRecepies: [],
            };
        },
        created: function () {
            this.GetFavorites();
        },
        methods: {
            GetFavorites() {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                    headers: { "Authorization": `Bearer ${sessionStorage.getItem('token')}` }
                    

                };
                fetch(
                    process.env.VUE_APP_BACKEND+ 'DietRecommendation/GetRecipes',
                    requestOptions
                )
                    .then((favData) => {
                        return favData.json();
                    })
                    .then((res) => {
                        console.log(res);
                        res.forEach(x => {
                            x.recipeId = x.uri.split('_')[1];
                        });
                        this.loadedRecepies = res;
                    }).catch((error) => {
                        this.message = "Error getting favorite list"
                    });

            },
            deleteFavorite(recipeId, index) {
                const requestOptions = {
                    method: "get",
                    credentials: "include",
                    headers: { "Authorization": `Bearer ${sessionStorage.getItem('token')}` }
                    
                };
                fetch(
                    process.env.VUE_APP_BACKEND+'DietRecommendation/DeleteFavorite?recipeId='+ this.recipeId,
                    requestOptions
                ).then((data) => {
                    return data.json();
                }).then((data) => {
                    if (data.success) {
                        this.loadedRecepies = this.loadedRecepies.filter((x, i) => i !== index);
                    } else {
                        this.message = data.message;
                    }
                })
            },


        },
    };
</script>