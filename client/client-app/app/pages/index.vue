<template>
  <div class="container">
    <h1>Nuxt 3 + ASP.NET Tasks</h1>

    <!-- Add Task Form -->
    <form @submit.prevent="addTask">
      <input v-model="newTaskTitle" placeholder="New Task Title" />
      <button type="submit">Add</button>
    </form>

    <ul>
      <li v-for="task in tasks" :key="task.id">
        <input v-model="task.title" @blur="saveTask(task)" />
        <button @click="removeTask(task.id)">❌</button>
      </li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useTasks } from '@/composables/useTasks';

const { getTasks, createTask, updateTask, deleteTask } = useTasks();

const tasks = ref<any[]>([]);
const newTaskTitle = ref('');

onMounted(async () => {
  tasks.value = await getTasks();
});

async function addTask() {
  if (!newTaskTitle.value.trim()) return;
  const task = await createTask({ title: newTaskTitle.value });
  tasks.value.push(task);
  newTaskTitle.value = '';
}

async function saveTask(task: any) {
  await updateTask(task.id, { title: task.title });
}

async function removeTask(id: number) {
  await deleteTask(id);
  tasks.value = tasks.value.filter(t => t.id !== id);
}
</script>

<style scoped>
.container {
  max-width: 500px;
  margin: auto;
}
input {
  margin: 5px;
}
button {
  margin-left: 5px;
}
</style>
