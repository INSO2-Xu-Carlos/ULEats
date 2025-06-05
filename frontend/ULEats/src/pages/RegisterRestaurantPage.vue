<template>
  <div class="register-restaurant-page">
    <h1>Registro de Restaurante</h1>
    <form @submit.prevent="submitRestaurant">
      <div class="form-group">
        <label for="name">Nombre del restaurante:</label>
        <input type="text" id="name" v-model="name" required />
      </div>
      <div class="form-group">
        <label for="address">Dirección:</label>
        <input type="text" id="address" v-model="address" required />
      </div>
      <div class="form-group">
        <label for="phone">Teléfono:</label>
        <input type="text" id="phone" v-model="phone" required />
      </div>
      <div class="form-group">
        <label for="description">Descripción:</label>
        <textarea id="description" v-model="description" required></textarea>
      </div>
      <div class="form-group">
        <label for="logoUrl">URL del logo:</label>
        <input type="url" id="logoUrl" v-model="logoUrl" required />
      </div>
      <button type="submit">Registrar restaurante</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "RegisterRestaurantPage",
  data() {
    return {
      name: "",
      address: "",
      phone: "",
      description: "",
      logoUrl: "",
    };
  },
  methods: {
    async submitRestaurant() {
    
    const payload = {
      Name: this.name,
      Address: this.address,
      Phone: this.phone,
      Description: this.description,
      LogoUrl: this.logoUrl,
      UserId: Number(localStorage.getItem("user_id")),
    };

    try {
      const response = await fetch("/api/Restaurant", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      });

      if (!response.ok) {
        const error = await response.text();
        alert("Error al registrar el restaurante: " + error);
        return;
      }

      // Suponiendo que el backend devuelve un objeto con userId
      const data = await response.json();
      if (data.userId) {
        localStorage.setItem("user_id", data.userId);
      }

      alert("Restaurante registrado correctamente.");
      this.$router.push('/restaurant');
    } catch (err) {
      alert("Error de conexión con el servidor.");
    }
  },
  },
};
</script>

<style scoped>
.register-restaurant-page {
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
input[type="text"],
input[type="url"],
textarea {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
textarea {
  resize: vertical;
  min-height: 60px;
}
button {
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
}
</style>