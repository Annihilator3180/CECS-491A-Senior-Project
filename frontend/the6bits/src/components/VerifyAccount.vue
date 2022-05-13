<template>
    <div class="form">
    <div v-if="formData.verifyResponse.isSuccess==1">
    {{formData.verifyResponse.data}} 
    you're new username is {{formData.verifyResponse.username}}
    </div>
    <div v-else>
    {{formData.verifyResponse.errorMessage}}
    </div>
    </div>
    
    

</template>
<script>
    export default {
        name: 'VerifyAccount',
        created(){
            this.ViewDrug()
        },
        data() {
        return {
            userid:this.$route.params.id,
            token:this.$route.params.token,
            formData :{
                verifyResponse: []
            },
            
        }
    },
    methods:{
        ViewDrug(){

            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json"},

            };
                fetch(process.env.VUE_APP_BACKEND+'Account/VerifyAccount?Code='+this.token+'&Username='+this.userid,requestOptions)
                .then(response => response.json())
                .then(body => this.formData.verifyResponse= body)
        },
            
    }
}
    
</script>
<style scoped>
    .form {
        width: 100%;
        margin: 0 auto
    }
    .description{
        font-size: 9px;
        color:grey
    }
</style>
