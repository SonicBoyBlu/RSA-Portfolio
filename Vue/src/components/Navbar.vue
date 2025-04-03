<script setup>
import logo from "@/assets/img/logo.png";
import NavItem from "@/components/buttons/NavbarItem.vue";
import { onMounted, reactive } from "vue";
import { RouterLink, useRoute } from "vue-router";

const isActiveLink = (routePath) => {
  const route = useRoute();
  return route.path.toLowerCase() === routePath;
};

const token = localStorage.getItem("token");
let user = reactive({
  firstName: "Guest",
});

onMounted(async () => {
  if (token) {
    user = JSON.parse(localStorage.getItem("token"));
  }

  //app.config.globalProperties.$testFunction();
});
</script>
<template>
  <nav class="bg-sky-300 border-b border-sky-900">
    <div class="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
      <div class="flex h-20 items-center justify-between">
        <div
          class="flex flex-1 items-center justify-center md:items-stretch md:justify-start"
        >
          <!-- Logo -->
          <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
            <img class="h-10 w-auto" :src="logo" alt="Vue Jobs" />
            <span class="hidden md:block text-white text-2xl font-bold ml-2"
              >Vue Jobs</span
            >
          </RouterLink>
          <div class="md:ml-auto">
            <div class="flex space-x-2">
              <NavItem to="/">Home</NavItem>
              <NavItem to="/jobs">Jobs</NavItem>
              <NavItem to="/dashboard">Dashboard</NavItem>
              <div class="px-3 py-2">
                Welcome, {{ $store.state.user.firstName }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>
