<template>
    <div class="form">
            <div>
                
                <p> Enter Drug Name</p>
                
                <label for="drugName">Drug name </label> 
                <input type="text" id="drugName" v-model="drugName" /> 
                <button @click = "FindDrug">Search</button> {{this.formData.FindDrugResponse.error}}
            </div>
       <div v-if="formData.FindDrugResponse.success==true">
           
        <th>brand name</th>
         <tr v-for="f in formData.FindDrugResponse.data" :key="f">
         <td v-if="f.brand_name">{{f.brand_name}}</td>
         <td v-else-if="f.generic_name">{{f.generic_name}}</td>
        <button v-if="f.brand_name!=''" @click = "$router.push({name:'ViewDrug',params:{id: f.brand_name}})">View Drug</button>
        <button v-else-if="f.generic_name!=''" @click = "$router.push({name:'ViewDrug',params:{id: f.generic_name}})">View Drug</button>
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
                headers : {"Authorization" :`Bearer ${sessionStorage.getItem('token')}`}

            };
            if (this.drugName!=""){
            
            fetch(process.env.VUE_APP_BACKEND+'Medication/Search?drugName='+this.drugName,requestOptions)
                .then(response => response.json())
                .then(body =>this.formData.FindDrugResponse = body)
            }

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
td {

    padding: 10px;
}

th {
    padding: 10px;
    background-color: #696969;
    color: #fff;
}



</style>
