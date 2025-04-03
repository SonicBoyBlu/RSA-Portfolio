<script>
// Options API
export default {
  data() {
    return {
      name: "John Doe",
      status: "active",
      tasks: [ "One", "Two", "Three"],
      links: {
        google: "https://google.com"
      }
    }
  },
  methods: {
    toggleStatus() {
      if(this.status === 'active') this.status = 'pending';
      else if(this.status === 'pending') this.status = 'inactive';
      else this.status = 'active';
    }
  }
}
</script>
<template>
  <h1>Vue Jobs for {{ name }}</h1>
  <br />
  <p v-if="status === 'active'" style="color: green;">Active</p>
  <p v-else-if="status === 'pending'" style="color: orange;">Pending</p>
  <p v-else style="color: red;">Inactive</p>
  <br />
  <h3>Tasks:</h3>
  <ul>
    <li v-for="task in tasks" :key="task">Item {{ task }}</li>
  </ul>
  <h2>Actions:</h2><br>
  <a :href="links.google" target="_blank">Open Google</a>
  <button @click="toggleStatus">Toggle Status</button>
</template>