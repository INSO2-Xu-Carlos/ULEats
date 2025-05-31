<template>
  <v-container fluid>
    <v-row>
      <v-col cols="8">
        <h1 class="mb-4">Mis restaurantes</h1>
        <v-data-table
          :headers="restaurantHeaders"
          :items="restaurants"
          class="elevation-1"
          item-key="id"
        >
          <template #item.actions="{ item }">
            <v-btn icon @click="editRestaurant(item)">
              <v-icon>mdi-pencil</v-icon>
            </v-btn>
            <v-btn icon @click="deleteRestaurant(item)">
              <v-icon color="red">mdi-delete</v-icon>
            </v-btn>
          </template>
        </v-data-table>
      </v-col>
      <v-col cols="4">
        <h2 class="mb-4">Pedidos en curso</h2>
        <v-data-table
          :headers="orderHeaders"
          :items="orders"
          class="elevation-1"
          item-key="id"
        >
          <template #item.status="{ item }">
            <v-chip :color="item.status === 'En curso' ? 'green' : 'grey'" dark>
              {{ item.status }}
            </v-chip>
          </template>
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const restaurantHeaders = [
  { text: 'Nombre', value: 'name' },
  { text: 'Dirección', value: 'address' },
  { text: 'Acciones', value: 'actions', sortable: false },
];

const restaurants = ref([
  { id: 1, name: 'Restaurante A', address: 'Calle 1, Ciudad' },
  { id: 2, name: 'Restaurante B', address: 'Calle 2, Ciudad' },
]);

const orderHeaders = [
  { text: 'Pedido', value: 'label' },
  { text: 'Cliente', value: 'customer' },
  { text: 'Estado', value: 'status' },
];

const orders = ref([
  { id: 1, label: 'Pedido 1', customer: 'Juan', status: 'En curso' },
  { id: 2, label: 'Pedido 2', customer: 'Ana', status: 'En curso' },
]);

function editRestaurant(item: any) {
  // Lógica para editar restaurante
  alert(`Editar restaurante: ${item.name}`);
}

function deleteRestaurant(item: any) {
  // Lógica para eliminar restaurante
  const idx = restaurants.value.findIndex(r => r.id === item.id);
  if (idx !== -1) restaurants.value.splice(idx, 1);
}
</script>