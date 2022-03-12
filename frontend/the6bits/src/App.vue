<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>
    <CreateRecords/>

</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'
import CreateRecords from './components/CreateRecords.vue'


export default {
  name: 'app',
  components: {
    LoginPost ,
    OTPPost,
    AccountRecovery,
    CreateRecords,
  },
}
</script>

