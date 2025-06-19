<template>
  <button @click="logout" class="logout-button">
    Cerrar sesión
  </button>
</template>

<script>
export default {
  name: "LogoutButton",
  methods: {
    logout() {
      localStorage.clear();
      this.$router.push('/');
    }
  }
};
</script>

<style scoped>
.logout-button {
  padding: 10px 20px;
  background: #e53935;
  color: #fff;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 16px;
  margin: 10px;
}
.logout-button:hover {
  background: #b71c1c;
}
</style>