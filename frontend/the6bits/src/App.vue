<template>
<div v-if="!sessionStorage">
  <nav>
    
    <router-link to="/">Home</router-link> |
    <router-link :to="{ name: 'login' }">login</router-link> |
    <router-link :to="{ name: 'AccountRecovery' }">Account Recovery</router-link>|
    <router-link :to="{ name: 'Registration' }"> Registration</router-link> |
    <router-link :to="{name: 'FAQ'}">FAQ</router-link>

    </nav>
    <router-view />
    </div>
    <div v-else>
        <nav>
           <router-link to="/">Home</router-link> |
            <div v-if="isAdmin==1">
                <router-link :to="{name: 'Analysis Dash'}"> Analysis </router-link>|
                <router-link :to="{ name: 'UM' }">User Management</router-link> 
            </div>

            

            <router-link :to="{ name: 'WeightManagement' }">Weight Management</router-link>|
            <router-link :to="{ name: 'MedSearch' }"> MedSearch</router-link>|
            <router-link :to="{ name: 'HotTopics' }"> HotTopics</router-link>|
            <router-link :to="{name: 'FavoriteDrugListPost'}"> Favorite Drugs List </router-link>|
            <router-link :to="{name: 'DietRecommendation'}"> Diet Recommendation</router-link>|
            <router-link :to="{name: 'DeleteAccount'}"> Delete </router-link>|
            <router-link :to="{name: 'CreateReminder'}"> CreateReminder </router-link>|
            <router-link :to="{name: 'viewReminder'}"> ViewReminder </router-link>|
            <router-link :to="{name: 'EditReminder'}"> EditReminder </router-link> |
            <router-link :to="{name: 'DeleteReminder'}"> DeleteReminder </router-link> |
            <router-link :to="{name: 'FoodLog'}"> FoodLog </router-link>|
            <router-link :to="{name: 'HealthRecorderHome'}"> HealthRecorderHome </router-link> |
            <router-link :to="{name: 'NutritionAnalysis'}"> Nutrition Analysis</router-link>|
            <router-link :to="{name: 'RecipeFavorite'}"> RecipeFavorite </router-link>|
            <router-link :to="{name: 'BMICalculator'}"> BMICalculator </router-link>|
            <router-link :to="{name: 'FAQ'}">FAQ</router-link>
             <router-link :to="{name: 'logout'}"> Log Out </router-link>|




        </nav>
    <router-view />
        </div>
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
  data(){
    return{
    sessionStorage:sessionStorage.getItem('token'),
    isAdmin:sessionStorage.getItem('IsAdmin')
    }
  },
};

</script>
