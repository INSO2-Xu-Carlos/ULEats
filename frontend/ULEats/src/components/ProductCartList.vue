<template>
  <div class="product-cart-list">
    <h3>Productos en el carrito</h3>
    <ul v-if="cartItems.length">
      <li v-for="item in cartItems" :key="item.productId || item.id" class="cart-item">
        {{ item.productName }} - {{ item.unitPrice }} € x {{ item.quantity || 1 }}
      </li>
    </ul>
    <p v-else>El carrito está vacío.</p>
    <div v-if="cartItems.length" class="cart-total">
      <strong>Total a pagar: {{ totalPrice }} €</strong>
    </div>
  </div>
</template>

<script>
export default {
  name: "ProductCartList",
  props: {
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
.product-cart-list {
  padding: 20px;
  background: #f9f9f9;
  border-radius: 8px;
}
.cart-item {
  margin-bottom: 10px;
}
.cart-total {
  margin-top: 15px;
  font-size: 18px;
  text-align: right;
}
</style>