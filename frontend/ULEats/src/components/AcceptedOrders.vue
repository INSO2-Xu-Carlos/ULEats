<template>
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