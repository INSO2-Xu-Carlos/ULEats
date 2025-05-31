<template>
  <div class="register-page">
    <h1>Register</h1>
    <form @submit.prevent="registerUser">
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
            id="customer"
            value="customer"
            name="userType"
            required
          />
          <label for="customer">Client</label>
        </div>
        <div>
          <input
            type="radio"
            id="restaurant"
            value="restaurant"
            name="userType"
            required
          />
          <label for="restaurant">Restaurant</label>
        </div>
        <div>
          <input
            type="radio"
            id="delivery"
            value="delivery"
            name="userType"
            required
          />
          <label for="delivery">Delivey</label>
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
      userType: "",
    };
  },
  methods: {
    async registerUser() {
      const payload = {
        FirstName: this.firstname,
        LastName: this.lastname,
        Email: this.email,
        Password: this.password,
        Phone: this.phonenumber,
        UserType: this.userType,
      };

      try {
        const response = await fetch("/api/Client/register", {
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

        alert("¡Registro exitoso!");
        this.$router.push('/login');
      } catch (error) {
        alert("Error de red: " + error);
      }
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