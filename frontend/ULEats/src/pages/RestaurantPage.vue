<template>
  <v-container fluid>
    <v-row>
      <v-col cols="8">
        <div class="d-flex justify-space-between align-center mb-4">
          <h1> My restaurants</h1>
          <div class="d-flex align-center">
            <v-btn color="primary" @click="addRestaurant" icon>
              <v-icon> mdi-plus</v-icon>
            </v-btn>
            <LogoutButton class="ml-4" />
          </div>
        </div>
        
        <v-data-table
          :headers="restaurantHeaders"
          :items="restaurants"
          class="elevation-1"
          item-key="id"
        >
          <template #item.actions="{ item }">
            <v-btn icon @click="openProductDialog(item)">
              <v-icon color="green"> mdi-plus-box</v-icon>
            </v-btn>
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
        <h2 class="mb-4">Orders</h2>
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
          <template #item.total="{ item }">
            {{ formatCurrency(item.total) }}
          </template>
          <template #item.actions="{ item }">
            <v-btn icon @click="viewOrder(item)">
              <v-icon>mdi-eye</v-icon>
            </v-btn>
          </template>
        </v-data-table>
        <div v-if="selectedOrder" class="mt-4 pa-4 elevation-2 rounded bg-grey-lighten-4">
          <h3>Order Details</h3>
          <p><strong>Client:</strong> {{ selectedOrder.customerName }}</p>
          <p><strong>Total:</strong> {{ formatCurrency(selectedOrder.total) }}</p>
          <p><strong>Status:</strong> {{ selectedOrder.status }}</p>
          <p><strong>Restaurant:</strong> {{ selectedOrder.restaurantName }}</p>
          <p><strong>Address:</strong> {{ selectedOrder.restaurantAddress }}</p>
          <v-btn color="secondary" class="mt-2" @click="selectedOrder = null">
            Close
          </v-btn>
        </div>
        <v-dialog v-model="showProductDialog" max-width="800px">
          <v-card>
            <v-card-title class="d-flex justify-space-between">
              Productos de {{ selectedRestaurant?.name }}
              <v-btn icon @click="closeProductDialog"><v-icon>mdi-close</v-icon></v-btn>
            </v-card-title>
            <v-card-text>
              <v-data-table
                :headers="productHeaders"
                :items="products"
                item-key="productId"
              >
                <template #item.actions="{ item }">
                  <v-btn icon @click="editProduct(item)">
                    <v-icon>mdi-pencil</v-icon>
                  </v-btn>
                  <v-btn icon @click="deleteProduct(item)">
                    <v-icon color="red">mdi-delete</v-icon>
                  </v-btn>
                </template>
              </v-data-table>
              <v-btn color="primary" class="mt-3" @click="newProduct">Añadir Producto</v-btn>
            </v-card-text>
          </v-card>
        </v-dialog>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { ca } from 'vuetify/locale';

export default {
  name: "MyRestaurantPage",
  data() {
    return {
      restaurants: [],
      selectedOrder: null,
      showProductDialog: false,

      orders: [],
      restaurantHeaders: [
        { text: "Name", value: "name" },
        { text: "Address", value: "address" },
        { text: "Actions", value: "actions", sortable: false },
      ],
      orderHeaders: [
        { text: "Client", value: "customerName" },
        { text: "Address", value: "address "},
        { text: "Restaurant", value: "restaurantName" },
        { text: "Total", value: "total" },
        { text: "Status", value: "status" },
        { text: "Actions", value: "actions", sortable: false },
      ],
      products: [],
      productHeaders: [
        { text: 'Nombre', value: 'name' },
        { text: 'Precio', value: 'price' },
        { text: 'Descripción', value: 'description' },
        { text: 'Acciones', value: 'actions', sortable: false },
      ],
    };
  },
  methods: {
  async fetchRestaurants() {
    const userId = localStorage.getItem("user_id");

    try {
      const response = await fetch(`/api/Restaurant/ByUser/${userId}`);
      if (response.ok) {
        const data = await response.json();

        this.restaurants = data.map(r => ({
          id: r.restaurantId,
          name: r.name,
          address: r.address
        }));
      } else {
        this.restaurants = [];
      }
    } catch (err) {
      this.restaurants = [];
    }
  },
  async fetchOrders() {
    const userId = localStorage.getItem("user_id");

    try {
      const response = await fetch(`/api/Order/ByOwner/${userId}`);
      if (!response.ok) {
        this.orders = [];
        return;
      }

      const data = await response.json();
      this.orders = data.map(o => ({
      id: o.orderId,
      customerName: o.customerName,
      total: o.total,
      status: o.status,
      restaurantName: o.restaurantName,
      restaurantAddress: o.restaurantAddress
}));
    } catch (err) {
      this.orders = [];
    }
  },

  addRestaurant() {
    this.$router.push('/register/restaurant');
  },

  editRestaurant(restaurant) {
    this.$router.push(`/register/restaurant/${restaurant.id}`);
  },

  async deleteRestaurant(restaurant) {
    if (confirm(`Are you sure you want to delete this restaurant: "${restaurant.name}"?`)) {
      try {
        const response = await fetch(`/api/Restaurant/${restaurant.id}`, {
          method: "DELETE",
        });

        if (response.ok) {
          this.fetchRestaurants();
        } else {
          throw new Error("Error deleting restaurant");
        }
      } catch (err) {
        console.error("Delete error:", err);
      }
    }
  },
  formatCurrency(value) {
    if( typeof value !== 'number') return value;
    return value.toLocaleString("es-ES", {
      style: 'currency',
      currency: 'EUR',
    });
  },
  viewOrder(order) {
    this.selectedOrder = order;
  },
  openProductDialog(restaurant) {
    this.selectedRestaurant = restaurant;
    this.fetchProducts(restaurant.id);
    this.showProductDialog = true;
  },
  closeProductDialog() {
    this.showProductDialog = false;
    this.selectedRestaurant = null;
    this.products = [];
  },
  async fetchProducts(restaurantId) {
    try {
      const response = await fetch(`/api/Product/ByRestaurant/${restaurantId}`);
      if (response.ok) {
        this.products = await response.json();
      } else {
        this.products = [];
      }
    } catch (err) {
      console.error(err);
      this.products = [];
    }
  },
  async deleteProduct(product) {
    if (confirm(`¿Eliminar producto "${product.name}"?`)) {
      try {
        const response = await fetch(`/api/Product/${product.productId}`, {
          method: 'DELETE'
        });
        if (response.ok) {
          this.fetchProducts(this.selectedRestaurant.id);
        } else {
          alert('Error al eliminar');
        }
      } catch (err) {
        console.error(err);
      }
    }
  },
  newProduct() {
    this.$router.push(`/register/product/new?restaurantId=${this.selectedRestaurant.id}`);
  },

  editProduct(product) {
    this.$router.push(`/register/product/${product.productId}`);
  },


},


  mounted() {
    this.fetchRestaurants();
    this.fetchOrders();
  },
};
</script>

<style scoped>
.v-data-table,
.v-data-table * {
  color: #6386a9 !important;
}

.v-chip {
  color: #fff !important;
}
</style>