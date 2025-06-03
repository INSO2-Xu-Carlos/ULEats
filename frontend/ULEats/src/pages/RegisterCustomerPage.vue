<template>
  <div class="register-customer-page">
    <h1>Registro de Cliente - Dirección</h1>
    <form @submit.prevent="submitAddress">
      <div class="form-group">
        <label for="street">Calle:</label>
        <input type="text" id="street" v-model="street" required />
      </div>
      <div class="form-group">
        <label for="city">Ciudad:</label>
        <input type="text" id="city" v-model="city" required />
      </div>
      <div class="form-group">
        <label for="postalCode">Código Postal:</label>
        <input type="text" id="postalCode" v-model="postalCode" required />
      </div>
      <div class="form-group">
        <label for="country">País:</label>
        <input type="text" id="country" v-model="country" required />
      </div>
      <button type="submit">Guardar dirección</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "RegisterCustomerPage",
  data() {
    return {
      street: "",
      city: "",
      postalCode: "",
      country: "",
    };
  },
  methods: {
    submitAddress() {
      // Aquí puedes enviar los datos al backend o continuar el flujo de registro
      alert(
        `Dirección guardada:\n${this.street}, ${this.city}, ${this.postalCode}, ${this.country}`
      );
      // Ejemplo: this.$router.push('/client');
    },
  },
};
</script>

<style scoped>
.register-customer-page {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  text-align: center;
}
.form-group {
  margin-bottom: 15px;
  text-align: left;
}
label {
  display: block;
  margin-bottom: 5px;
}
input[type="text"] {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
button {
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
}
</style>