<script setup>
import { useRouter } from "vue-router";
import { useStore } from "vuex";
const router = useRouter();
const store = useStore();

const handleLogout = async () => {
  localStorage.removeItem("token");
  store.commit("setUser", { firstName: " Guest" });
  store.commit("setAuth", false);
  router.push("/");
};
</script>
<template>
  <button
    type="button"
    @click="handleLogout"
    class="inline-block bg-red-700 text-white rounded-lg px-4 py-2 hover:bg-red-400"
  >
    Logout
  </button>
</template>
