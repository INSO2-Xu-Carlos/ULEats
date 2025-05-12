<template>
  <button @click="handleLogin">Log In</button>
</template>

<script>
export default {
  methods: {
    handleLogin() {
      // Aquí puedes manejar la lógica de inicio de sesión
      console.log("Login sucessfull");
    }
  }
}
</script>

<style scoped>
button {
  padding: 10px 20px;
  margin: 10px;
  font-size: 16px;
}
</style>