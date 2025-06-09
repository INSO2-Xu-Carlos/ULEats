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
import { ref } from 'vue';
import PendingOrders from '@/components/PendingOrders.vue';
import AcceptedOrders from '@/components/AcceptedOrders.vue';

type Order = {
  id: number;
  label: string;
  customer: string;
  status: string;
};

function updateSelectedOrder(val: Order | null) {
  selectedOrder.value = val;
}

const availableOrders = ref<Order[]>([
  { id: 1, label: 'Pedido 1', customer: 'Juan', status: 'Pendiente' },
  { id: 2, label: 'Pedido 2', customer: 'Ana', status: 'Pendiente' },
  { id: 3, label: 'Pedido 3', customer: 'Luis', status: 'Pendiente' },
]);

const acceptedOrders = ref<Order[]>([]);

const selectedOrder = ref<Order | null>(null);

const orderHeaders = [
  { text: 'Pedido', value: 'label' },
  { text: 'Cliente', value: 'customer' },
  { text: 'Estado', value: 'status' },
];

function acceptOrder() {
  if (selectedOrder.value) {
    const order = { ...selectedOrder.value, status: 'En curso' };
    acceptedOrders.value.push(order);
    availableOrders.value = availableOrders.value.filter(o => o.id !== order.id);
    selectedOrder.value = null;
  }
}
</script>