<template>
  <div id="app">
    <h1>ULEats</h1>
    <LoginButton />
    <RegisterButton />
  </div>
</template>

<script>
import LoginButton from './components/LoginButton.vue';
import RegisterButton from './components/RegisterButton.vue';

export default {
  components: {
    LoginButton,
    RegisterButton
  }
}
</script>

<style>
#app {
  text-align: center;
  margin-top: 50px;
}
</style>