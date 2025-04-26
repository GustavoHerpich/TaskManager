<template>
  <div>
    <AlertMessage
      v-if="alert.message"
      :message="alert.message"
      :type="alert.type"
    />

    <TaskForm @saved="handleSaved" @error="handleError"/>
    <v-divider class="my-4"></v-divider>

    <v-btn @click="toggleCompleted" color="info" class="mb-4">
      {{ showCompleted ? 'Mostrar Ativas' : 'Mostrar Concluídas' }}
    </v-btn>

    <TaskItem
      v-for="task in taskStore.tasks"
      :key="task.id"
      :task="task"
      @updated="loadTasks"
      @error="handleError"
    />

  </div>
</template>

<script setup lang="ts">
import TaskForm from '@/components/TaskForm.vue';
import TaskItem from '@/components/TaskItem.vue';
import AlertMessage from '@/components/AlertMessage.vue';
import { useTaskStore } from '@/store/modules/taskModule';
import { onMounted, ref } from 'vue';
import { parseErrorMessage } from '@/utils/parseError';

const taskStore = useTaskStore();
const showCompleted = ref(false);

const alert = ref<{ message: string; type: 'info' | 'warning' | 'error' } | { message: '', type: 'info' }>({ message: '', type: 'info' });

const loadTasks = async () => {
  try {
    await taskStore.fetchTasks(showCompleted.value);
  } catch (error: any) {
    alert.value = {
      message: parseErrorMessage(error, 'Erro inesperado ao carregar tarefas.'),
      type: 'warning',
    };
  }
};

const toggleCompleted = async () => {
  showCompleted.value = !showCompleted.value;
  await loadTasks();
};

const handleSaved = () => {
  loadTasks();
  alert.value = {
    message: 'Tarefa criada com sucesso!',
    type: 'info',
  };
};

const handleError = (errorMessage: string) => {
  alert.value = {
    message: errorMessage,
    type: 'warning',
  };
};

onMounted(loadTasks);
</script>
