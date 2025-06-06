<template>
  <div class="client-page">
    <header class="header">
      <h1>Bienvenido a ULEats</h1>
      <p>Explora los restaurantes disponibles para tu pedido.</p>
      <button class="cart-button" @click="showCart = true" title="Ver carrito">
        🛒
      </button>
    </header>
    <CartDrawer
      :visible="showCart"
      :cartItems="cart"
      @close="showCart = false"
      @remove-item="removeFromCart"
      @buy="goToPayment"
    />
    <main class="main-content">
      <RestaurantList
        :restaurants="restaurants"
        @select-restaurant="handleSelectRestaurant"
      />
      <section class="order-section">
        <h2>Productos del restaurante</h2>
        <ProductList
          :products="products"
          v-if="selectedRestaurant"
          @select-product="addToCart"
        />
        <p v-else>Selecciona un restaurante para ver sus productos.</p>
      </section>
    </main>
  </div>
</template>

<script>
import RestaurantList from "@/components/RestaurantList.vue";
import ProductList from "@/components/ProductList.vue";
import CartDrawer from "@/components/CartDrawer.vue";

export default {
  name: "ClientPage",
  components: {
    RestaurantList,
    ProductList,
    CartDrawer,
  },
  data() {
    return {
      restaurants: [],
      selectedRestaurant: null,
      products: [],
      cart: [],
      showCart: false,
    };
  },
  methods: {
    async fetchRestaurants() {
      try {
        const response = await fetch("/api/Restaurant");
        if (response.ok) {
          this.restaurants = await response.json();
        } else {
          this.restaurants = [];
        }
      } catch {
        this.restaurants = [];
      }
    },
    async handleSelectRestaurant(restaurant) {
      this.selectedRestaurant = restaurant;
      try {
        const response = await fetch(`/api/Product/ByRestaurant/${restaurant.restaurantId || restaurant.id}`);
        if (response.ok) {
          this.products = await response.json();
        } else {
          this.products = [];
        }
      } catch {
        this.products = [];
      }
    },
    async addToCart(product) {
      const customerId = localStorage.getItem("customer_id");
      if (!customerId) {
        alert("No se ha encontrado el customer_id. Inicia sesión de nuevo.");
        return;
      }

      // Comprobar si el producto ya está en el carrito
      let cartItems = [];
      try {
        const response = await fetch(`/api/OrderItem/byCustomer/${customerId}`);
        if (response.ok) {
          cartItems = await response.json();
        }
      } catch {
        cartItems = [];
      }

      const existingItem = cartItems.find(
        item => (item.productId || item.id) === (product.productId || product.id)
      );

      if (existingItem) {
        // Si existe, actualiza la cantidad
        const updatedPayload = {
          quantity: existingItem.quantity + 1
        };
        await fetch(`/api/OrderItem/${existingItem.orderItemId || existingItem.id}`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(updatedPayload),
        });
      } else {
        // Si no existe, crea el order_item
        const payload = {
          customerId: Number(customerId),
          productId: product.productId || product.id,
          quantity: 1,
          unitPrice: product.price,
          productName: product.name
        };
        await fetch("/api/OrderItem", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(payload),
        });
      }

      this.fetchCartItems();
    },
    async fetchCartItems() {
      const customerId = localStorage.getItem("customer_id");
      if (!customerId) {
        this.cart = [];
        return;
      }
      try {
        const response = await fetch(`/api/OrderItem/byCustomer/${customerId}`);
        if (response.ok) {
          this.cart = await response.json();
        } else {
          this.cart = [];
        }
      } catch {
        this.cart = [];
      }
    },
    async removeFromCart(item) {
      try {
        const response = await fetch(`/api/OrderItem/${item.orderItemId || item.id}`, {
          method: "DELETE",
        });
        if (response.ok) {
          this.fetchCartItems();
        } else {
          alert("No se pudo eliminar el producto del carrito.");
        }
      } catch {
        alert("Error de conexión al eliminar del carrito.");
      }
    },
    goToPayment() {
      this.$router.push("/customer/payment");
    },
  },
  mounted() {
    this.fetchRestaurants();
    this.fetchCartItems();
  },
};
</script>

<style scoped>
.client-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  font-family: Arial, sans-serif;
}

.header {
  text-align: center;
  margin-bottom: 20px;
  position: relative;
}

.cart-button {
  position: absolute;
  right: 20px;
  top: 20px;
  background: #fff;
  border: 1px solid #ccc;
  border-radius: 50%;
  font-size: 28px;
  padding: 8px 14px;
  cursor: pointer;
  transition: background 0.2s;
}
.cart-button:hover {
  background: #f0f0f0;
}

.main-content {
  display: flex;
  justify-content: space-between;
  width: 100%;
  max-width: 1200px;
}

.order-section {
  flex: 2;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
}

h1, h2 {
  color: #333;
}

p {
  color: #666;
}
</style>