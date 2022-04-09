<template>
    <div class="form">
            <div>
            {{message}}
             <button @click = "ViewDrug">View</button>
    </div>
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
        ViewDrug(){
            const requestOptions = {
                method: "post",
                credentials: 'include',
                body: JSON.stringify({ generic_name: "coffee"})

            };
            fetch('https://localhost:7011/Medication/viewDrug',requestOptions)
                .then(response => console.log(response.json()))
                .then(body => this.message = body)
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
