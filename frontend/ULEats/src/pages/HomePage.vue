<template>
  <div class="home-page">
    <h1>Welcome to ULEats</h1>
    <router-link to="/login">
      <button>Log In</button>
    </router-link>
    <router-link to="/register">
      <button>Register</button>
    </router-link>
  </div>
</template>

<script>
export default {
  name: 'HomePage',
};
</script>

<style scoped>
.home-page {
  text-align: center;
  margin-top: 50px;
}
button {
  margin: 10px;
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
}
</style>