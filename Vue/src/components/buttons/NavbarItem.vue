<script setup>
import { RouterLink, useRoute } from "vue-router";

defineProps({
  to: {
    type: String,
    default: "",
  },
  section: {
    type: String,
    default: "",
  },
});

const isActiveLink = (routePath) => {
  const route = useRoute();

  return (
    route.path.toLowerCase().split("/")[1] ===
    routePath.toLowerCase().split("/")[1]
  );
  // return route.path.toLowerCase().indexOf(routePath) > -1;
  //return route.path.toLowerCase() === routePath;
};
</script>
<template>
  <RouterLink
    :to="`${to}`"
    :class="`${
      isActiveLink(to) ? 'bg-sky-700' : 'hover:underline'
    } text-white hover:text-white rounded-md px-3 py-2`"
    ><slot></slot
  ></RouterLink>
</template>
