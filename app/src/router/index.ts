import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import TaskList from '@/views/TaskList.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'taskList',
    component: TaskList,
  },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
