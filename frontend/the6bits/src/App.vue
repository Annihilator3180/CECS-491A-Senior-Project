<template>
  <nav>
    <router-link to="/">Home</router-link> |
    <router-link :to="{ name: 'login' }">login</router-link> |
    <router-link :to="{ name: 'otp' }">otp</router-link> |
    <router-link :to="{ name: 'UM' }">User Management</router-link> |
    <router-link :to="{ name: 'WeightManagement' }">Weight Management</router-link>|
    <router-link :to="{ name: 'ResetPassword' }">ResetPassword</router-link>|
    <router-link :to="{ name: 'AccountRecovery' }">Account Recovery</router-link>|
    <router-link :to="{ name: 'Registration' }"> Registration</router-link> |
    <router-link :to="{ name: 'MedSearch' }"> MedSearch</router-link>|
    <router-link :to="{ name: 'HotTopics' }"> HotTopics</router-link>|
    <router-link :to="{ name: 'CreateRecords' }"> CreateRecords</router-link>

  </nav>
  <router-view />
</template>

<script>
export default {
  name: "app",
  components: {},
};
</script>
