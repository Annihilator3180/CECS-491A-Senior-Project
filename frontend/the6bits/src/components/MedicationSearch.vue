<template>
    <div class="form">
            <div>
                <p> Enter Drug Name</p>
                <label for="drugName">Drug name </label>
                <input type="text" id="drugName" v-model="formData.drugName" />
            </div>
            <button @click = "FindDrug">Search</button>
            {{message}}
    </div>

</template>
<script>
    export default {
        name: 'MedSearch',
        data() {
        return {
            formData :{
               drugName: ""
            },
             message : '',
        }
    },
    methods:{
        FindDrug(){
            const requestOptions = {
                method: "get",
                credentials: 'include',

            };
            
            fetch('https://localhost:7011/Medication/Search?drugName='+this.formData.drugName,requestOptions)
                .then(response => response.text())
                .then(body => this.message = body)
                .error("can't find result")
                console.log("b")
                console.log(this.message)
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
