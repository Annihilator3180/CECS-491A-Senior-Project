<template>
<div v-if="!sessionStorage">
  <nav>
    
    <router-link to="/">Home</router-link> |
    <router-link :to="{ name: 'login' }">login</router-link> |
    <router-link :to="{ name: 'otp' }">otp</router-link> |
    <router-link :to="{ name: 'AccountRecovery' }">Account Recovery</router-link>|
    <router-link :to="{ name: 'Registration' }"> Registration</router-link> |
    </nav>
    <router-view />
    </div>
    <div v-else>
        <nav>
    <router-link to="/">Home</router-link> |
    <router-link :to="{ name: 'UM' }">User Management</router-link> |
    <router-link :to="{ name: 'WeightManagement' }">Weight Management</router-link>|
    <router-link :to="{ name: 'ResetPassword' }">ResetPassword</router-link>|

    <router-link :to="{ name: 'MedSearch' }"> MedSearch</router-link>|
    <router-link :to="{ name: 'HotTopics' }"> HotTopics</router-link>|
    <router-link :to="{name: 'SearchFood'}"> SearchFood</router-link>|
    <router-link :to="{name: 'HealthRecorder'}"> HealthRecorder</router-link>|
   <router-link :to="{name: 'FavoriteDrugListPost'}"> Favorite Drugs List </router-link>| 
    <router-link :to="{name: 'getTLogs'}"> TrackingLog</router-link>|
    <router-link :to="{name: 'SearchFood'}"> SearchFood</router-link> |
   <router-link :to="{name: 'FavoriteDrugListPost'}"> Favorite Drugs List </router-link> |
    <router-link :to="{name: 'DietRecommendation'}"> Diet Recommendation</router-link>|
    <router-link :to="{name: 'DeleteAccount'}"> Delete </router-link>|
    <router-link :to="{name: 'viewReminder'}"> ViewReminder </router-link>|
    <router-link :to="{name: 'logout'}"> Log Out </router-link> |
    <router-link :to="{name: 'Analysis Dash'}"> Analysis </router-link>



  </nav>
    <router-view />
        </div>
</template>

<script>
export default {
  mounted(){
    console.log(process.env.VUE_APP_NOT_SECRET_CODE)
    console.log(sessionStorage.getItem('token'))
  },
  data(){
    return{
    sessionStorage:sessionStorage.getItem('token')
    }
  },
  name: "app",
  components: {},
};
</script>
