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
        </v-data-table>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "MyRestaurantPage",
  data() {
    return {
      restaurants: [],
      orders: [],
      restaurantHeaders: [
        { text: "Name", value: "name" },
        { text: "Address", value: "address" },
        { text: "Actions", value: "actions", sortable: false },
      ],
      orderHeaders: [
        { text: "Client", value: "customerName" },
        { text: "Total", value: "total" },
        { text: "Status", value: "status" },
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
    this.orders = [];
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
},
  mounted() {
    this.fetchRestaurants();
    this.fetchOrders();
  },
};
</script>

