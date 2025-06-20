<template>
  <div>
    <SiteHeader />

    <div
      class="container p-4 mx-auto grid grid-cols-1 md:grid-cols-4 md:gap-6 gap-0"
    >
      <!-- Left Column (2/3 width on md+) -->
      <div class="md:col-span-3">
        <slot />
      </div>
      <!-- Right Column (1/3 width on md+) -->
      <div class="md:col-span-1">
        <MyCart />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;

useHead({
  title: siteTitle + " | Merch",
  meta: [{ name: "description", content: "Nuxt 3 shopping app merch page." }],
});
</script>

<style>
body {
  min-height: 100vh;
  background: #b6b1ad;
  background: linear-gradient(170deg, #b6b1ad 50%, #c026d3 75%, #1f2937 93%);
}
</style>
