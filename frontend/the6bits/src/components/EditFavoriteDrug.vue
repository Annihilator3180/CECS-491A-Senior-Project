<template>
    <div class="form">
        <div v-if="formData.drugInfoResponse.isSuccess==true">
        <div v-if="formData.drugInfoResponse.data?.isFavorited==true">
        {{drugName}}
            <div>
                
                <label for="price">Enter a price </label>
                <input type="number" id="price" v-model="formData.lowestprice"/>
            </div>
            <div className="description">
                    <p> price must not be negative or greater than 9 million.
                        Lowest previous price: {{formData.drugInfoResponse.data.favoriteDrug.lowestprice}}</p>
            </div>
            <div>
            <label for="Enter location of price">Location of price </label>
                <input type="text" id="price" v-model="formData.lowestpriceLocation"/>
            </div>
            <div className="description">
            <p> Must be under 150 characters  Previous Location: {{formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation}}</p>
                
            </div>
            <div>
            <label for="Enter a description">Description </label>
           <textarea v-model="formData.description"/>
            </div>
            <div className="description">
            <p> Must be under 500 characters  Previous Description: {{formData.drugInfoResponse.data.favoriteDrug.description}}</p>
                
            </div>
            <div>
            <button @click = "UpdateDrug()">Update Favorite</button> {{formData.message}}
            </div>
            <div>
            Enter a Reminder date

                
                <label for="date">date</label>
                <input type="date" id="month" min=1 max=31 v-model="formData.date"/>
                {{formData.date}}
                <label   for="privOption">Repeat Reminder</label>
                <select v-model ="formData.frequency">
                        <option value="none">---</option>
                        <option value="daily">Daily</option>
                        <option value="weekly">Weekly</option>
                        <option value="monthly">Monthly</option>
                </select>
                <button @click = "CreateReminder()">Create Reminder</button> {{formData.reminderMessage}}
                Adding a reminder does not affect previous reminders

            </div>
        </div>
            <div v-else>
                drug not favorite
            </div>
            </div>
        <div v-else>
            {{formData.drugInfoResponse.error}}
        </div>
    </div>

    
    

</template>
<script>
    export default {
        name: 'EditFavoriteDrug',
        created(){
            this.ViewDrug()
        },
        data() {
        return {
            drugName:this.$route.params.id,
            formData :{
                drugInfoResponse: [],
                lowestprice: null,
                lowestpriceLocation: null,
                message:"",
                reminderMessage:"",
                month:0,
                date:"",
                frequency: "none",
                description: ""
            },
            
        }
    },
    methods:{
        ViewDrug(){

            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json",
                "Authorization" :`Bearer ${sessionStorage.getItem('token')}`},
                body: JSON.stringify({generic_name : this.drugName })

            };
                fetch(process.env.VUE_APP_BACKEND+'Medication/viewDrug?brand_name='+this.drugName,requestOptions)
                .then(response => response.text())
                .then(body => this.formData.drugInfoResponse= JSON.parse(body))
        },
        UpdateDrug(){
            if(!this.formData.lowestprice){
               this.formData.lowestprice= this.formData.drugInfoResponse.data.favoriteDrug.lowestprice
            }
            if(!this.formData.lowestpriceLocation){
               this.formData.lowestpriceLocation= this.formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation
            }
            if(!this.formData.description){
               this.formData.description= this.formData.drugInfoResponse.data.favoriteDrug.description
            }
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json",
                "Authorization" :`Bearer ${sessionStorage.getItem('token')}`},
                body: JSON.stringify({
                    "generic_name": this.formData.drugInfoResponse.data.favoriteDrug.generic_name,
                    "product_id": this.formData.drugInfoResponse.data.favoriteDrug.product_id,
                    "brand_name": this.formData.drugInfoResponse.data.favoriteDrug.brand_name,
                    "lowestprice": this.formData.lowestprice,
                    "lowestPriceLocation": this.formData.lowestpriceLocation,
                    "description": this.formData.description
                })

            };
            fetch(process.env.VUE_APP_BACKEND+'Medication/UpdateFavorite/',requestOptions)
                .then(response => response.text())
                .then(body => this.formData.message=body)
                .catch(()=>{
                    this.formData.message="Error updating"
                });
        },
        CreateReminder(){
            if(!this.formData.frequency){
                this.formData.frequency="none"
            }
            if(!this.formData.lowestprice){
               this.formData.lowestprice= this.formData.drugInfoResponse.data.favoriteDrug.lowestprice
            }
            if(!this.formData.lowestpriceLocation){
               this.formData.lowestpriceLocation= this.formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation
            }
            const requestOptions = {
                method: "post",
                credentials: 'include',
                headers: { "Content-Type": "application/json",
                "Authorization" :`Bearer ${sessionStorage.getItem('token')}`}

            };
            fetch(process.env.VUE_APP_BACKEND+'Medication/Reminder?name='+ this.drugName+ '&description='+
            this.formData.drugInfoResponse.data.favoriteDrug.lowestprice+"."+
            this.formData.drugInfoResponse.data.favoriteDrug.lowestPriceLocation+'&date='+this.formData.date+
            '&time=0:0:0'+'&repeat='+this.formData.frequency
            ,requestOptions)
                .then(response => response.text())
                .then(body => this.formData.reminderMessage=body)
                .catch(()=>{
                    this.formData.reminderMessage="Error updating"
                });
        }                   
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
