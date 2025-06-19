<template>
  <div class="home-layout">
    <header class="header">
      <h1>ULEats</h1>
    </header>
    <main class="main-content">
      <slot />
    </main>
    <AppFooter />
  </div>
</template>

<script>
import AppFooter from '@/components/AppFooter.vue';

export default {
  components: {
    AppFooter
  }
};
</script>

<style scoped>
.home-layout {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}
.header {
  background: #1976d2;
  color: #fff;
  padding: 1.5rem 0;
  text-align: center;
}
.main-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
</style>