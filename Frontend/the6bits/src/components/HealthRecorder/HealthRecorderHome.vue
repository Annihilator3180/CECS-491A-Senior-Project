<template>
    <h1>
        Health Recorder Home
            </h1>

        <div>
        <button @click="CreateRecord()"> Create Record </button>
        </div>
        <div>
        <button @click="ViewRecord()"> View Records </button>
        </div>


</template>

<script>
export default {
    methods:{
        CreateRecord(){
            this.$router.push('/CreateHealthRecord')
        },
        ViewRecord(){
            this.$router.push('/ViewMedicalRecords')
        }
    }

}
</script>

<style>

</style>