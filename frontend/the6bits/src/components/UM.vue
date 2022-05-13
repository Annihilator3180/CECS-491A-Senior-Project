<template>
    <UMUserObj/>
    <UMUsername/>
    <div class="bottomleft" >
        <label>Files
          <input type="file" id="file" ref="file" multiple v-on:change="handleFileUpload()" accept=".txt" />
        </label>
        <button v-on:click="submitFiles()">Submit</button>
        {{message}}
    </div> 
</template>

<script>
import UMUserObj from './UMComponents/UMUserObj.vue'
import UMUsername from './UMComponents/UMUsername.vue'

export default {
  name: 'UM',
  created(){
    window.setInterval(()=>{this.timer+=1}, 1000)
  },
  beforeUnmount(){
    this.TimerTime(),
    this.timer=0
  },
  data() {
        return {
            file: '',
            FoodLogs : [],
            message :'',
            timer:0
        }
    },
  components: {
    UMUserObj,
    UMUsername,
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

        fetch(process.env.VUE_APP_BACKEND + 'UM/UploadFile', 
                {
                    method: 'POST',
                    credentials: 'include',
                    headers:{
                      "Authorization" : `Bearer ${sessionStorage.getItem('token')}`
                    },
                    
                    body: formData
                })
                .then(response =>  response.text())
                .then(body => {this.message = body, console.log(body)})
                
       
      },
        handleFileUpload(){
        //references the file object based on input from html
        this.file = this.$refs.file.files[0];
      },
      TimerTime(){
        const requestOptions = {
          method:"post",
          headers: {"Content-Type": "application/json"},
        };
        fetch(process.env.VUE_APP_BACKEND+'Account/ViewTime?time='+this.timer+'&view=UM', requestOptions)
      }
  }
}
</script>

