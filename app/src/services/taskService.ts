import axios from 'axios';
import type { Task } from '@/types/task';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL
});

export const taskService = {
  getAll(completed: boolean) {
    return api.get<Task[]>(`/completed/${completed}`);
  },
  getById(id: string) {
    return api.get<Task>(`/${id}`);
  },
  create(description: string) {
    return api.post<Task>('', { description });
  },
  update(id: string, description: string, markAsCompleted: boolean) {
    return api.put<Task>(`/${id}`, { description, markAsCompleted });
  },
  delete(id: string) {
    return api.delete(`/` + id);
  }
};