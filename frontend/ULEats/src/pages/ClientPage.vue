<template>
  <div class="client-page">
    <header class="header">
      <div class="header-buttons">
        <LogoutButton class="logout-button" />
        <button class="cart-button" @click="showCart = true" title="Ver carrito">
          🛒
        </button>
        <button class="orders-button" @click="showOrders = !showOrders">
          Ver mis pedidos
        </button>
      </div>
      <h1>Bienvenido a ULEats</h1>
      <p>Explora los restaurantes disponibles para tu pedido.</p>
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
          v-if="selectedRestaurant && products.length"
          @select-product="addToCart"
        />
        <p
          v-else-if="selectedRestaurant && !products.length"
          class="no-products-message"
        >
          No hay productos disponibles para este restaurante.
        </p>
        <p v-else>Selecciona un restaurante para ver sus productos.</p>
        <div v-if="showOrders" class="user-orders-wrapper-blue">
          <UserOrders />
        </div>
      </section>
    </main>
  </div>
</template>

<script>
import RestaurantList from "@/components/RestaurantList.vue";
import ProductList from "@/components/ProductList.vue";
import CartDrawer from "@/components/CartDrawer.vue";
import UserOrders from "@/components/UserOrders.vue";
import LogoutButton from "@/components/LogoutButton.vue";

export default {
  name: "ClientPage",
  components: {
    RestaurantList,
    ProductList,
    CartDrawer,
    UserOrders,
    LogoutButton,
  },
  data() {
    return {
      restaurants: [],
      selectedRestaurant: null,
      products: [],
      cart: [],
      showCart: false,
      cartRestaurantId: null,
      showOrders: false,
    };
  },
  methods: {
    async fetchRestaurants() {
      try {
        const response = await fetch("https://uleats-8xnb.onrender.com/Restaurant");
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
        const response = await fetch(`https://uleats-8xnb.onrender.com/Product/ByRestaurant/${restaurant.restaurantId || restaurant.id}`);
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
      const productRestaurantId = this.selectedRestaurant?.restaurantId || this.selectedRestaurant?.id;
      if (this.cart.length > 0 && this.cartRestaurantId && this.cartRestaurantId !== productRestaurantId) {
        alert("Solo puedes añadir productos de un restaurante a la vez. Vacía el carrito para cambiar de restaurante.");
        return;
      }

      if (this.cart.length === 0) {
        this.cartRestaurantId = productRestaurantId;
      }

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

      this.fetchCartItems();
    },
    async fetchCartItems() {
      const customerId = localStorage.getItem("customer_id");
      if (!customerId) {
        this.cart = [];
        this.cartRestaurantId = null;
        return;
      }
      try {
        const response = await fetch(`https://uleats-8xnb.onrender.com/OrderItem/byCustomer/${customerId}`);
        if (response.ok) {
          this.cart = await response.json();
          if (this.cart.length > 0) {
            this.cartRestaurantId = this.selectedRestaurant?.restaurantId || this.selectedRestaurant?.id;
          } else {
            this.cartRestaurantId = null;
          }
        } else {
          this.cart = [];
          this.cartRestaurantId = null;
        }
      } catch {
        this.cart = [];
        this.cartRestaurantId = null;
      }
    },
    async removeFromCart(item) {
      try {
        const response = await fetch(`https://uleats-8xnb.onrender.com/OrderItem/${item.orderItemId || item.id}`, {
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

.header-buttons {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 16px;
  margin-bottom: 16px;
}

.cart-button {
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

.orders-button {
  background: #fff;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 16px;
  padding: 8px 14px;
  cursor: pointer;
  transition: background 0.2s;
  color: #111;
}
.orders-button:hover {
  background: #f0f0f0;
}

.user-orders-wrapper-blue {
  background: #1976d2;
  border-radius: 16px;
  box-shadow: 0 2px 12px rgba(25, 118, 210, 0.08);
  padding: 24px;
  margin-top: 20px;
}

.user-orders-wrapper-blue,
.user-orders-wrapper-blue * {
  color:#6386a9 !important;
}

.no-products-message {
  color: #111;
  font-weight: 500;
  margin-top: 12px;
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