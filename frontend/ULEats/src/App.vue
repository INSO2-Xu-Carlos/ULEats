<template>
  <div id="app">
    <h1>ULEats</h1>
    <LoginButton @click="goToLogin" />
    <RegisterButton @click="goToRegister" />
  </div>
</template>

<script>
import LoginButton from './components/LoginButton.vue';
import RegisterButton from './components/RegisterButton.vue';

export default {
  components: {
    LoginButton,
    RegisterButton,
  },
  methods: {
    goToLogin() {
      this.$router.push('/login');
    },
    goToRegister() {
      this.$router.push('/register');
    },
  },
};
</script>

<style>
#app {
  text-align: center;
  margin-top: 50px;
}
</style>