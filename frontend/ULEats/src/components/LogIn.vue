<template>
    <v-text-field :label="username" :v-model = "username_value"></v-text-field>
    <v-text-field :label="password" :v-mode = "password_value"></v-text-field>

    <v-btn>
        Submit
    </v-btn>

</template>

<script lang="ts" setup>
    const username = "Username"
    const password = "Password"
    const error = ref('')

    let username_value = ""
    let password_value = ""

    const handleSubmit = () =>{
      if(!username_value || !password_value) {
          error.value = "All fields are required "
      }
    }
      if(!username.value || !password_value) =>{

    }

</script>
