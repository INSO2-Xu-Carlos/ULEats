<template>
  <div class="cart-drawer" v-if="visible">
    <h3>Carrito</h3>
    <ul v-if="cartItems.length">
      <li v-for="item in cartItems" :key="item.productId || item.id" class="cart-item">
        {{ item.productName }} - {{ item.unitPrice }} € x {{ item.quantity || 1 }}
        <button class="remove-btn" @click="$emit('remove-item', item)">🗑️</button>
      </li>
    </ul>
    <p v-else>El carrito está vacío.</p>
    <div v-if="cartItems.length" class="cart-total">
      <strong>Total: {{ totalPrice }} €</strong>
    </div>
    <button v-if="cartItems.length" class="buy-btn" @click="$emit('buy')">Realizar compra</button>
    <button @click="$emit('close')">Cerrar</button>
  </div>
</template>

<script>
export default {
  name: "CartDrawer",
  props: {
    visible: Boolean,
    cartItems: {
      type: Array,
      default: () => [],
    },
  },
  computed: {
    totalPrice() {
      return this.cartItems
        .reduce((sum, item) => sum + (item.unitPrice * (item.quantity || 1)), 0)
        .toFixed(2);
    },
  },
};
</script>

<style scoped>
.cart-drawer {
  position: fixed;
  top: 60px;
  right: 30px;
  width: 300px;
  background: #fff;
  border: 2px solid #007bff;
  border-radius: 8px;
  box-shadow: 0 2px 12px rgba(0,0,0,0.2);
  padding: 20px;
  z-index: 1000;
  color: #111;
}
.cart-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;
}
.cart-total {
  margin-top: 15px;
  font-size: 18px;
  text-align: right;
}
.buy-btn {
  width: 100%;
  margin-top: 10px;
  background: #007bff;
  color: #fff;
  border: none;
  border-radius: 5px;
  padding: 10px 0;
  font-size: 16px;
  cursor: pointer;
  transition: background 0.2s;
}
.buy-btn:hover {
  background: #0056b3;
}
.remove-btn {
  background: none;
  border: none;
  color: #d32f2f;
  font-size: 18px;
  cursor: pointer;
}
.remove-btn:hover {
  color: #b71c1c;
}
</style>