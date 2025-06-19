<template>
  <div class="home-page">
    <LoginButton @click="goToLogin" />
    <RegisterButton @click="goToRegister" />
  </div>
</template>

<script>
import LoginButton from '@/components/LoginButton.vue';
import RegisterButton from '@/components/RegisterButton.vue';
import HomeLayout from '@/layouts/HomeLayout.vue';

export default {
  name: 'HomePage',
  components: {
    HomeLayout,
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