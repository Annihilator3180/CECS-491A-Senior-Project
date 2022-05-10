<template>
    <div  >
            <button v-on:click="GetFirstImage()">ViewImage</button>
                <img width="100" height="200">
            <button v-on:click="IterateImage(-1)">Previous</button>
            <button v-on:click="IterateImage(1)">Next</button>
            <button v-on:click="DeleteImage(this.ImageIds[this.position])">Delete</button>

    </div> 
</template>




<script>
    import { GetAllFoodLogs }  from '@/components/WeightManagement/WeightManagement'
    export default {
        name : 'LoadWeightImages',
        data() {
        return {
            ImageIds : [],
            DisplayImages : '',
            Images : [],
            position:0,
        }
    },
    methods:{
        GetAllFoodLogs,
        LoadImageIds(){
           const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
            };
            fetch(process.env.VUE_APP_BACKEND+'WeightManagement/GetAllImageIds',requestOptions)
                .then(response =>  response.text())
                .then(body => this.ImageIds = JSON.parse(body))
        },
        GetImage(id){
           const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
            };
            fetch(process.env.VUE_APP_BACKEND+'WeightManagement/GetImage?index='+parseInt(id),requestOptions)
                .then(response => {
                response.blob().then(blobResponse => {
                    var objectURL = URL.createObjectURL(blobResponse);
                    var myImage = document.querySelector('img');
                    myImage.src = objectURL;
                })
                });


        },
        GetFirstImage(){
            this.LoadImageIds();
            if (this.ImageIds.length != 0){
                this.GetImage(this.ImageIds[this.position])
            }
            else{
                    var myImage = document.querySelector('img');
                    myImage.src = "";
            }
        },
        IterateImage(iter){
        if(this.position + iter < this.ImageIds.length && this.position + iter >= 0){
            this.position += iter
            this.GetImage(this.ImageIds[this.position])
        }

        },
        DeleteImage(id){
           const requestOptions = {
                method: "POST",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
            };
            fetch(process.env.VUE_APP_BACKEND+'WeightManagement/DeleteImage?index='+parseInt(id),requestOptions)
                .then(response =>  response.text())
                .then(body => console.log(body))
                .then(() => {
                                this.LoadImageIds();
                                this.position = 0;
                })


        },
    },
    beforeMount(){
        this.LoadImageIds();
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