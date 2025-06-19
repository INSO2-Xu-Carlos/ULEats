/**
 * router/index.ts
 *
 * Automatic routes for `./src/pages/*.vue`
 */

// Composables
import { createRouter, createWebHistory } from 'vue-router'
//import { setupLayouts } from 'virtual:generated-layouts'
//import { routes } from 'vue-router/auto-routes'
import HomePage from "@/pages/HomePage.vue";
import LoginPage from "@/pages/LoginPage.vue";
import RegisterPage from "@/pages/RegisterPage.vue";
import RegisterCustomerPage from '@/pages/RegisterCustomerPage.vue';
import RegisterRestaurantPage from '@/pages/RegisterRestaurantPage.vue';
import RegisterDeliveryPage from '@/pages/RegisterDeliveryPage.vue';
import CustomerPage from '@/pages/ClientPage.vue';
import RestaurantPage from '@/pages/RestaurantPage.vue';
import DeliveryPage from '@/pages/DeliveryPage.vue';
import PaymentPage from '@/pages/PaymentPage.vue';

const routes = [
  { path: '/', component: HomePage },
  { path: "/login", component: LoginPage },
  { path: "/register", component: RegisterPage },
  { path: "/register/client", component: RegisterCustomerPage },
  { path: "/register/restaurant/:id?", component: RegisterRestaurantPage, props: true },
  { path: "/register/delivery", component: RegisterDeliveryPage },
  { path: "/customer", component: CustomerPage },
  { path: "/restaurant", component: RestaurantPage },
  { path: "/delivery", component: DeliveryPage },
  { path: "/customer/payment", component: PaymentPage },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach((to, from, next) => {
  const userId = localStorage.getItem('user_id');
  const customerId = localStorage.getItem('customer_id');
  const restaurantId = localStorage.getItem('restaurant_id');
  const deliveryId = localStorage.getItem('delivery_id');

  if (!userId && to.path !== '/login' && to.path !== '/register' && to.path !== '/') {
    return next('/');
  }

  if (customerId && !to.path.startsWith('/customer') && to.path !== '/login' && to.path !== '/register') {
    return next('/customer');
  }

  if (restaurantId && !to.path.startsWith('/restaurant') && to.path !== '/login' && to.path !== '/register') {
    return next('/restaurant');
  }

  if (deliveryId && !to.path.startsWith('/delivery') && to.path !== '/login' && to.path !== '/register') {
    return next('/delivery');
  }

  next();
});

// Workaround for https://github.com/vitejs/vite/issues/11804
router.onError((err, to) => {
  if (err?.message?.includes?.('Failed to fetch dynamically imported module')) {
    if (!localStorage.getItem('vuetify:dynamic-reload')) {
      console.log('Reloading page to fix dynamic import error')
      localStorage.setItem('vuetify:dynamic-reload', 'true')
      location.assign(to.fullPath)
    } else {
      console.error('Dynamic import error, reloading page did not fix it', err)
    }
  } else {
    console.error(err)
  }
})

router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload')
})

export default router
