<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>
    <reset-password/>

</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'
import ResetPassword from './components/ResetPassword.vue'



export default {
  name: 'app',
  components: {
    LoginPost ,
    OTPPost,
    AccountRecovery,
    ResetPassword,
  },
}
</script>

