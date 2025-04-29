<template>
  <v-card
    class="hoverable mb-4"
    @mouseover="hover = true"
    @mouseleave="hover = false"
    :class="{ 'hovered-card': hover }"
  >
    <v-card-title class="text-subtitle-1 font-weight-bold">
      {{ task.title }}
    </v-card-title>

    <v-card-text>
      <v-textarea
        v-model="editedDescription"
        :readonly="!isEditing || isTaskCompleted"
        density="compact"
        hide-details
        variant="solo"
        rows="2"
        auto-grow
        @click.stop="startEditing"
        @blur="saveEdit"
        class="mb-3"
      />
      <p class="text-caption">Criada em: {{ formatDate(task.createdAt) }}</p>
      <p class="text-caption" v-if="!isTaskCompleted">Tempo ativo: {{ elapsedTime }}</p>
      <p class="text-caption" v-else>Concluída em: {{ formatDate(task.completedAt || '') }}</p>
    </v-card-text>

      <v-card-actions class="justify-end">
        <v-btn
          v-if="!isTaskCompleted"
          icon
          color="success"
          @click="markTaskAsDone"
          :disabled="!isFormValid"
        >
          <v-icon>mdi-check</v-icon>
        </v-btn>

        <v-btn
          v-else
          icon
          color="warning"
          @click="markTaskAsActive"
          :disabled="!isFormValid"
        >
          <v-icon>mdi-undo</v-icon> 
        </v-btn>

        <v-btn 
          icon color="error" 
          @click="removeTask"
          >
            <v-icon>mdi-delete</v-icon>
        </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted, watch} from 'vue';
import type { Task } from '@/types/task';
import { taskService } from '@/services/taskService';
import { parseErrorMessage } from '@/utils/parseError';
import { useAlertStore } from '@/store/modules/alertModule';

const alertStore = useAlertStore();
const props = defineProps<{ task: Task }>();
const emit = defineEmits(['updated', 'error', 'warning']);
const hover = ref(false);
const isEditing = ref(false);
const isFormValid = ref(false);
const editedDescription = ref(props.task.description);
const elapsedTime = ref('');

const isTaskCompleted = computed(() => !!props.task.completedAt);

let intervalId: number;

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

const markTaskAsDone = async () => {
  if (isTaskCompleted.value) return;

  try {
    await taskService.update(props.task.id, props.task.title, props.task.description, true);
    emit('updated');
    alertStore.showAlert('Tarefa conluída com sucesso!', 'info');
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

const markTaskAsActive = async () => {
  if (!isTaskCompleted.value) return;

  try {
    await taskService.update(props.task.id, props.task.title, props.task.description, false);
    emit('updated');
    alertStore.showAlert('Tarefa reativada com sucesso!', 'info');
  } catch (error: any) {
    const result = parseErrorMessage(error, 'Erro ao reativar tarefa.');
    if (result.handled) {
      emit('warning', result.message);
      return;
    }
    emit('error', result.message);
  }
};

const removeTask = async () => {
  try {
    await taskService.delete(props.task.id);
    emit('updated');
    alertStore.showAlert('Tarefa removida com sucesso!', 'info');
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

const saveEdit = async () => {
  if (!isEditing.value || editedDescription.value.trim() === props.task.description) {
    isEditing.value = false;
    return;
  }

  try {
    await taskService.update(props.task.id, props.task.title, editedDescription.value, isTaskCompleted.value);
    emit('updated');
    alertStore.showAlert('Tarefa atualizada com sucesso!', 'info');
  } catch (error: any) {
    const result = parseErrorMessage(error, 'Erro ao criar tarefa.');
    if (result.handled)
    {
      emit('warning', result.message)
      return
    }
    emit('error', result.message);
  }
  isEditing.value = false;
};

const startEditing = () => {
  if (!isTaskCompleted.value) {
    isEditing.value = true;
  }
};

const formatDate = (dateStr: string) => {
  const date = new Date(dateStr);
  return date.toLocaleString('pt-BR', { timeZone: 'America/Sao_Paulo' });
};

onMounted(() => {
  if (!isTaskCompleted.value) {
    updateElapsedTime();
    intervalId = setInterval(updateElapsedTime, 1000);
  }
});

onUnmounted(() => {
  clearInterval(intervalId);
});

watch(editedDescription, (newVal) => {
  isFormValid.value = newVal.trim().length >= 3;
}, { immediate: true });

</script>

<style scoped>
.hoverable {
  transition: background-color 0.3s;
}
.hovered-card {
  background-color: #e3f2fd;
}
</style>
