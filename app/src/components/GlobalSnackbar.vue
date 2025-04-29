<template>
  <v-snackbar
    v-model="alertStore.visible"
    :color="alertStore.type"
    timeout="3000"
    top
    right
  >
    {{ alertStore.message }}
  </v-snackbar>
</template>

<script setup lang="ts">
import { useAlertStore } from '@/store/modules/alertModule';

const alertStore = useAlertStore();
</script>
