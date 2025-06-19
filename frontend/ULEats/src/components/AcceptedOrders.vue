<template>
  <v-card>
    <v-card-title>Pedidos aceptados</v-card-title>
    <v-data-table
      :headers="orderHeaders"
      :items="acceptedOrders"
      item-key="id"
    >
      <template #item.status="{ item }">
        <v-chip
          :color="item.status === 'En camino' ? 'blue' : item.status === 'Entregado' ? 'green' : 'orange'"
          dark
        >
          {{ item.status }}
        </v-chip>
      </template>
      <template #item.actions="{ item }">
        <v-btn
          color="info"
          v-if="item.status === 'En curso'"
          @click="$emit('on-way-order', item)"
        >
          En camino
        </v-btn>
        <v-btn
          color="success"
          v-if="item.status === 'En camino'"
          @click="$emit('delivered-order', item)"
        >
          Entregado
        </v-btn>
        <v-btn color="error" @click="$emit('unassign-order', item)">
          Desasignar
        </v-btn>
      </template>
    </v-data-table>
  </v-card>
</template>

<script>
export default {
  name: "AcceptedOrders",
  props: {
    acceptedOrders: {
      type: Array,
      required: true,
    },
    orderHeaders: {
      type: Array,
      required: true,
    },
  },
};
</script>