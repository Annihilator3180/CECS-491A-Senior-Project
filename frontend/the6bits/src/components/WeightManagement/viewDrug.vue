<template>
    <div class="form">
            <div>
            <label for="Enter a username">Username </label>
            <input type="text" id="drugName" v-model="drugName"/>
             <button @click = "ViewDrug">View</button>
             {{formData.drugInfo.boxed_warning}}
             {{formData.drugInfo.pregnancy}}
             {{formData.drugInfo.indications_and_usage}}
             {{formData.drugInfo.warnings_and_cautions}}
             {{formData.drugInfo.inactive_ingredient}}
             {{formData.drugInfo.information_for_patients}}
             {{formData.drugInfo.isFavorited}}
             {{formData.drugInfo.openfda[0].generic_name}}
             
    </div>
    </div>

</template>
<script>
    export default {
        name: 'ViewDrug',
        data() {
        return {
            formData :{
                drugInfo: [],
                boxed_warning: [],
                indications_and_usage: [],
                warnings_and_cautions: [],
                pregnancy: [],
                inactive_ingredient:[],
                information_for_patients: [],
                isFavorited: 0,
                openfda:[],
                favoriteDrug: [],
                brand_name:[],
            },
            
             message : '',
        }
    },
    methods:{
        ViewDrug(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
            fetch('https://localhost:7011/Medication/viewDrug?generic_name='+this.drugName,requestOptions)
                .then(response => response.text())
                .then(body => this.formData.drugInfo = JSON.parse(body))
                console.log("t")
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
