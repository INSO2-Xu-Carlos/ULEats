<template>
  <div class="new-address">
    <label>
      <input
        type="radio"
        value="saved"
        v-model="selectedOption"
      />
      Usar mi dirección guardada: <strong>{{ savedAddress }}</strong>
    </label>
    <br />
    <label>
      <input
        type="radio"
        value="new"
        v-model="selectedOption"
      />
      Usar una nueva dirección
    </label>
    <div v-if="selectedOption === 'new'" class="new-address-field">
      <input
        type="text"
        v-model="newAddress"
        placeholder="Introduce la nueva dirección"
      />
    </div>
    <button class="confirm-btn" @click="confirmAddress">Confirmar dirección</button>
  </div>
</template>

<script>
export default {
  name: "NewAddress",
  props: {
    savedAddress: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      selectedOption: "saved",
      newAddress: "",
    };
  },
  methods: {
    confirmAddress() {
      const address =
        this.selectedOption === "new" && this.newAddress
          ? this.newAddress
          : this.savedAddress;
      if (!address) {
        alert("Por favor, introduce una dirección.");
        return;
      }
      this.$emit("address-selected", address);
    },
  },
};
</script>

<style scoped>
.new-address {
  margin-bottom: 20px;
}
.new-address-field {
  margin-top: 10px;
  margin-bottom: 10px;
}
.confirm-btn {
  margin-top: 10px;
  background: #007bff;
  color: #fff;
  border: none;
  border-radius: 5px;
  padding: 8px 16px;
  font-size: 15px;
  cursor: pointer;
}
.confirm-btn:hover {
  background: #0056b3;
}
</style>