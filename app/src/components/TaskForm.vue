<template>
  <v-card class="pa-4 mb-6">
    <v-card-title class="text-h6 font-weight-bold">Nova Tarefa</v-card-title>
    <v-form @submit.prevent="submitForm">
      <v-text-field v-model="title" label="Título da tarefa" required class="mb-3" />
      <v-textarea v-model="description" label="Descrição" rows="2" auto-grow required class="mb-4" />
      <v-btn type="submit" color="primary" block>Salvar</v-btn>
    </v-form>
  </v-card>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useTaskStore } from '@/store/modules/taskModule';
import { parseErrorMessage } from '@/utils/parseError';
import { useAlertStore } from '@/store/modules/alertModule';

const alertStore = useAlertStore();
const emit = defineEmits(['saved', 'error', 'warning']);
const taskStore = useTaskStore();
const title = ref('');
const description = ref('');

const submitForm = async () => {
  if (!title.value.trim() || !description.value.trim()) return;

  try {
    await taskStore.createTask(title.value, description.value);
    title.value = '';
    description.value = '';
    emit('saved');
    alertStore.showAlert('Tarefa criada com sucesso!', 'info');
  } catch (error: any) {
    const result = parseErrorMessage(error, 'Erro ao criar tarefa.');
    if (result.handled)
    {
      emit('warning', result.message)
      return
    }
    emit('error', result.message);
  }
};
</script>
