<template>
  <div>
    <TaskForm @saved="onTaskSaved" @error="onTaskError" @warning="onTaskWarnig"/>
    <v-divider class="my-4" />
    <v-btn @click="toggleCompleted" color="info" class="mb-4">
      {{ showCompleted ? 'Mostrar Ativas' : 'Mostrar Concluídas' }}
    </v-btn>
    <TaskItem
      v-for="task in taskStore.tasks"
      :key="task.id"
      :task="task"
      @updated="loadTasks"
      @error="onTaskError"
      @warning="onTaskWarnig"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import TaskForm from '@/components/TaskForm.vue';
import TaskItem from '@/components/TaskItem.vue';
import { useTaskStore } from '@/store/modules/taskModule';
import { useAlertStore } from '@/store/modules/alertModule';
import { parseErrorMessage } from '@/utils/parseError';

const taskStore = useTaskStore();
const alertStore = useAlertStore();
const showCompleted = ref(false);

const loadTasks = async () => {
  try {
    await taskStore.fetchTasks(showCompleted.value);
  } catch (error: any) {
    const result = parseErrorMessage(error, 'Erro ao carregar tarefas.');
    if (result.handled)
    {
      onTaskWarnig(result.message)
      return;
    }
    onTaskError(result.message)
  }
};

const toggleCompleted = async () => {
  showCompleted.value = !showCompleted.value;
  await loadTasks();
};

const onTaskSaved = () => {
  loadTasks();
};

const onTaskError = (errorMessage: string) => {
  alertStore.showAlert(errorMessage, 'error');
};

const onTaskWarnig = (errorMessage: string) => {
  alertStore.showAlert(errorMessage, 'warning');
};

onMounted(loadTasks);
</script>
