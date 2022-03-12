<template>
    <LoginPost />
    <OTPPost />
    <account-recovery />
    <DeleteAccount />
    <DietRecommendation />
    <router-link :to="{name: 'Diet Recommendation'}"> DietRecommendation</router-link>|
    <router-link :to="{name: 'Delete'}"> DeleteAccount</router-link>



</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'
    import DeleteAccount from './components/DeleteAccount.vue'
    import DietRecommendation from './components/DietRecommendation.vue'



export default {
  name: 'app',
  components: {
   LoginPost ,
    OTPPost,
    AccountRecovery,
      DeleteAccount,
      DietRecommendation,

  },
}
</script>

