<script setup>
import { RouterLink } from "vue-router";

defineProps({
  title: {
    type: String,
    default: "Title",
  },
  tagline: {
    type: String,
    default: "Tagline",
  },
  btnLabel: {
    type: String,
    default: "Click Me",
  },
  btnAction: {
    type: String,
    default: "#",
  },
  accentColor: {
    type: String,
    default: "black",
  },
  hoverColor: {
    type: String,
    default: "gray-700",
  },

  button: {
    type: Object,
    default: {
      label: {
        type: String,
        default: "Click Here",
      },
      action: {
        type: String,
        default: "#",
      },
      background: {
        type: String,
        default: "bg-black",
      },
    },
  },
});
</script>

<template>
  <h2 :class="`text-2xl font-bold text-${accentColor}`">{{ title }}</h2>
  <p class="mt-2 mb-4">{{ tagline }}</p>
  <p class="text-right">
    <RouterLink
      :to="btnAction"
      :class="`inline-block bg-${accentColor} text-white rounded-lg px-4 py-2 hover:bg-${hoverColor}`"
    >
      {{ btnLabel }}
    </RouterLink>
    <!--  
  <a
    :href="button.action"
    :class="`inline-block ${button.background} text-white rounded-lg px-4 py-2 hover:bg-gray-700`"
  >
    {{ button.label }}
  </a>
  --></p>
</template>
