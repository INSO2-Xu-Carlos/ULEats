<template>
  <aside class="restaurant-display">
    <h2>Restaurantes Disponibles</h2>
    <ul v-if="restaurants.length" class="restaurant-list">
      <li v-for="restaurant in restaurants" :key="restaurant.restaurantId || restaurant.id">
        <button class="restaurant-btn" @click="$emit('select-restaurant', restaurant)">
          {{ restaurant.name }}
        </button>
      </li>
    </ul>
    <p v-else>No hay restaurantes disponibles en este momento.</p>
  </aside>
</template>

<script>
export default {
  name: "RestaurantList",
  props: {
    restaurants: {
      type: Array,
      default: () => [],
    },
  },
};
</script>

<style scoped>
.restaurant-display {
  flex: 1;
  margin-right: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
}
.restaurant-list {
  list-style: none;
  padding: 0;
}
.restaurant-btn {
  width: 100%;
  padding: 10px 0;
  margin-bottom: 10px;
  background: #fff;
  border: 1px solid #007bff;
  border-radius: 5px;
  color: #007bff;
  font-size: 16px;
  cursor: pointer;
  transition: background 0.2s, color 0.2s;
}
.restaurant-btn:hover {
  background: #007bff;
  color: #fff;
}
</style>