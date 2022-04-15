<template>
    <h1>
        Health Recorder
    </h1>
    <form action="https://localhost:7011/HealthRecorder/CreateRecord/" method="POST" enctype="multipart/form-data">
        <div>
            <label for = "userFileInput"> Please select up to 2 .pdf or .jpeg health files: </label>
            <input type = "text" name = "recordName" id = "recordName" placeholder="Enter record name" minlength = "2" required>
        </div>
        <div>
            <select name = "categoryName" id="categoryName" >
                <option value = "Medical"> Medical Record </option>
                <option value = "Other"> Other </option>
            </select>
        </div>
        <div>
<!--   The accept attribute doesn't validate the types of the selected files; 
it provides hints for browsers to guide users towards selecting the correct file types.
 It is still possible (in most cases) for users to toggle an option in the file chooser
  that makes it possible to override this and select any file they wish, and then choose incorrect file types. -->
            <input type = "file" id = "file" name = "file" required accept=".jpg,.jpg,.pdf">
        </div>
        <div>
            <input type = "file" id = "file2" name = "file2" accept=".jpg,.jpg,.pdf">  
        </div>
        <div>
            <input type = "submit" value="submit">
        </div>
    </form>
</template>


<script>
//validation as a whole and validation as individual, try to reuse the same
// mozilla -> web.dev
//multipart mime type
export default{
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
            this.onSubmit();  
        },
        ProcessFile(event){
            this.file = event.target.files[0];
        },
        }

    }
</script>






