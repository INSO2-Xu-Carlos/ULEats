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
        const response = await fetch("/api/Client/login", {
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
        alert("Error de conexión con el servidor.");
      }
    },
  },
};
</script>

<style scoped>
.login-page {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  text-align: center;
}
.form-group {
  margin-bottom: 15px;
}
label {
  display: block;
  margin-bottom: 5px;
}
input {
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