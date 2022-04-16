<template>
    <div class="form">
            <div>
                
                <p> Enter Drug Name</p>
                
                <label for="drugName">Drug name </label> 
                <input type="text" id="drugName" v-model="drugName" /> 
                <button @click = "FindDrug">Search</button> {{this.formData.FindDrugResponse.error}}
            </div>
         <p>&nbsp;&nbsp;</p>
       <div v-if="formData.FindDrugResponse.success==true">
        <th>brand name</th>
         <tr v-for="f in formData.FindDrugResponse.data" :key="f">
         <td>{{f.brand_name}}</td>
        <button @click = "$router.push({name:'ViewDrug',params:{id: f.brand_name}})">View Drug</button>
         </tr>
    </div>
</div>
</template>
<script>
    export default {
        name: 'MedSearch',
        data() {
        return {
            drugName:"",
            formData :{
               FindDrugResponse: [],
               
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
            
            fetch(process.env.VUE_APP_BACKEND+'Medication/Search?drugName='+this.drugName,requestOptions)
                .then(response => response.text())
                .then(body =>this.formData.FindDrugResponse = JSON.parse(body))
        }
    }
}
</script>
<style scoped>
    .form {
        width: 100%;
        margin: 0 auto
    }
 td:nth-child(odd){
    border-style: solid;
    border-color: grey;
    padding: 20px;
}

tr:nth-child(even) {
    background-color: lightgrey;
    color: black
}
tr:nth-child(odd) {
    background-color: white;
    color: black
}
td,th {
    border: 1px solid rgb(190, 190, 190);
    padding: 10px;
}

th {
    background-color: #696969;
    color: #fff;
}



</style>
