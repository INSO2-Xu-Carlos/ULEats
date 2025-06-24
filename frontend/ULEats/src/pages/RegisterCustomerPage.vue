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

      const payload = {
        UserId: Number(localStorage.getItem("user_id")),
        Address: this.address,
      };

      try {
        const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
        const response = await fetch(`${baseUrl}/Customer`, {
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
          localStorage.setItem("customer_id", data.customerId);
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
  margin: 40px auto;
  padding: 32px 24px;
  text-align: center;
  background: #f6f8fa;
  border-radius: 16px;
  box-shadow: 0 2px 12px rgba(25, 118, 210, 0.08);
}

.form-group {
  margin-bottom: 18px;
  text-align: left;
}

label {
  display: block;
  margin-bottom: 7px;
  color: #1976d2;
  font-weight: 600;
  text-align: left;
}

input[type="text"] {
  width: 100%;
  padding: 10px;
  box-sizing: border-box;
  border: 1px solid #bdbdbd;
  border-radius: 6px;
  background: #fff;
  font-size: 1rem;
  margin-bottom: 4px;
  color: #111;
}

input[type="text"]:focus {
  border-color: #1976d2;
  outline: none;
}

button {
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
  background: linear-gradient(90deg, #43e97b 0%, #38f9d7 100%);
  color: #fff;
  border: none;
  border-radius: 8px;
  font-weight: 600;
  margin-top: 10px;
  transition: background 0.2s, box-shadow 0.2s;
  box-shadow: 0 2px 8px rgba(67, 233, 123, 0.08);
}

button:hover {
  background: linear-gradient(90deg, #38f9d7 0%, #43e97b 100%);
  box-shadow: 0 4px 16px rgba(67, 233, 123, 0.16);
}
</style>