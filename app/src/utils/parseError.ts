export function parseErrorMessage(error: any, fallbackMessage = 'Erro inesperado.') {
    const errorData = error?.response?.data;
  
    if (Array.isArray(errorData) && errorData.length > 0) {
      return errorData[0].message ?? fallbackMessage;
    }
  
    if (typeof errorData?.message === 'string') {
      return errorData.message;
    }
  
    return fallbackMessage;
  }
  