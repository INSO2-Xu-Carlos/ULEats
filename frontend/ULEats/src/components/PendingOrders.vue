<template>
  <v-card class="mb-6">
    <v-card-title>Pedidos disponibles</v-card-title>
    <v-card-text>
      <v-select
        v-model="localSelectedOrder"
        :items="availableOrders"
        item-title="label"
        item-value="id"
        label="Selecciona un pedido para aceptar"
        return-object
        clearable
        @update:modelValue="updateSelectedOrder"
      ></v-select>
      <v-btn
        :disabled="!localSelectedOrder"
        color="primary"
        class="mt-2"
        @click="acceptOrder"
      >
        Aceptar pedido
      </v-btn>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  name: "PendingOrders",
  props: {
    availableOrders: {
      type: Array,
      required: true,
    },
    selectedOrder: {
      type: Object,
      default: null,
    },
  },
  data() {
    return {
      localSelectedOrder: this.selectedOrder,
    };
  },
  watch: {
    selectedOrder(newVal) {
      this.localSelectedOrder = newVal;
    },
  },
  methods: {
    updateSelectedOrder(order) {
      this.$emit("update:selectedOrder", order);
    },
    acceptOrder() {
      this.$emit("accept-order");
    },
  },
};
</script>