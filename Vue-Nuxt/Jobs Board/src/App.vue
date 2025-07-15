<script setup>
import Navbar from "@/components/Navbar.vue";
import { RouterView } from "vue-router";
import { useStore } from "vuex";
const store = useStore();

store.commit("loadState");
</script>
<template class="bg-sky-100">
  <Navbar />
  <RouterView />
</template>
