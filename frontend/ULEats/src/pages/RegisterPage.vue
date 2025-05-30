<template>
  <div class="register-page">
    <h1>Register</h1>
    <form @submit.prevent="handleRegister">
      <div class="form-group">
        <label for="firstname">First Name:</label>
        <input type="text" id="firstname" v-model="firstname" required />
      </div>
      <div class="form-group">
        <label for="lastname">Last Name:</label>
        <input type="text" id="lastname" v-model="lastname" required />
      </div>
      <div class="form-group">
        <label for="email">Email:</label>
        <input type="email" id="email" v-model="email" required />
      </div>
      <div class="form-group">
        <label for="phonenumber">Phone Number:</label>
        <input type="phonenumber" id="phonenumber" v-model="phonenumber" required />
      </div>
      <div class="form-group">
        <label for="password">Password:</label>
        <input type="password" id="password" v-model="password" required />
      </div>
      <div class="form-group">
        <label>Record type:</label>
        <div>
          <input
            type="radio"
            id="client"
            value="client"
            v-model="userType"
            required
          />
          <label for="client">Client</label>
        </div>
        <div>
          <input
            type="radio"
            id="resstaurant"
            value="restaurant"
            v-model="userType"
            required
          />
          <label for="restaurante">Restaurant</label>
        </div>
        <div>
          <input
            type="radio"
            id="delivery"
            value="delivery"
            v-model="userType"
            required
          />
          <label for="restaurante">Delivey</label>
        </div>
      </div>
      <button type="submit">Register</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "RegisterPage",
  data() {
    return {
      firstname: "",
      lastname: "",
      email: "",
      password: "",
      phonenumber: "",
      userType: "", // Campo adicional para el tipo de usuario
    };
  },
  methods: {
    handleRegister() {
      console.log("Registration data:", {
        firstname: this.firstname,
        lastname: this.lastname,
        email: this.email,
        password: this.password,
        phonenumber: this.phonenumber,
        userType: this.userType,
      });
      alert(
        `Registro exitoso como ${this.userType === "cliente" ? "Cliente" : "Restaurante Delivery"} (simulado)`
      );
    },
  },
};
</script>

<style scoped>
.register-page {
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
input[type="text"],
input[type="email"],
input[type="password"] {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
input[type="radio"] {
  margin-right: 5px;
}
button {
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
}
</style>