<template>
  <div class="home-page">
    <LoginButton @click="goToLogin" />
    <RegisterButton @click="goToRegister" />
  </div>
</template>

<script>
import LoginButton from '@/components/LoginButton.vue';
import RegisterButton from '@/components/RegisterButton.vue';

export default {
  name: 'HomePage',
  components: {
    LoginButton,
    RegisterButton
  },
  methods: {
    goToLogin() {
      this.$router.push('/login');
    },
    goToRegister() {
      this.$router.push('/register');
    }
  }
};
</script>