<template>
  <div class="container">
    <div class="large-12 medium-12 small-12 cell">
                <div>
            <label for = "userFileInput"> Please select up to 2 .pdf or .jpeg health files:
            </label>
            <input type = "text" name = "recordName" id = "recordName" placeholder="Enter record name" minlength = "2" required v-model="newRecordName" >
        </div>
        <div>
            <select name = "categoryName" id="categoryName" v-model="categoryName" >
                <option value = "Medical"> Medical Record </option>
                <option value = "Other"> Other </option>
            </select>
        </div>
      <label>Files
        <input type="file" id="files" ref="files"  multiple v-on:change="handleFilesUpload()" required />
      </label>
      <button v-on:click="submitFiles()">Submit</button>
    </div>
  </div>
</template>

<script>

export default {
    name: 'EditMedicalRecords',
    data(){
       return{
           oldRecordName : null,
           newRecordName: null,
           categoryName:null,
           files:[null],
           oldRecord:[]
       }
   },
   methods:{
   LoadMedicalRecord(lastRecordIndex){
           this.GetRecord(lastRecordIndex).then(value =>{
           this.MedicalRecords = value.records;
           console.log(value);
       })
       
   },
   handleFilesUpload(){
        //references the file object based on input from html
        this.files = this.$refs.files.files;
      },
      submitFiles(){
        
        /*
          Initialize the form data
        */
        let formData = new FormData();
        

        /*
          Iteate over any file sent over appending the files
          to the form data.
        */
        let file1 = this.files[0]
        let file2 = this.files[1]
        let type = ""
        var blob1;
        var blob2;

        if (file1 == null){
            let fileString = this.oldRecord.record1;
            let firstFive = fileString.substring(0,5)

            if (firstFive == "JVBER"){
                type = "application/pdf"
            }
            if(firstFive == "/9j/4"){
                type = "image/jpeg"
            }
            blob1 = new Blob([this.base64ToArrayBuffer(fileString)], {type: type})
        }
        if (file2 == null){
            let fileString = this.oldRecord.record2;
            let firstFive = fileString.substring(0,5)

            if (firstFive == "JVBER"){
                type = "application/pdf"
            }
            else if(firstFive == "/9j/4"){
                type = "image/jpeg"
            }
            blob2 = new Blob([this.base64ToArrayBuffer(fileString)], {type: type})
        }


        
       
         formData.append('newRecordName', (this.newRecordName??this.oldRecord.recordName))
        formData.append('file', file1??blob1);
         formData.append('file2', file2??blob2)
        formData.append('oldRecordName', this.oldRecordName)
        formData.append('categoryName', this.categoryName??this.oldRecord.categoryName)
        
        fetch(process.env.VUE_APP_BACKEND + 'HealthRecorder/EditRecord', 
                {
                    method: 'POST',
                    credentials: 'include',
                    
                    body: formData
                })
                .then(response => response.json()) 
                .then (data =>{
                  if (data.errorMessage != null){
                    window.alert(data.errorMessage)
                  }
                  else{
                    window.alert(data.data)
                  }
                  this.$router.push('/ViewMedicalRecords')
                
                })
                
       
      },
      base64ToArrayBuffer(base64) {
        //base64 to binary string
    var binaryString = window.atob(base64);
    var binaryLen = binaryString.length;
    //new array with binary string length
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
      //loops through binary string and gets/places each ascii value of index into new byte array
       var ascii = binaryString.charCodeAt(i);
       bytes[i] = ascii;
    }
    return bytes;
 }

},

beforeMount(){
        var record = localStorage.getItem('editRecord')
        this.oldRecord = JSON.parse(record)
        this.oldRecordName = this.oldRecord.recordName
        console.log(this.oldRecord)  

}
}
</script>
