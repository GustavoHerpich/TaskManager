export function parseErrorMessage(error: any, fallbackMessage = 'Erro inesperado.') {
  const errorData = error?.response?.data;

  if (Array.isArray(errorData) && errorData.length > 0) {
    return { message: errorData[0]?.message ?? fallbackMessage, handled: true };
  }

  if (typeof errorData?.message === 'string') {
    return { message: errorData.message, handled: true };
  }

  return { message: fallbackMessage, handled: false };
}
