export const useTasks = () => {
    const baseURL = 'https://localhost:7269/WeatherForecast'; // Change to your ASP.NET API URL
  
    const getTasks = async () => {
      return await $fetch(baseURL);
    };
  
    const getTask = async (id: number) => {
      return await $fetch(`${baseURL}/${id}`);
    };
  
    const createTask = async (task: { title: string; description?: string }) => {
      return await $fetch(baseURL, {
        method: 'POST',
        body: task
      });
    };
  
    const updateTask = async (id: number, task: { title: string; description?: string }) => {
      return await $fetch(`${baseURL}/${id}`, {
        method: 'PUT',
        body: task
      });
    };
  
    const deleteTask = async (id: number) => {
      return await $fetch(`${baseURL}/${id}`, {
        method: 'DELETE'
      });
    };
  
    return { getTasks, getTask, createTask, updateTask, deleteTask };
  };