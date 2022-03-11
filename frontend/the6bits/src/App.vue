<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>
    <UM/>
    <CreateGoal/>
    <bulkActions/>
</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'
import UM from './components/UM.vue'
import CreateGoal from './components/WeightManagement/CreateWeightGoal.vue'
import bulkActions from './components/BulkUM/bulkActions.vue'

export default {
  name: 'app',
  components: {
    LoginPost ,
    OTPPost,
    AccountRecovery,
    UM,
    CreateGoal,
    bulkActions,
  },
}
</script>

