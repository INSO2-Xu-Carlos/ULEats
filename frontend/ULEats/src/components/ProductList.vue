<template>
  <section class="product-list">
    <ul v-if="products.length">
      <li
        v-for="product in products"
        :key="product.productId || product.id"
        class="product-item"
      >
        <button class="product-btn" @click="$emit('select-product', product)">
          <div class="product-info">
            <span class="product-name">{{ product.name }}</span>
            <span class="product-price">{{ product.price }} €</span>
          </div>
          <div class="product-description">{{ product.description }}</div>
        </button>
      </li>
    </ul>
    <p v-else>No hay productos disponibles para este restaurante.</p>
  </section>
</template>

<script>
export default {
  name: "ProductList",
  props: {
    products: {
      type: Array,
      default: () => [],
    },
  },
};
</script>

<style scoped>
.product-list {
  margin-top: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background: #f9f9f9;
}
.product-item {
  margin-bottom: 15px;
  padding-bottom: 10px;
  border-bottom: 1px solid #eee;
  list-style: none;
}
.product-btn {
  width: 100%;
  background: #fff;
  border: 1px solid #007bff;
  border-radius: 5px;
  padding: 10px;
  text-align: left;
  cursor: pointer;
  transition: background 0.2s, color 0.2s;
}
.product-btn:hover {
  background: #007bff;
  color: #fff;
}
.product-info {
  display: flex;
  justify-content: space-between;
  font-weight: bold;
}
.product-name {
  color: #333;
}
.product-price {
  color: #007bff;
}
.product-description {
  color: #666;
  margin-top: 5px;
}
</style>