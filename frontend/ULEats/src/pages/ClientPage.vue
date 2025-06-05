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
    goToCart() {
      this.showCart = true;
    },
    addToCart(product) {
      const existing = this.cart.find(
        (item) => (item.productId || item.id) === (product.productId || product.id)
      );
      if (existing) {
        existing.quantity = (existing.quantity || 1) + 1;
      } else {
        this.cart.push({ ...product, quantity: 1 });
      }
    },
    removeFromCart(product) {
      this.cart = this.cart.filter(
        (item) => (item.productId || item.id) !== (product.productId || product.id)
      );
    },
    async fetchRestaurants() {
      try {
        const response = await fetch("/api/Restaurant");
        if (response.ok) {
          const data = await response.json();
          this.restaurants = data;
        } else {
          this.restaurants = [];
        }
      } catch (err) {
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
      } catch (err) {
        this.products = [];
      }
    },
  },
  mounted() {
    this.fetchRestaurants();
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
}

.main-content {
  display: flex;
  justify-content: space-between;
  width: 100%;
  max-width: 1200px;
}

.restaurant-display {
  flex: 1;
  margin-right: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
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