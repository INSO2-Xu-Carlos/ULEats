<template>
  <v-container>
    <div class="header-row">
      <h1 class="mb-6">Repartidor</h1>
      <LogoutButton class="logout-button" />
    </div>
    <v-row>
      <v-col cols="12" md="6">
        <div class="delivery-section-bg">
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
            @on-way-order="onWayOrder"
            @delivered-order="deliveredOrder"
          />
        </div>
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
      status: "En curso",
    };
    const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
    fetch(`${baseUrl}/Order/${orderId}`, {
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

function onWayOrder(order: Order) {
  const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';

  fetch(`${baseUrl}/Order/${order.id}/status-delivery`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      status: "En camino",
      deliveryId: order.deliveryId,
    }),
  })
  .then(res => {
    if (res.ok) {
      const idx = acceptedOrders.value.findIndex(o => o.id === order.id);
      if (idx !== -1) {
        acceptedOrders.value[idx].status = "En camino";
      }
    } else {
      alert("No se pudo actualizar el pedido a 'En camino'.");
    }
  });
}

function deliveredOrder(order: Order) {
  const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
  fetch(`${baseUrl}/Order/${order.id}/status-delivery`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      status: "Entregado",
      deliveryId: order.deliveryId,
    }),
  })
  .then(res => {
    if (res.ok) {
      const idx = acceptedOrders.value.findIndex(o => o.id === order.id);
      if (idx !== -1) {
        acceptedOrders.value[idx].status = "Entregado";
      }
    } else {
      alert("No se pudo actualizar el pedido a 'Entregado'.");
    }
  });
}

function unassignOrder(order: Order) {
  const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
  fetch(`${baseUrl}/Order/${order.id}/status-delivery`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      status: "Pendiente",
      deliveryId: null,
    }),
  })
  .then(res => {
    if (res.ok) {
      acceptedOrders.value = acceptedOrders.value.filter(o => o.id !== order.id);
      availableOrders.value.push({ ...order, deliveryId: null, status: "Pendiente" });
    } else {
      alert("No se pudo desasignar el pedido.");
    }
  });
}

onMounted(async () => {
  const deliveryId = Number(localStorage.getItem("delivery_id"));
  const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
  const res = await fetch(`${baseUrl}/Order`);
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
</script>

<style scoped>
.header-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 24px;
}

.logout-button {
  margin-left: 16px;
}

.delivery-section-bg {
  background: #1976d2;
  border-radius: 16px;
  box-shadow: 0 2px 12px rgba(25, 118, 210, 0.08);
  padding: 24px;
  color: #fff;
}

.delivery-section-bg .v-data-table,
.delivery-section-bg .v-data-table * {
  color: #111111 !important;
  background: transparent !important;
}

.delivery-section-bg,
.delivery-section-bg * {
  color:#6386a9 !important;
}

.delivery-section-bg .v-chip {
  color: #fff !important;
}
</style>