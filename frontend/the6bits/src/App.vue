<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>
    <UM/>
    <CreateGoal/>
</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'
import UM from './components/UM.vue'
import CreateGoal from './components/WeightManagement/CreateWeightGoal.vue'

export default {
  name: 'app',
  components: {
    LoginPost ,
    OTPPost,
    AccountRecovery,
    UM,
    CreateGoal,
  },
}
</script>

