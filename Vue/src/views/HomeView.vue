<script setup>
import Hero from "@/components/Hero.vue";
import HomeCards from "@/components/HomeCards.vue";
import JobListView from "@/components/Jobs/JobList.vue";
</script>
<template>
  <Hero
    title="Become a Vue Dev"
    subtitle="Find the Vue job that fits your skills and needs"
  />
  <HomeCards />
  <JobListView :size="3" :showButton="true" :current="0" />
</template>
