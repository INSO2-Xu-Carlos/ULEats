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
      <button type="submit">
        {{ isEdit ? 'Actualizar restaurante' : 'Registrar restaurante' }}
      </button>
    </form>
  </div>
</template>

<script>
export default {
  name: "RegisterRestaurantPage",
  props: {
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      isEdit: false,
      restaurantId: null,
      name: "",
      address: "",
      phone: "",
      description: "",
      logoUrl: "",
    };
  },
  
  methods: {
    async initEdit() {
      await this.loadRestaurantData(this.restaurantId);
    },

    async loadRestaurantData(id) {
      try {
        const response = await fetch(`https://uleats-8xnb.onrender.com/Restaurant/${id}`);
        if (!response.ok) throw new Error("error");

        const data = await response.json();
        this.name = data.name;
        this.address = data.address;
        this.phone = data.phone;
        this.description = data.description;
        this.logoUrl = data.logoUrl;
      } catch (err) {
        console.error(err);
      }
    },

    async createRestaurant() {
      const payload = {
        Name: this.name,
        Address: this.address,
        Phone: this.phone,
        Description: this.description,
        LogoUrl: this.logoUrl,
        UserId: Number(localStorage.getItem("user_id")),
      };

      try {
        const response = await fetch("https://uleats-8xnb.onrender.com/Restaurant", {
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

    async updateRestaurant() {
      const payload = {
        RestaurantId: Number(this.restaurantId),
        Name: this.name,
        Address: this.address,
        Phone: this.phone,
        Description: this.description,
        LogoUrl: this.logoUrl,
        UserId: Number(localStorage.getItem("user_id")),
        Orders: [],
        Products: [],
      };

      try {
        const response = await fetch(`https://uleats-8xnb.onrender.com/Restaurant/${this.restaurantId}`, {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(payload),
        });

        if (!response.ok) {
          const error = await response.text();
          alert("Error al actualizar el restaurante: " + error);
          return;
        }
        alert("Restaurante actualizado correctamente.");
        this.$router.push('/restaurant');
      } catch (err) {
        alert("Error de conexión con el servidor.");
      }
    },

    async submitRestaurant() {
      if (this.isEdit) {
        await this.updateRestaurant();
      } else {
        await this.createRestaurant();
      }
    },
  },
  watch: {
  id: {
    immediate: true,
    handler(newId) {
      // Considera null, undefined y string vacío como "no hay id"
      if (newId && newId !== "undefined" && newId !== "") {
        this.isEdit = true;
        this.restaurantId = newId;
        this.initEdit();
      } else {
        this.isEdit = false;
        this.restaurantId = null;
        this.name = "";
        this.address = "";
        this.phone = "";
        this.description = "";
        this.logoUrl = "";
      }
    }
  }
},
};
</script>

<style scoped>
.register-restaurant-page {
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
input[type="url"],
textarea {
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
input[type="url"]:focus,
textarea:focus {
  border-color: #1976d2;
  outline: none;
}

textarea {
  resize: vertical;
  min-height: 60px;
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