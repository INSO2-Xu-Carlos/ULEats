<template>
  <button @click="goToLogin">Log In</button>
</template>

<script>
export default {
  name: "LoginButton",
  methods: {
    goToLogin() {
      this.$router.push("/login");
      console.log('Log In');
    },
  },
};
</script>

<style scoped>
button {
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
}
</style>