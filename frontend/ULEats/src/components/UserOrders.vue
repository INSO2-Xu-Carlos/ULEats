<template>
  <v-card>
    <v-card-title>Mis pedidos</v-card-title>
    <v-data-table
      :headers="headers"
      :items="orders"
      class="elevation-1"
      item-key="id"
    >
      <template #item.status="{ item }">
        <v-chip :color="statusColor(item.status)" dark>
          {{ item.status }}
        </v-chip>
      </template>
    </v-data-table>
  </v-card>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';

const headers = [
  { text: 'Dirección', value: 'deliveryAddress' },
  { text: 'Estado', value: 'status' },
];
const orders = ref<any[]>([]);

const baseUrl = import.meta.env.PROD
  ? 'https://uleats-8xnb.onrender.com'
  : '/api';

function statusColor(status: string) {
  switch (status) {
    case 'En curso': return 'orange';
    case 'Entregado': return 'green';
    case 'Cancelado': return 'red';
    default: return 'grey';
  }
}

const customerId = localStorage.getItem('customer_id');

onMounted(async () => {
  if (!customerId) return;
  try {
    const res = await fetch(`${baseUrl}/Order/byCustomer/${customerId}`);
    if (res.ok) {
      orders.value = await res.json();
    } else {
      orders.value = [];
    }
  } catch {
    orders.value = [];
  }
});
</script>