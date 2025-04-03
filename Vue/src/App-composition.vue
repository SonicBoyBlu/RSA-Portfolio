<script setup>
// Composition API
import { ref, onMounted } from "vue";

let name = ref("John Doe");
const tasks = ref(["One", "Two", "Three"]);
//const statuses = ref(["Pending","Active", "Inactive"]);
const statuses = ref({
  //status: { id: 0, label: "Inactive", color: "F00" },
  current: { id: 0, label: "Inactive", color: "#F00" },
  options: [
    { id: 0, label: "Inactive", color: "#F00" },
    { id: 1, label: "Pending", color: "#FF0" },
    { id: 2, label: "Active", color: "#0F0" },
  ],
});
let status = ref("Active");
let newTask = ref("");

const radios = ref([
  { label: "R1", value: "r1" },
  { label: "R2", value: "r2" },
  { label: "R3", value: "r3" },
]);
let radio = ref("Unset");
const checks = ref([
  { label: "C1", value: "c1" },
  { label: "C2", value: "c2" },
  { label: "C3", value: "c3" },
]);
let check = ref([]);

const toggleStatus = () => {
  if (status.value === "Active") status.value = "Pending";
  else if (status.value === "Pending") status.value = "Inactive";
  else status.value = "Active";
};
const addTask = () => {
  if (newTask.value.trim() !== "") {
    tasks.value.push(newTask.value);
    newTask.value = "";
  }
};
const removeTask = (i) => {
  tasks.value.splice(i, 1);
};

const updateStatus = () => {
  console.log(statuses.value.current);
};

onMounted(async () => {
  // Load tasks
  try {
    const res = await fetch("https://jsonplaceholder.typicode.com/todos");
    const data = await res.json();
    tasks.value = data.splice(0, 10).map((task) => task.title);
  } catch (error) {
    console.log("Error fetching tasks");
  }

  // Load presets
  try {
    statuses.value.current = statuses.value.options[0];
    setTimeout(() => {
      statuses.value.current = statuses.value.options[1];
    }, 3000);
  } catch (error) {
    console.log(error);
  }
});
</script>

<template>
  <div id="app-demo">
    <h1 class="text-2xl">Vue Jobs for {{ name }}*</h1>
    <br />
    <h3>Status Toggle/Loop</h3>
    <p>
      <strong>Actions: </strong>
      <span v-if="status === 'Active'" style="color: green">Active</span>
      <span v-else-if="status === 'Pending'" style="color: orange"
        >Pending</span
      >
      <span v-else style="color: red">Inactive</span>
    </p>
    <button @click="toggleStatus">Toggle Status</button> <br /><br />

    <p>Dropdown:</p>
    StatusID:
    <span :style="{ color: statuses.current.color }">{{
      statuses.current.label
    }}</span>
    <select v-model="statuses.current" @change="updateStatus">
      <option
        v-for="status in statuses.options"
        :key="status.id"
        :value="status"
      >
        {{ status.label }}
      </option>
    </select>

    <h3>Controls</h3>
    <ul>
      <li>Checkbox: {{ check }}</li>
      <li>
        <ul>
          <li v-for="chk in checks" :key="chk.value">
            <label>
              <input type="checkbox" :value="chk.value" v-model="check" />
              Checkbox - {{ chk.label }}
            </label>
          </li>
        </ul>
      </li>
      <li>Radios: {{ radio }}</li>
      <li>
        <ul>
          <li v-for="rdo in radios" :key="rdo.value">
            <label>
              <input type="radio" :value="rdo.value" v-model="radio" />
              Radio - {{ rdo.label }}
            </label>
          </li>
        </ul>
      </li>
    </ul>
    <br />

    <h3>Tasks:</h3>
    <form @submit.prevent="addTask">
      <label for="newTask">Add Task</label>
      <input
        id="newTask"
        name="newTask"
        type="text"
        placeholder="New Task"
        v-model="newTask"
        @change="taskAddChange"
      />
      <button type="submit">Add</button>
    </form>
    <ol>
      <li v-for="(task, i) in tasks" :key="task">
        <span>Item: {{ task }}</span>
        <button @click="removeTask(i)">x</button>
      </li>
      <li>... {{ newTask }}</li>
    </ol>
  </div>
</template>

<style type="text/css" lang="scss" scoped>
#app-demo {
  background-color: #333 !important;
  color: #fff;
  padding: 2em;

  select,
  input,
  textarea,
  button {
    color: #333;
    background-color: #fff;
    padding: 0.25em;
    border-radius: 0.25em;
    margin: 0.5em;
  }

  ol {
    list-style: decimal;
    margin-left: 2.5em;
  }
}
</style>
