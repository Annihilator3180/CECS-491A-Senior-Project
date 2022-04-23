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
   <router-link  :to="{name: 'FavoriteDrugListPost'}"> Favorite Drugs List </router-link>| 
    <router-link :to="{name: 'getTLogs'}"> TrackingLog</router-link>|
    <router-link :to="{name: 'SearchFood'}"> SearchFood</router-link> |
   <router-link  :to="{name: 'FavoriteDrugListPost'}"> Favorite Drugs List </router-link> |
    <router-link :to="{name: 'DietRecommendation'}"> Diet Recommendation</router-link>|
    <router-link :to="{name: 'DeleteAccount'}"> Delete </router-link>|
    <router-link :to="{name: 'viewReminder'}"> ViewReminder </router-link>|
    <router-link :to="{name: 'FoodLog'}"> FoodLog </router-link>|
    <router-link :to="{name: 'logout'}"> Log Out </router-link>|
    <router-link :to ="{name: 'HealthRecorderHome'}"> HealthRecorderHome </router-link> |
    <router-link :to="{name: 'RecipeFavorite'}"> RecipeFavorite </router-link>


    

    </nav>
    <router-view />
</template>

<script>
import router from './router/index'
export default {
  name: "app",
  components: {},
  //CHECKS EVERY 1000 MS REROUTS TO LOGIN IF NOT LOGGED IN
  mounted: function () {
  window.setInterval(() => {
    if(router.currentRoute.value.meta.requiresAuth==true && sessionStorage.getItem('token') == null){
          router.push("login")
    }
  }, 1000)
},
};

</script>
