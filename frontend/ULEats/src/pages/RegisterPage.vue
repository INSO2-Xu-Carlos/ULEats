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
        <div class="record-type-options">
          <div>
            <input
              type="radio"
              id="customer"
              value="customer"
              v-model="userType"
              required
            />
            <label for="customer">Client</label>
          </div>
          <div>
            <input
              type="radio"
              id="restaurant"
              value="restaurant"
              v-model="userType"
              required
            />
            <label for="restaurant">Restaurant</label>
          </div>
          <div>
            <input
              type="radio"
              id="delivery"
              value="delivery"
              v-model="userType"
              required
            />
            <label for="delivery">Delivery</label>
          </div>
        </div>
      </div>
      <button type="submit">Register</button>
    </form>
    <button class="back-button" @click="goHome">Volver a la página principal</button>
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
        const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
        const response = await fetch(`${baseUrl}/Client/register`, {
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
        const text = await response.text();
        let data = {};
        if (text) {
          data = JSON.parse(text);
        }
        console.log("Respuesta del backend:", data);
        if (data.userId) {
          localStorage.setItem("user_id", data.userId);
        }
        if (this.userType === "customer") {
          this.$router.push("/register/client");
        } else if (this.userType === "restaurant") {
          this.$router.push("/register/restaurant");
        } else if (this.userType === "delivery") {
          this.$router.push("/register/delivery");
        }

      } catch (error) {
        alert("Error de red: " + error);
      }
    },
    goHome() {
      this.$router.push('/');
    }
  },
  
};
</script>

<style scoped>
.register-page {
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
}

label {
  display: block;
  margin-bottom: 7px;
  color: #1976d2;
  font-weight: 600;
  text-align: left;
}

input[type="text"],
input[type="email"],
input[type="phonenumber"],
input[type="password"] {
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

input[type="text"]:focus,
input[type="email"]:focus,
input[type="phonenumber"]:focus,
input[type="password"]:focus {
  border-color: #1976d2;
  outline: none;
}

input[type="radio"] {
  margin-right: 5px;
}

.record-type-options {
  display: flex;
  flex-direction: row;
  gap: 18px;
  justify-content: flex-start;
  margin-top: 6px;
}

.record-type-options > div {
  display: flex;
  align-items: center;
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