import { defineStore } from 'pinia';
import { ref } from 'vue';

type AlertType = 'info' | 'warning' | 'error';

export const useAlertStore = defineStore('alert', () => {
  const message = ref('');
  const type = ref<AlertType>('info');
  const visible = ref(false);

  function showAlert(newMessage: string, newType: AlertType = 'info') {
    message.value = newMessage;
    type.value = newType;
    visible.value = true;

    setTimeout(() => {
      visible.value = false;
    }, 3000);
  }

  function hideAlert() {
    visible.value = false;
  }

  return { message, type, visible, showAlert, hideAlert };
});
