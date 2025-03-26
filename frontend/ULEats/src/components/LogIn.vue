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

    let username_value = ""
    let password_value = ""

</script>
