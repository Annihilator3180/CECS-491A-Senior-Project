<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>
    <UM/>
</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'
import UM from './components/UM.vue'

export default {
  name: 'app',
  components: {
    LoginPost ,
    OTPPost,
    AccountRecovery,
    UM,
  },
}
</script>

