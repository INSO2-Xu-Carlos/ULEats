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
          @unassign-order="unassignOrder"
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
  customer?: string;
  status: string;
  deliveryId: number | null;
};

const availableOrders = ref<Order[]>([]);
const acceptedOrders = ref<Order[]>([]);
const selectedOrder = ref<Order | null>(null);

const orderHeaders = [
  { text: 'Pedido', value: 'label' },
  { text: 'Estado', value: 'status' },
  { text: 'Acciones', value: 'actions', sortable: false },
];

function updateSelectedOrder(val: Order | null) {
  selectedOrder.value = val;
}

function acceptOrder() {
  if (selectedOrder.value) {
    const deliveryId = Number(localStorage.getItem("delivery_id"));
    const orderId = selectedOrder.value.id;

    const orderToUpdate = {
      ...selectedOrder.value,
      deliveryId: deliveryId,
      status: "preparing"
    };

    fetch(`/api/Order/${orderId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(orderToUpdate),
    })
    .then(res => {
      if (res.ok) {
        const { id, label } = selectedOrder.value!;
        const order = {
          id,
          label,
          status: 'En curso',
          deliveryId,
        };
        acceptedOrders.value.push(order);
        availableOrders.value = availableOrders.value.filter(o => o.id !== order.id);
        selectedOrder.value = null;
      } else {
        alert("No se pudo aceptar el pedido en la base de datos.");
      }
    });
  }
}


onMounted(async () => {
  const deliveryId = Number(localStorage.getItem("delivery_id"));
  const res = await fetch('/api/Order');
  const data = await res.json();

  availableOrders.value = data
    .filter((order: any) => order.deliveryId === null)
    .map((order: any) => ({
      id: order.orderId,
      label: order.deliveryAddress || order.address || `Pedido ${order.orderId}`,
      status: order.status,
      deliveryId: order.deliveryId,
      deliveryAddress: order.deliveryAddress || order.address || '',
    }));
  acceptedOrders.value = data
    .filter((order: any) => order.deliveryId === deliveryId)
    .map((order: any) => ({
      id: order.orderId,
      label: order.deliveryAddress || order.address || `Pedido ${order.orderId}`,
      status: order.status,
      deliveryId: order.deliveryId,
      deliveryAddress: order.deliveryAddress || order.address || '',
    }));
});

function unassignOrder(order: Order) {
  fetch(`/api/Order/${order.id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      ...order,
      deliveryId: null,
      status: "pending"
    }),
  })
  .then(res => {
    if (res.ok) {
      acceptedOrders.value = acceptedOrders.value.filter(o => o.id !== order.id);
      availableOrders.value.push({ ...order, deliveryId: null, status: "pending" });
    } else {
      alert("No se pudo desasignar el pedido.");
    }
  });
}
</script>