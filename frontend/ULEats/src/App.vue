<template>
  <v-app>
    <div id="app">
      <h1>ULEats</h1>
      <router-view />
      <AppFooter />
    </div>
  </v-app>
</template>

<script>
import AppFooter from '@/components/AppFooter.vue';

export default {
  components: {
    AppFooter
  }
};
</script>