<template>
  <div class="payment-page">
    <ProductCartList :cartItems="cartItems" />
    <Payment />
  </div>
</template>

<script>
import ProductCartList from "@/components/ProductCartList.vue";
import Payment from "@/components/Payment.vue";

export default {
  name: "PaymentPage",
  components: {
    ProductCartList,
    Payment,
  },
  data() {
    return {
      cartItems: [],
    };
  },
  mounted() {
    const customerId = localStorage.getItem("customer_id");
    if (!customerId) {
      this.cartItems = [];
      return;
    }
    fetch(`/api/OrderItem/byCustomer/${customerId}`)
      .then(res => res.ok ? res.json() : [])
      .then(data => {
        this.cartItems = data || [];
      })
      .catch(() => {
        this.cartItems = [];
      });
  },
};
</script>

<style scoped>
.payment-page {
  display: flex;
  flex-direction: column;
  gap: 30px;
  max-width: 500px;
  margin: 0 auto;
  padding: 30px 0;
}
</style>