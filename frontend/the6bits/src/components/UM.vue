<template>
    <LoginPost/>
    <OTPPost/>
    <account-recovery/>
    <UMUserObj/>
    <UMUsername/>

</template>

<script>
import UMUserObj from './components/UMComponents/UMUserObj.vue'
import UMUsername from './components/UMComponents/UMUsername.vue'

export default {
  name: 'app',
  components: {
    UMUserObj,
    UMUsername,
  },
}
</script>

