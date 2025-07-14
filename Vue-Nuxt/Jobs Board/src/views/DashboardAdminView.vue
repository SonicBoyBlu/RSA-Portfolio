<script setup>
import ButtonLogout from "@/components/buttons/ButtonLogout.vue";
</script>
<template>
  <section class="container m-auto py-10 px-6">
    <div class="grid grid-cols-1 md:grid-cols-70/30 w-full gap-6">
      <main>
        <h2 class="text-3xl font-semibold mb-6">Employer Dashboard</h2>
        <div>
          <RouterLink
            to="/jobs/add"
            :class="`inline-block bg-sky-700 text-white rounded-lg px-4 py-2 hover:bg-sky-400 mr-3`"
          >
            Add Job
          </RouterLink>

          <ButtonLogout />
        </div>
      </main>
    </div>
  </section>
</template>
