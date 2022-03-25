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
                        <td>{{f.food.nutrients.enerC_KCAL}}</td>
                        <td>{{f.food.nutrients.procnt}}</td>
                        <td>{{f.food.nutrients.fat}}</td>
                        <td>{{f.food.nutrients.chocdf}}</td>
                        <td>{{f.food.nutrients.fibtg}}</td>



                    </tr>

                </tbody>
            </div>
            <button @click = "SearchFood">Search</button>


    </div>
</template>

<script>
    export default {
        name : 'CreateGoal',
        data() {
        return {
            formData :{ 
                food: '',
                foods: [],
            },
            message : '',
        }
    },
    methods:{
        SearchFood(){
            const requestOptions = {
                method: "GET",
                credentials: 'include',
            
            };
            fetch('https://localhost:7011/WeightManagement/SearchFood?queryString=' +this.formData.food ,requestOptions)
                .then(response =>  response.text())
                .then(body => this.formData.foods = JSON.parse(body))
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