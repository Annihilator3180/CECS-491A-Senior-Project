<template>
    <div class="form">
            <div>
                
                <p> Enter Drug Name</p>
                
                <label for="drugName">Drug name </label>
                <input type="text" id="drugName" v-model="drugName" />
            </div>
            <button @click = "FindDrug">Search</button>
         <tr v-for="f in formData.searchList" :key="f">
         <td>{{f.brand_name}}</td>
        <button @click = "$router.push({name:'ViewDrug',params:{id: f.brand_name}})">View Drug</button>
         </tr>
    </div>

</template>
<script>
    export default {
        name: 'MedSearch',
        data() {
        return {
            drugName:"",
            formData :{
               searchList: [],
               
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
            
            fetch('https://localhost:7011/Medication/Search?drugName='+this.drugName,requestOptions)
                .then(response => response.text())
                .then(body =>this.formData.searchList = JSON.parse(body))
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
