<template>
  <div class="payment-container">
    <h2>Selecciona método de pago</h2>
    <div class="payment-methods">
      <label>
        <input type="radio" value="cash" v-model="paymentType" />
        Efectivo
      </label>
      <label>
        <input type="radio" value="card" v-model="paymentType" />
        Tarjeta
      </label>
    </div>

    <div v-if="paymentType === 'card'" class="card-fields">
      <div>
        <label>Número de tarjeta</label>
        <input type="text" v-model="cardNumber" maxlength="16" placeholder="1234 5678 9012 3456" />
      </div>
      <div>
        <label>CCV</label>
        <input type="text" v-model="ccv" maxlength="3" placeholder="123" />
      </div>
      <div>
        <label>Fecha de caducidad</label>
        <input type="text" v-model="expiry" maxlength="5" placeholder="MM/AA" />
      </div>
      <div>
        <label>Nombre en la tarjeta</label>
        <input type="text" v-model="cardName" placeholder="Nombre completo" />
      </div>
    </div>
    <button class="pay-btn" @click="pay">Pagar</button>
  </div>
</template>

<script>
export default {
  name: "Payment",
  data() {
    return {
      paymentType: "cash",
      cardNumber: "",
      ccv: "",
      expiry: "",
      cardName: "",
    };
  },
  methods: {
    pay() {
      if (this.paymentType === "card") {
        if (
          !this.cardNumber ||
          !this.ccv ||
          !this.expiry ||
          !this.cardName
        ) {
          alert("Por favor, completa todos los campos de la tarjeta.");
          return;
        }
      }
      alert(
        this.paymentType === "cash"
          ? "Pago en efectivo seleccionado."
          : "Pago con tarjeta realizado."
      );
      this.$emit("payment-success");
    },
  },
};
</script>

<style scoped>
.payment-container {
  padding: 20px;
  max-width: 400px;
  margin: 0 auto;
}
.payment-methods {
  display: flex;
  gap: 20px;
  margin-bottom: 20px;
}
.card-fields > div {
  margin-bottom: 12px;
}
.pay-btn {
  width: 100%;
  background: #007bff;
  color: #fff;
  border: none;
  border-radius: 5px;
  padding: 10px 0;
  font-size: 16px;
  cursor: pointer;
  transition: background 0.2s;
}
.pay-btn:hover {
  background: #0056b3;
}
</style>