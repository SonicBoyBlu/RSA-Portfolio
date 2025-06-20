<template>
  <div class="card mb-5">
    <h2 v-if="title">{{ title }}</h2>
    <slot />
  </div>
</template>
<script setup lang="ts">
const props = defineProps(["title"]);
</script>
