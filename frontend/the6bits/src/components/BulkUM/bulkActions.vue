<template>
    <div style="border-style:solid">
        <H1>Bulk File Upload</H1>
      <input type="file" ref="doc" @change="readFile()" />
      <div>{{content}}</div>
    </div>
</template>

<script>
  import test from '../UMComponents/UMFunctions'
    
    export default {
        name : 'bulkActions',
        data() {
        return {
            file: null, content: null
        }
    },
    methods:{
            readFile() {
      this.file = this.$refs.doc.files[0];
      const reader = new FileReader();
      if (this.file.name.includes(".txt")) {
        reader.onload = (res) => {
          this.content = res.target.result;
          
        const requestOptions = {
                method: "POST",
                credentials: 'include',
                body:  this.content
            };
            fetch(process.env.VUE_APP_BACKEND+'UM/UploadFiles' ,requestOptions)
                .then(response => console.log(response))


        };

        
        reader.onerror = (err) => console.log(err);
        reader.readAsText(this.file);
      } 
      
      
      
      
      
      else {
        this.content = "check the console for file output";
        reader.onload = (res) => {
          console.log(res.target.result);
        };
        reader.onerror = (err) => console.log(err);
        reader.readAsText(this.file);
      }
    }
    }
}
</script>

<style scoped>   
.form { width:300px; margin:0 auto }
label {
    /* Other styling... */
    text-align: right;
    clear: both;
    float:left;
    margin-right:15px;
}
</style>