<template>
    <div class="bottomleft" >
        <label>Files
          <input type="file" id="file" ref="file" multiple v-on:change="handleFileUpload()" accept=".pdf,.jpg, .jpeg" />
        </label>
        <button v-on:click="submitFiles()">Submit</button>
        {{message}}
    </div> 
</template>




<script>
    export default {
        name : 'UploadProgressImage',
        data() {
        return {
            file: '',
            FoodLogs : [],
            message :''
        }
    },
    methods:{
        submitFiles(){
        
        /*
          Initialize the form data
        */
        let formData = new FormData();
        /*
          Iteate over any file sent over appending the files
          to the form data.
        */
         let file = this.file;
        
        formData.append('file', file);

        fetch(process.env.VUE_APP_BACKEND + 'WeightManagement/SaveImage', 
                {
                    method: 'POST',
                    credentials: 'include',
                    headers:{
                      "Authorization" : `Bearer ${sessionStorage.getItem('token')}`
                    },
                    
                    body: formData
                })
                .then(response =>  response.text())
                .then(body => this.message = body)
                
       
      },
        handleFileUpload(){
        //references the file object based on input from html
        this.file = this.$refs.file.files[0];
      }
    }
        
    }
</script>

<style scoped>   

.bottomleft {
position: absolute;
left:    0;
bottom:   0;
    padding-bottom:200px;

}


</style>