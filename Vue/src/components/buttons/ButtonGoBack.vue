<script setup>
import { RouterLink } from "vue-router";

const handleGoBack = () => {
  window.history.back();
};
</script>
<template>
  <div class="container m-auto py-6 px-6">
    <!--
    <RouterLink
      to="/jobs"
      class="text-sky-700 hover:text-sky-400 flex items-center"
    >
      <i class="fas fa-arrow-left mr-2"></i> Back to Job Listings
    </RouterLink>
  -->
    <button
      @click="handleGoBack"
      class="text-sky-700 hover:text-sky-400 flex items-center"
    >
      <i class="fas fa-arrow-left mr-2"></i> Back to Job Listings
    </button>
  </div>
</template>
