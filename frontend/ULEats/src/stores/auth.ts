import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || '',
    user: null as null | { name: string },
  }),
  actions: {
    async login(email: string, password: string) {
      // Simulación de login (reemplaza por tu llamada a API real)
      if (email === 'admin@uleats.com' && password === '1234') {
        this.token = 'fake-jwt-token'
        this.user = { name: 'Admin' }
        localStorage.setItem('token', this.token)
      } else {
        throw new Error('Credenciales inválidas')
      }
    },
    logout() {
      this.token = ''
      this.user = null
      localStorage.removeItem('token')
    },
  },
  getters: {
    isAuthenticated: state => !!state.token,
  },
})
