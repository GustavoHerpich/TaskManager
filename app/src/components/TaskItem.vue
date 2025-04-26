<template>
  <v-card
    class="hoverable"
    @mouseover="hover = true"
    @mouseleave="hover = false"
    :class="{ 'hovered-card': hover }"
  >
    <v-card-text>
        <v-text-field
          class="mb-4"
          v-model="editedDescription"
          :readonly="!isEditing || isTaskCompleted"
          density="compact"
          hide-details
          variant="solo"
          @click.stop="startEditing"
          @blur="saveEdit"
        />
      <p></p>
      <p>Criada em: {{ formatDate(task.createdAt) }}</p>
      <p v-if="!isTaskCompleted">Tempo ativo: {{ elapsedTime }}</p>
      <p v-else>Concluída em: {{ formatDate(task.completedAt || '') }}</p>
    </v-card-text>
    <v-card-actions>
      <v-btn @click="markAsDone" :disabled="isTaskCompleted" color="success">Concluir</v-btn>
      <v-btn @click="remove" color="error">Excluir</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import { onMounted, onUnmounted, ref, computed  } from 'vue';
import type { Task } from '@/types/task';
import { taskService } from '@/services/taskService';
import { parseErrorMessage } from '@/utils/parseError';

const props = defineProps<{ task: Task }>();
const emit = defineEmits(['updated', 'error']);

const elapsedTime = ref('');
const hover = ref(false);
const isEditing = ref(false);
const editedDescription = ref(props.task.description);

const isTaskCompleted = computed(() => !!props.task.completedAt && props.task.completedAt !== "");

let interval: number;

function updateElapsedTime() {
  const createdUtc = new Date(props.task.createdAt); 
  const localOffsetMs = createdUtc.getTimezoneOffset() * 60000; 
  const createdLocal = new Date(createdUtc.getTime() - localOffsetMs);

  const now = new Date(); 

  const diff = Math.floor((now.getTime() - createdLocal.getTime()) / 1000);

  const hours = Math.floor(diff / 3600);
  const minutes = Math.floor((diff % 3600) / 60);
  const seconds = diff % 60;

  elapsedTime.value = `${hours}h ${minutes}m ${seconds}s`;
}

onMounted(() => {
  if (!isTaskCompleted.value) {
    updateElapsedTime();
    interval = setInterval(updateElapsedTime, 1000);
  }
});

onUnmounted(() => {
  clearInterval(interval);
});

const markAsDone = async () => {
  if (!isTaskCompleted.value) {
    try {
      await taskService.update(props.task.id, props.task.description, true);
      emit('updated');
    } catch (error: any) {
      emit('error', parseErrorMessage(error, 'Erro ao concluir tarefa.'));
    }
  }
};

const remove = async () => {
  try {
    await taskService.delete(props.task.id);
    emit('updated');
  } catch (error: any) {
    emit('error', parseErrorMessage(error, 'Erro ao excluir tarefa.'));
  }
};

function formatDate(dateStr: string) {
  const date = new Date(dateStr);
  return date.toLocaleString('pt-BR', { timeZone: 'America/Sao_Paulo' });
}

const startEditing = () => {
  if (!isTaskCompleted.value) {
    isEditing.value = true;
  }
};

const saveEdit = async () => {
  if (editedDescription.value.trim() && editedDescription.value !== props.task.description) {
    try {
      await taskService.update(props.task.id, editedDescription.value, isTaskCompleted.value);
      emit('updated');
    } catch (error: any) {
      emit('error', parseErrorMessage(error, 'Erro inesperado ao editar tarefa.'));
    }
  }
  isEditing.value = false;
};
</script>

<style scoped>
.hoverable {
  transition: background-color 0.3s;
}
.hovered-card {
  background-color: #e3f2fd;
}
.mb-4 {
  margin-bottom: 16px;
}
</style>
