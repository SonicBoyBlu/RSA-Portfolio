<script setup>
defineProps({
  background: {
    type: String,
    default: "bg-gray-100",
  },
});
</script>

<template>
  <div :class="`${background} p-6 rounded-lg shadow-md`">
    <slot></slot>
  </div>
</template>
