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
      let vehiclePlate = this.licensePlate;

      if(this.vehicleType == "Bike" ||this.vehicleType == "Scooter"){
        vehiclePlate = null;
      }
      const payload = {
        vehiclePlate: this.licensePlate,
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
          localStorage.setItem("delivery_id", data.deliveryId);
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

input[type="text"],
select {
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
select:focus {
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