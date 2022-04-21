
<template>
    <div >
        <label for="site-search">Search for a record:</label>
                <input type="search" id="site-search" name="q">

                <button @click="SearchMedicalRecord()">Search</button>

            <li v-for="RecordComponent in MedicalRecords" :key="RecordComponent">
              {{ RecordComponent.categoryName }} | {{RecordComponent.recordName}}
              <button @click="ExportMedicalRecord(RecordComponent.recordName, RecordComponent.categoryName, '1')"> Export Record #1 </button>
                <button @click="ExportMedicalRecord(RecordComponent.recordName, RecordComponent.categoryName, '2')"> Export Record #2</button>

               <button @click="DeleteMedicalRecord(RecordComponent.recordName, RecordComponent.categoryName)"> Delete </button>
               <button @click="EditMedicalRecord(RecordComponent)"> Edit </button>


            </li>
        <button id="1" @click="click(0)">1</button>
        <button id="2" @click="click(1 * 100)">2</button>
        <button id="3" @click="click(2 * 100)">3</button>
        <button id="4" @click="click(3 * 100)">4</button>
        <button id="5" @click="click(4 * 100)">5</button>
        <button id="6" @click="click(5 * 100)">6</button>
        <button id="7" @click="click(6 * 100)">7</button>
        <button id="8" @click="click(7 * 100)">8</button>
        <button id="9" @click="click(8 * 100)">9</button>
        <button id="10" @click="click(9 * 100)">10</button>

         
    </div> 
 
</template>
<script>

import { GetRecord, ExportRecord, DeleteRecord, SearchRecord } from '@/components/HealthRecorder/HealthRecorder'

export default {
   name: 'ViewMedicalRecords',
   data(){
       return{
           MedicalRecords: [],
           ExportMessage: [],
           DeleteMessage:[],
       }
   },
   methods:{
       GetRecord,
       ExportRecord,
       DeleteRecord,
       SearchRecord,
       LoadMedicalRecord(lastRecordIndex){
           this.GetRecord(lastRecordIndex).then(value =>{
           this.MedicalRecords = value.records;
           console.log(value);
       })
       
   },
    ExportMedicalRecord(recordName, categoryName, recordNumber){
        this.ExportRecord(recordName, categoryName, recordNumber).then(value =>{
           this.ExportMessage = value.records;
        if (value.errorMessage != null){
            window.alert(value.errorMessage)
        }
        else{
            let recordString = value.file
            let firstFive = recordString.substring(0,5)
            let type = ""

            if (firstFive == "JVBER"){
                type = "application/pdf"
            }
            else if(firstFive == "/9j/4"){
                type = "image/jpeg"
            }
            //decodes a base 64 string into a new string with a character for each byte of binary data
            const byteCharacters = atob(recordString);
            //create array based on byte characters
            const byteNumbers = new Array(byteCharacters.length);
            for (let i = 0; i < byteCharacters.length; i++) {
                //places the value of each byte into byteNumbers arr
                byteNumbers[i] = byteCharacters.charCodeAt(i);
                }
                //converts to real typed byte array
            const byteArray = new Uint8Array(byteNumbers);
            //create blob object based on aboive array
            const blob = new Blob([byteArray], {type: type});


            const objectUrl = window.URL.createObjectURL(blob)
            window.open(objectUrl)
            

        
        }
        })

 },
    DeleteMedicalRecord(recordName, categoryName){
        let confirmResult = confirm("Are you sure you want to delete " + recordName)
        if (confirmResult){
        this.DeleteRecord(recordName, categoryName).then(value =>{
           this.DeleteMessage = value.records;
           if (value.errorMessage != null){
               window.alert(value.errorMessage)
           }
           
           location.reload()
        })
        }
        else{
            return
        }
    },
    EditMedicalRecord(event){
        localStorage.setItem('editRecord', JSON.stringify(event))
        this.$router.push('/EditMedicalRecords')
    },
   click(pageNumber){
    this.LoadMedicalRecord(pageNumber)
},
SearchMedicalRecord(){
     var request = document.getElementById('site-search').value;
    this.SearchRecord(request, request).then(value=>{
            this.MedicalRecords = value.records
            })
}
   
   },
   beforeMount(){
       this.LoadMedicalRecord(0);
   },

}
</script>
