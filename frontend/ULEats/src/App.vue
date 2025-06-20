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

<style>
body {
  background: #f6f8fa;
  font-family: 'Segoe UI', 'Roboto', Arial, sans-serif;
  color: #222;
  margin: 0;
  padding: 0;
}

h1, h2, h3 {
  color: #1976d2;
  margin-bottom: 0.5em;
}

.v-card, .card, .main-content {
  background: #fff;
  border-radius: 16px;
  box-shadow: 0 2px 12px rgba(25, 118, 210, 0.08);
  padding: 24px;
  margin-bottom: 24px;
}

button, .v-btn {
  background: linear-gradient(90deg, #43e97b 0%, #38f9d7 100%);
  color: #fff;
  border: none;
  border-radius: 8px;
  padding: 10px 24px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s, box-shadow 0.2s;
  box-shadow: 0 2px 8px rgba(67, 233, 123, 0.08);
  margin: 8px 0;
}

button:hover, .v-btn:hover {
  background: linear-gradient(90deg, #38f9d7 0%, #43e97b 100%);
  box-shadow: 0 4px 16px rgba(67, 233, 123, 0.16);
}

input, select, textarea {
  border: 1px solid #bdbdbd;
  border-radius: 6px;
  padding: 10px;
  font-size: 1rem;
  margin-bottom: 16px;
  width: 100%;
  box-sizing: border-box;
  background: #f9f9f9;
  transition: border 0.2s;
}

input:focus, select:focus, textarea:focus {
  border-color: #1976d2;
  outline: none;
}

.v-chip {
  border-radius: 16px;
  font-weight: 500;
  font-size: 0.95em;
}

.header, .home-layout .header {
  background: linear-gradient(90deg, #1976d2 0%, #43e97b 100%);
  color: #fff;
  padding: 1.5rem 0;
  text-align: center;
  letter-spacing: 2px;
  font-size: 2rem;
  font-weight: bold;
  box-shadow: 0 2px 8px rgba(25, 118, 210, 0.08);
}

.footer, .home-layout .footer {
  background: #222;
  color: #fff;
  text-align: center;
  padding: 1rem 0;
  font-size: 1rem;
  letter-spacing: 1px;
}

.v-data-table th {
  background: #e3f2fd;
  color: #1976d2;
  font-weight: 700;
}

.v-data-table td {
  background: #fff;
}

@media (max-width: 600px) {
  .v-card, .card, .main-content {
    padding: 12px;
    border-radius: 8px;
  }
  .header, .home-layout .header {
    font-size: 1.2rem;
    padding: 1rem 0;
  }
}
</style>