<template>
  <v-container>
    <h1 class="mb-6">Repartidor</h1>
    <v-row>
      <v-col cols="12" md="6">
        <v-card class="mb-6">
          <v-card-title>Pedidos disponibles</v-card-title>
          <v-card-text>
            <v-select
              v-model="selectedOrder"
              :items="availableOrders"
              item-title="label"
              item-value="id"
              label="Selecciona un pedido para aceptar"
              return-object
              clearable
            ></v-select>
            <v-btn
              :disabled="!selectedOrder"
              color="primary"
              class="mt-2"
              @click="acceptOrder"
            >
              Aceptar pedido
            </v-btn>
          </v-card-text>
        </v-card>
        <v-card>
          <v-card-title>Pedidos aceptados</v-card-title>
          <v-data-table
            :headers="orderHeaders"
            :items="acceptedOrders"
            class="elevation-1"
            item-key="id"
          >
            <template #item.status="{ item }">
              <v-chip :color="item.status === 'En curso' ? 'green' : 'grey'" dark>
                {{ item.status }}
              </v-chip>
            </template>
          </v-data-table>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue';

type Order = {
  id: number;
  label: string;
  customer: string;
  status: string;
};

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
    // Cambia el estado y mueve el pedido a aceptados
    const order = { ...selectedOrder.value, status: 'En curso' };
    acceptedOrders.value.push(order);
    availableOrders.value = availableOrders.value.filter(o => o.id !== order.id);
    selectedOrder.value = null;
  }
}
</script>