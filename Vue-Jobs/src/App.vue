<script setup>
import Navbar from "@/components/Navbar.vue";
import { RouterView } from "vue-router";
import { useStore } from "vuex";
const store = useStore();

store.commit("loadState");
</script>
<template>
  <Navbar />
  <RouterView />
</template>
