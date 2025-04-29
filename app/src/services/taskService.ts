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
  create(title: string, description: string) {
    return api.post<Task>('', { title, description });
  },
  update(id: string, title: string, description: string, markAsCompleted: boolean) {
    return api.put<Task>(`/${id}`, { title, description, markAsCompleted });
  },
  delete(id: string) {
    return api.delete(`/` + id);
  }
};