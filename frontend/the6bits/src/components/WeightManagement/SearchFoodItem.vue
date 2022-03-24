<template>
    <div  class="form">
            <div >
                <H1>Search Food</H1>
                <label for="Search Food">Enter Food : </label>
                <input type="text" id="food" v-model ="formData.food" />
                {{message}}
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
                .then(body => this.message = body)
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