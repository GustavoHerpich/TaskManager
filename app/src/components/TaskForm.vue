<template>
  <v-form @submit.prevent="submit">
    <v-text-field v-model="description" label="Descrição da tarefa" required />
    <v-btn type="submit" color="primary">Salvar</v-btn>
  </v-form>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useTaskStore } from '@/store/modules/taskModule';
import { parseErrorMessage } from '@/utils/parseError';

const emit = defineEmits(['saved', 'error']);
const taskStore = useTaskStore();
const description = ref('');

const submit = async () => {
  if (description.value.trim() === '') return;

  try {
    await taskStore.createTask(description.value);
    emit('saved');
    description.value = '';
  } catch (error: any) {
    emit('error', parseErrorMessage(error, 'Erro inesperado ao criar tarefa.'));
  }
};
</script>
