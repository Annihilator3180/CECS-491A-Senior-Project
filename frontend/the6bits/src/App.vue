<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>
    <RegistrationGet/>
    <medSearch/>

</template>

<script>
import LoginPost from './components/LoginPost.vue'
import OTPPost from './components/OneTimePass.vue'
import AccountRecovery from './components/AccountRecovery.vue'
import RegistrationGet from './components/Registration.vue'
import MedSearch from './components/MedicationSearch.vue'



export default {
  name: 'app',
  components: {
    LoginPost ,
    OTPPost,
   AccountRecovery,
    RegistrationGet,
    MedSearch
  },
}
</script>

