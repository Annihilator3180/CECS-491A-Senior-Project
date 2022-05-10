<template>
  <div class="container">
    <div class="large-12 medium-12 small-12 cell">
      <div>
      <label for = "userFileInput"> Please select up to 2 .pdf or .jpeg health files:
      </label>
        <input type = "text" name = "recordName" id = "recordName" placeholder="Enter record name" minlength = "2" required v-model="recordName">
      </div>
      <div>
        <select name = "categoryName" id="categoryName"  required v-model="categoryName" >
          <option value = "Medical"> Medical Record </option>
          <option value = "Other"> Other </option>
        </select>
      </div>
      <div>
        <label>Files
          <input type="file" id="files" ref="files" multiple v-on:change="handleFilesUpload()" accept=".pdf,.jpg, .jpeg" />
        </label>
        <button v-on:click="submitFiles()">Submit</button>
      </div>
      </div>
  </div>
</template>

<script>
  export default {
    /*
      Defines the data used by the component
    */
    data(){
      return {
        files: '',
        recordName:'',
        categoryName:'',
      }
    },

    methods: {
    
      /*
        Submits all of the files to the server
      */
      submitFiles(){
        
        /*
          Initialize the form data
        */
        let formData = new FormData();

        /*
          Iteate over any file sent over appending the files
          to the form data.
        */
         let file = this.files[0];
         let file2 = this.files[1];
        

        formData.append('file', file);
         formData.append('file2', file2)
         formData.append('recordName', this.recordName)
        formData.append('categoryName', this.categoryName)
        
        fetch(process.env.VUE_APP_BACKEND + 'HealthRecorder/CreateRecord', 
                {
                    method: 'POST',
                    credentials: 'include',
                    headers:{
                      "Authorization" : `Bearer ${sessionStorage.getItem('token')}`
                    },
                    
                    body: formData
                })
                .then(response => response.json()) 
                .then (data =>{
          
                  if (data.errorMessage != null){
                    window.alert(data.errorMessage)
                  }
                   else if(data.data == undefined){
                    window.alert("Please select a category name, file, and record name")
                  }
                  else{
                    window.alert(data.data)
                  }
                  this.$router.push('/ViewMedicalRecords')
                
                })
                
       
      },

      /*
        Handles a change on the file upload
      */
      handleFilesUpload(){
        //references the file object based on input from html
        this.files = this.$refs.files.files;
      }
    }
  }
</script>