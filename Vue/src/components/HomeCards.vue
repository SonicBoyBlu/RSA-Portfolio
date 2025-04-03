<script setup>
import Card from "@/components/Card.vue";
import CTA from "@/components/CTA.vue";
</script>
<template>
  <section class="py-4">
    <div class="container-xl lg:container m-auto">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4 p-4 rounded-lg">
        <!-- Candidates -->
        <Card background="bg-sky-100">
          <CTA
            title="For Developers"
            tagline="Browse our Vue jobs and start your career today"
            btnLabel="Browse Jobs"
            btnAction="/jobs"
            accentColor="sky-700"
            hoverColor="sky-400"
          />
        </Card>

        <!-- Employers -->
        <Card background="bg-green-100">
          <CTA
            title="For Employers"
            tagline="List your job to find the perfect developer for the role"
            btnAction="/jobs/add"
            accentColor="green-700"
            hoverColor="green-900"
            btnLabel="Manage Jobs"
          />
        </Card>
      </div>
    </div>
  </section>
</template>
