<template>
  <div class="product-form-page">
    <h1>{{ isEdit ? "Editar Producto" : "Crear Producto" }}</h1>
    <form @submit.prevent="submitProduct">
      <div class="form-group">
        <label for="name">Nombre:</label>
        <input type="text" id="name" v-model="name" required />
      </div>

      <div class="form-group">
        <label for="description">Descripción:</label>
        <textarea id="description" v-model="description"></textarea>
      </div>

      <div class="form-group">
        <label for="price">Precio:</label>
        <input type="number" id="price" v-model.number="price" step="0.01" min="0" required />
      </div>

      <div class="form-group">
        <label for="imageUrl">URL de la imagen:</label>
        <input type="url" id="imageUrl" v-model="imageUrl" />
      </div>

      <div class="form-group">
        <label for="category">Categoría:</label>
        <input type="text" id="category" v-model="category" />
      </div>

      <div class="form-group">
        <label>
          <input type="checkbox" v-model="isAvailable" />
          Disponible
        </label>
      </div>

      <button type="submit">{{ isEdit ? "Actualizar Producto" : "Crear Producto" }}</button>
    </form>
  </div>
</template>

<script>
export default {
  name: "ProductForm",
  data() {
    return {
      isEdit: false,
      productId: null,
      restaurantId: null,
      name: "",
      description: "",
      price: 0,
      imageUrl: "",
      category: "",
      isAvailable: true,
    };
  },
  created() {
    const id = this.$route.params.id; // "new" o un número
    const restaurantId = this.$route.query.restaurantId;
    this.restaurantId = restaurantId ? Number(restaurantId) : null;

    if (id && id !== "new") {
      this.isEdit = true;
      this.productId = Number(id);
      this.loadProductData(this.productId);
    } else {
      this.isEdit = false;
      // En creación, restaurantId debe estar definido
      if (!this.restaurantId) {
        alert("Falta el restaurantId en la URL");
        this.$router.push("/");
      }
    }
  },
  methods: {
    async loadProductData(id) {
      try {
        const response = await fetch(`/api/Product/${id}`);
        if (!response.ok) throw new Error("Error al cargar producto");
        const data = await response.json();
        this.name = data.name;
        this.description = data.description;
        this.price = data.price;
        this.imageUrl = data.imageUrl;
        this.category = data.category;
        this.isAvailable = data.isAvailable ?? true;
        this.restaurantId = data.restaurantId;
      } catch (err) {
        alert(err.message);
        this.$router.push("/");
      }
    },

    async submitProduct() {
      const payload = {
        RestaurantId: this.restaurantId,
        Name: this.name,
        Description: this.description,
        Price: this.price,
        ImageUrl: this.imageUrl,
        Category: this.category,
        IsAvailable: this.isAvailable,
      };

      try {
        let response;
        if (this.isEdit) {
          response = await fetch(`/api/Product/${this.productId}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload),
          });
        } else {
          response = await fetch("/api/Product", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(payload),
          });
        }

        if (!response.ok) {
          const errorText = await response.text();
          alert("Error al guardar el producto: " + errorText);
          return;
        }

        alert(this.isEdit ? "Producto actualizado" : "Producto creado");
        this.$router.push(`/restaurant/`);
      } catch (error) {
      }
    },
  },
};
</script>

<style scoped>
.product-form-page {
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
input[type="number"],
textarea {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  color: black;
  background-color: white;
}
textarea {
  resize: vertical;
  min-height: 60px;
}
button {
  padding: 10px 20px;
  font-size: 16px;
  cursor: pointer;
  background-color: #d32f2f;
  color: white;
  border: none;
  border-radius: 4px;
}
button:hover {
  background-color: #b71c1c;
}
</style>
