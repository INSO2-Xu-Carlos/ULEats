<template>
  <div class="client-page">
    <header class="header">
      <h1>Bienvenido a ULEats</h1>
      <p>Explora los restaurantes disponibles para tu pedido.</p>
    </header>
    <main class="main-content">
      <aside class="restaurant-display">
        <h2>Restaurantes Disponibles</h2>
        <p>No hay restaurantes disponibles en este momento.</p>
      </aside>
      <section class="order-section">
        <h2>Tu Pedido</h2>
        <p>Aquí aparecerán los detalles de tu pedido.</p>
      </section>
    </main>
  </div>
</template>

<script>
export default {
  name: "ClientPage",
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