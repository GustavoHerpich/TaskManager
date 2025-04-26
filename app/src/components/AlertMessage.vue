<template>
    <v-alert
      v-if="visible"
      :type="typeVuetify"
      :icon="iconVuetify"
      dismissible
      @input="visible = false"
      class="mb-4"
    >
      {{ message }}
    </v-alert>
</template>
  
<script setup lang="ts">
import { ref, computed, watch } from 'vue';

const props = defineProps<{
    message: string;
    type?: 'error' | 'warning' | 'info';
}>();

const visible = ref(true);

const typeVuetify = computed(() => {
    switch (props.type) {
        case 'warning':
            return 'warning'; 
        case 'info':
            return 'info'; 
        case 'error':
            default:
                return 'error'; 
}
});

const iconVuetify = computed(() => {
    switch (props.type) {
    case 'warning':
        return 'mdi-alert-circle-outline';
    case 'info':
        return 'mdi-information-outline';
    case 'error':
    default:
        return 'mdi-alert-outline';
}
});

watch(() => props.message, () => {
    visible.value = true;
    setTimeout(() => {
        visible.value = false;
    }, 10000);
});
</script>
  