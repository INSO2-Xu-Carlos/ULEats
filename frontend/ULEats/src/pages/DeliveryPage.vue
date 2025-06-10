<template>
  <v-container>
    <h1 class="mb-6">Repartidor</h1>
    <v-row>
      <v-col cols="12" md="6">
        <PendingOrders
          :availableOrders="availableOrders"
          :selectedOrder="selectedOrder"
          @update:selectedOrder="updateSelectedOrder"
          @accept-order="acceptOrder"
        />
        <AcceptedOrders
          :acceptedOrders="acceptedOrders"
          :orderHeaders="orderHeaders"
        />
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import PendingOrders from '@/components/PendingOrders.vue';
import AcceptedOrders from '@/components/AcceptedOrders.vue';

type Order = {
  id: number;
  label: string;
  customer: string;
  status: string;
  deliveryId: number | null;
};

const availableOrders = ref<Order[]>([]);
const acceptedOrders = ref<Order[]>([]);
const selectedOrder = ref<Order | null>(null);

const orderHeaders = [
  { text: 'Pedido', value: 'label' },
  { text: 'Cliente', value: 'customer' },
  { text: 'Estado', value: 'status' },
];

function updateSelectedOrder(val: Order | null) {
  selectedOrder.value = val;
}

function acceptOrder() {
  if (selectedOrder.value) {
    const order = { ...selectedOrder.value, status: 'En curso' };
    acceptedOrders.value.push(order);
    availableOrders.value = availableOrders.value.filter(o => o.id !== order.id);
    selectedOrder.value = null;
  }
}

// Cargar pedidos pendientes desde la API
onMounted(async () => {
  const res = await fetch('/api/Order');
  const data = await res.json();
  // Filtrar los pedidos con deliveryId === null
  availableOrders.value = data
    .filter((order: any) => order.deliveryId === null)
    .map((order: any) => ({
      id: order.id,
      label: `Pedido ${order.id}`,
      customer: order.customerName || order.customer || 'Desconocido',
      status: order.status,
      deliveryId: order.deliveryId,
    }));
});
</script>