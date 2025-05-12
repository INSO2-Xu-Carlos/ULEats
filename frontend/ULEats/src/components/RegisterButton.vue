<template>
  <button @click="register" class="register-button">
    Register
  </button>
</template>

<script>
export default {
  methods: {
    register() {
      // Logic for registration goes here
      console.log('Register init');
    }
  }
}
</script>

<style scoped>
.register-button {
  padding: 10px 20px;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.register-button:hover {
  background-color: #45a049;
}
</style>