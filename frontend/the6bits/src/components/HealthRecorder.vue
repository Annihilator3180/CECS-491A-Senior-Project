<template>
    <div class = 'form'>
    <h1>
        Health Recorder
    </h1>
    <form name = "xx">
        <label for = "userFileInput"> Please select up to 2 .pdf or .jpeg health files: </label>
        <input type = "text" id = "recordName" name = "recordName" placeholder="Enter record name" minlength = "2" required>
        <select name = "categoryName" id = "categoryName" >
            <option value = "Medcial"> Medical Record </option>
            <option value = "Other"> Other </option>
        </select>
        <input type = "file" id = "file" name = "file" required >
        <input type = "file" id = "file2" name = "file2" >  
        <button @click="Validation"> Submit </button>
    </form>

    </div>  
</template>

<script>
export default{
    data(){
    return {
    formData: {
        recordName : '',
        categoryName : '',
        file : '',
        file2 : '',

         },
     }
    },
        methods:{
        Validation(){
            var fileName = document.querySelector('#file').value;
            var fileName2 = document.querySelector('#file2').value
            if (fileName2 != ""){
                var extension2 = fileName2.split('.').pop();
                if ((extension2 != "pdf" && extension2 != "jpg" && extension2 != "jpeg")) {
                alert("Invalid file 2 type!");
            }
            }
            var extension = fileName.split('.').pop();

            if ((extension != "pdf" && extension != "jpg" && extension != "jpeg")) {
                alert("Invalid file type!");
            }
            //this.OnSubmit();
        
        },
        }

    }
</script>






