<template>
  <LoginPost />
  <OTPPost />
  <account-recovery />
  <view-hot-topics />
</template>

<script>
import LoginPost from "./components/LoginPost.vue";
import OTPPost from "./components/OneTimePass.vue";
import AccountRecovery from "./components/AccountRecovery.vue";
import ViewHotTopics from "./components/viewHotTopics.vue";

export default {
  name: "app",
  components: {
    LoginPost,
    OTPPost,
    AccountRecovery,
    ViewHotTopics,
  },
};
</script>
