<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>

</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'



export default {
  name: 'app',
  components: {
    LoginPost ,
    OTPPost,
    AccountRecovery,
  },
}
</script>

