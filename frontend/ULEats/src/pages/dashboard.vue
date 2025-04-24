<template>
  <v-container>
    <v-card class="pa-4">
      <v-card-title>Welcome to the Panel</v-card-title>
      <v-card-text>Protected content here.</v-card-text>
      <v-btn color="error" @click="logout">Log out</v-btn>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores/auth'
import { useRouter } from 'vue-router'

const auth = useAuthStore()
const router = useRouter()

const logout = () => {
  auth.logout()
  router.push('/login')
}
</script>

<script lang="ts">
export default {
  layout: 'default',
  meta: {
    requiresAuth: true,
  },
}
</script>
