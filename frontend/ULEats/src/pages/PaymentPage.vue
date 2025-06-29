<template>
  <div class="payment-page">
    <ProductCartList :cartItems="cartItems" />
    <NewAddress
      :savedAddress="savedAddress"
      @address-selected="setSelectedAddress"
    />
    <Payment @payment-success="newOrder" />
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
    async newOrder() {
      const customerId = localStorage.getItem("customer_id");
      if (!customerId) {
        this.$router.push("/customer");
        return;
      }

      const productId = this.cartItems[0].productId || this.cartItems[0].id;

      let restaurantId = null;
      try {
        const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
        const res = await fetch(`${baseUrl}/Product/${productId}`);
        if (res.ok) {
          const product = await res.json();
          restaurantId = product.restaurantId;
        }
      } catch {
        restaurantId = null;
      }

      if (!restaurantId) {
        alert("No se ha encontrado el restaurante del pedido.");
        return;
      }

      const subtotal = this.cartItems.reduce(
        (sum, item) => sum + (item.unitPrice * (item.quantity || 1)), 0
      );
      const deliveryFee = 2;
      const totalAmount = subtotal + deliveryFee;

      if (!this.selectedAddress){
        this.selectedAddress = this.savedAddress;
      }

      const orderPayload = {
        restaurantId: restaurantId,
        customerId: Number(customerId),
        deliveryId: null,
        orderDate: new Date().toISOString(),
        status: "pending",
        deliveryAddress: this.selectedAddress,
        subtotal: subtotal,
        deliveryFee: deliveryFee,
        totalAmount: totalAmount,
        estimatedDeliveryTime: new Date().toISOString(),
        actualDeliveryTime: new Date().toISOString(),
        specialInstructions: "",
      };

      const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
        
      const orderResponse = await fetch(`${baseUrl}/Order`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(orderPayload),
      });

      if (orderResponse.ok) {
        const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
        await fetch(`${baseUrl}/OrderItem/byCustomer/${customerId}`, {
          method: "DELETE",
        });

        this.$router.push("/customer");
      }
    },
  },
   mounted() {
    const customerId = localStorage.getItem("customer_id");
    if (!customerId) {
      this.cartItems = [];
      return;
    }
    const baseUrl = import.meta.env.PROD
        ? 'https://uleats-8xnb.onrender.com'
        : '/api';
        
    fetch(`${baseUrl}/OrderItem/byCustomer/${customerId}`)
      .then(res => res.ok ? res.json() : [])
      .then(data => {
        this.cartItems = data || [];
      })
      .catch(() => {
        this.cartItems = [];
      });
    fetch(`${baseUrl}/Customer/${customerId}`)
      .then(res => res.ok ? res.json() : {})
      .then(data => {
        this.savedAddress = data.address || "";
        this.selectedAddress = data.address || "";
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
  margin: 40px auto;
  padding: 32px 24px;
  background: #f6f8fa;
  border-radius: 16px;
  box-shadow: 0 2px 12px rgba(25, 118, 210, 0.08);
}

.payment-page,
.payment-page * {
  color: #111 !important;
}

.cart-total,
.total-amount,
.total {
  font-size: 1.2em;
  font-weight: bold;
  color: #1976d2 !important;
  margin-top: 12px;
}
</style>