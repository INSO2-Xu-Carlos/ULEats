<template>
  <div class="login-page">
    <h1>Iniciar Sesión</h1>
    <form @submit.prevent="handleLogin">
      <div class="form-group">
        <label for="email">Email</label>
        <input type="email" id="email" v-model="email" required />
      </div>
      <div class="form-group">
        <label for="password">Password:</label>
        <input type="password" id="password" v-model="password" required />
      </div>
      <button type="submit">Log In</button>
    </form>
    <button class="back-button" @click="goHome">Volver a la página principal</button>
  </div>
</template>

<script>
export default {
  name: "LoginPage",
  data() {
    return {
      email: "",
      password: "",
    };
  },
  methods: {
    async handleLogin() {
      const payload = {
        Email: this.email,
        Password: this.password,
      };

      try {
        const response = await fetch("https://uleats-8xnb.onrender.com/Client/login", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(payload),
        });

        if (!response.ok) {
          const errorText = await response.text();
          alert("Error: " + errorText);
          return;
        }

        const data = await response.json();
        console.log("Respuesta del backend:", data);

        if (data.userId) {
          localStorage.setItem("user_id", data.userId);
        }
        if(data.customerId) {
          localStorage.setItem("customer_id", data.customerId);
        }
        if(data.deliveryId) {
          localStorage.setItem("delivery_id", data.deliveryId);
        }
        if (data.userType) {
          localStorage.setItem("user_type", data.userType);
          this.userType = data.userType;
        }
        
        alert("Login exitoso");
        if (this.userType === "customer") {
          this.$router.push("/customer");
        } else if (this.userType === "restaurant") {
          this.$router.push("/restaurant");
        } else if (this.userType === "delivery") {
          this.$router.push("/delivery");
        }
      } catch (err) {
        alert("Error de conexión con el servidor. " +err.message);
      }
    },
    goHome() {
      this.$router.push('/');
    }
  },
};
</script>

<style scoped>
.login-page {
  max-width: 400px;
  margin: 40px auto;
  padding: 32px 24px;
  text-align: center;
  background: #f6f8fa; /* Fondo claro pero diferente al blanco */
  border-radius: 16px;
  box-shadow: 0 2px 12px rgba(25, 118, 210, 0.08);
}

.form-group {
  margin-bottom: 18px;
}

label {
  display: block;
  margin-bottom: 7px;
  color: #1976d2;
  font-weight: 600;
  text-align: left;
}

input {
  width: 100%;
  padding: 10px;
  box-sizing: border-box;
  border: 1px solid #bdbdbd;
  border-radius: 6px;
  background: #fff;
  font-size: 1rem;
  margin-bottom: 4px;
  color: #111; /* <-- Añadido para texto negro */
}

input:focus {
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

.back-button {
  background: #eee;
  color: #1976d2;
  border: 1px solid #bdbdbd;
  margin-top: 18px;
}

.back-button:hover {
  background: #e3f2fd;
}
</style>