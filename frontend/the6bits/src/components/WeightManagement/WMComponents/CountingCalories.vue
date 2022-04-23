<template>
    <div v-if="Profile!=''">
        Recommended Daily Calories : {{ Profile.calorieRecommendation }} |
        This Week's Caloric Intake : {{ Profile.weekCaloriesEaten }} |
        Today's Caloric Intake : {{ Profile.todayCaloriesEaten }} | 
        Calories Left :  {{ Profile.caloriesCompared }} |
        Average Daily Calories This Week : {{ Profile.dailyAverageThisWeekCalories }}
    </div> 
            <button v-on:click="LoadProfile">Calculate Info</button>
</template>
<script>
    export default {
        name : 'LoadProfile',
        data() {
        return {
            Profile : '',
        }
    },
    methods:{
        LoadProfile(){
            const requestOptions = {
                method: "GET",
                credentials: 'include',
                headers: { 
                    "Authorization" : `Bearer ${sessionStorage.getItem('token')}`,
                    "Content-Type": "application/json"},
            };
            fetch(process.env.VUE_APP_BACKEND+'WeightManagement/GetProfileInfo',requestOptions)
                .then(response =>  response.text())
                .then(body => this.Profile = JSON.parse(body))
        },
    },
        
    }
</script>