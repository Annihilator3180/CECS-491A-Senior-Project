<template>
  <nav>
    <router-link to="/">Home</router-link> |
    <router-link :to="{ name: 'login' }">login</router-link> |
    <router-link :to="{ name: 'otp' }">otp</router-link> |
    <router-link :to="{ name: 'UM' }">User Management</router-link> |
    <router-link :to="{ name: 'WeightManagement' }">Weight Management</router-link>|
    <router-link :to="{ name: 'ResetPassword' }">ResetPassword</router-link>|
    <router-link :to="{ name: 'AccountRecovery' }">Account Recovery</router-link>|
    <router-link :to="{ name: 'Registration' }"> Registration</router-link> |
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
    <router-link :to="{name: 'ViewReminder'}"> ViewReminder </router-link>

  </nav>
    <router-view />
</template>

<script>
export default {
  mounted(){
    console.log(process.env.VUE_APP_NOT_SECRET_CODE)
  },
  name: "app",
  components: {},
};
</script>
