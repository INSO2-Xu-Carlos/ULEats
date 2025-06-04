<template>
  <div class="register-delivery-page">
    <h1>Registro de Repartidor</h1>
    <form @submit.prevent="submitDelivery">
      <div class="form-group">
        <label for="licensePlate">Matrícula del vehículo:</label>
        <input type="text" id="licensePlate" v-model="licensePlate" />
      </div>
      <div class="form-group">
        <label for="phone">Número de teléfono:</label>
        <input type="text" id="phone" v-model="phone" required />
      </div>
      <div class="form-group">
        <label for="vehicleType">Tipo de vehículo:</label>
        <select id="vehicleType" v-model="vehicleType" required>
          <option value="" disabled>Selecciona una opción</option>
          <option value="Bike">Bike</option>
          <option value="Scooter">Scooter</option>
          <option value="Motorbike">Motorbike</option>
          <option value="Car">Car</option>
        </select>
      </div>
      <button type="submit">Guardar datos</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "RegisterDeliveryPage",
  data() {
    return {
      licensePlate: "",
      phone: "",
      vehicleType: "",
    };
  },
  methods: {
    async submitDelivery() {
      const userId = localStorage.getItem("user_id");
      const payload = {
        LicensePlate: this.licensePlate,
        Phone: this.phone,
        VehicleType: this.vehicleType,
        userId: userId,
      };

      try {
        const response = await fetch("/api/Delivery", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(payload),
        });

        if (!response.ok) {
          const error = await response.text();
          alert("Error al registrar el repartidor: " + error);
          return;
        }

        const data = await response.json();
        if (data.userId) {
          localStorage.setItem("user_id", data.userId);
        }

        alert("Repartidor registrado correctamente.");
        this.$router.push('/delivery');
      } catch (err) {
        alert("Error de conexión con el servidor.");
      }
    },
  },
};
</script>

<style scoped>
.register-delivery-page {
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
select {
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