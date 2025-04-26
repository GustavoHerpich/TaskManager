import { defineStore } from 'pinia';
import { ref } from 'vue';
import type { Task } from '@/types/task';
import { taskService } from '@/services/taskService';

export const useTaskStore = defineStore('task', () => {
  const tasks = ref<Task[]>([]);

  async function fetchTasks(completed: boolean) {
    const response = await taskService.getAll(completed);
    tasks.value = response.data;
  }

  async function createTask(description: string) {
    const response = await taskService.create(description);
    tasks.value.unshift(response.data); 
  }

  return { tasks, fetchTasks, createTask };
});
