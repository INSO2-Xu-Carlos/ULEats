<template>
  <div class="register-customer-page">
    <h1>Registro de Cliente</h1>
    <form @submit.prevent="submitAddress">
      <div class="form-group">
        <label for="street">Dirección Completa:</label>
        <input type="text" id="address" v-model="address" required />
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
      address: "",
    };
  },
  methods: {
    async submitAddress() {
      let userId = localStorage.getItem("user_id");

      if (userId) userId = Number(userId);
      const payload = {
        UserId: Number(localStorage.getItem("user_id")),
        Address: this.address,
      };

      try {
        const response = await fetch("/api/Customer", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(payload),
        });

        if (!response.ok) {
          const error = await response.text();
          alert("Error al guardar la dirección: " + error);
          return;
        }

        const data = await response.json(); 
        if (data.userId) {
          localStorage.setItem("user_id", data.userId);
        }

        alert("Dirección guardada correctamente.");
        this.$router.push('/customer');
      } catch (err) {
        alert("Error de conexión con el servidor.");
      }
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