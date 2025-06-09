<template>
  <div class="payment-page">
    <ProductCartList :cartItems="cartItems" />
    <NewAddress
      :savedAddress="savedAddress"
      @address-selected="setSelectedAddress"
    />
    <Payment @payment-success="realizarPedido" />
  </div>
</template>

<script>
import ProductCartList from "@/components/ProductCartList.vue";
import Payment from "@/components/Payment.vue";
import NewAddress from "@/components/NewAddress.vue";

export default {
  name: "PaymentPage",
  components: {
    ProductCartList,
    Payment,
    NewAddress,
  },
  data() {
    return {
      cartItems: [],
      savedAddress: "",
      selectedAddress: "",
    };
  },
  methods: {
    setSelectedAddress(address) {
      this.selectedAddress = address;
    },
    async realizarPedido() {
      const customerId = localStorage.getItem("customer_id");
      if (!customerId) {
        this.$router.push("/customer");
        return;
      }

      const subtotal = this.cartItems.reduce(
        (sum, item) => sum + (item.unitPrice * (item.quantity || 1)), 0
      );
      const deliveryFee = 2;
      const totalAmount = subtotal + deliveryFee;
      const restaurantId = this.cartItems.length > 0 ? this.cartItems[0].restaurantId || 0 : 0;

      const orderPayload = {
        customerId: Number(customerId),
        restaurantId: restaurantId,
        orderDate: new Date().toISOString(),
        status: "pendiente",
        deliveryAddress: this.selectedAddress,
        subtotal: subtotal,
        deliveryFee: deliveryFee,
        totalAmount: totalAmount,
        estimatedDeliveryTime: new Date().toISOString(),
        actualDeliveyTime: new Date().toISOString(),
        specialInstructions: "",
      };

      await fetch("/api/Order", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(orderPayload),
      });

      this.$router.push("/customer");
    },
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
    fetch(`/api/Customer/${customerId}`)
      .then(res => res.ok ? res.json() : {})
      .then(data => {
        this.savedAddress = data.direccion || "";
        this.selectedAddress = data.direccion || "";
      })
      .catch(() => {
        this.savedAddress = "";
        this.selectedAddress = "";
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