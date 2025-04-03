<script setup>
import { ref, defineProps, onMounted, reactive } from "vue";
import JobListingItem from "@/components/Jobs/JobListingItem.vue";
import { RouterLink } from "vue-router";
import Loading from "vue-spinner/src/PulseLoader.vue";
import axios from "axios";

defineProps({
  current: {
    type: Number,
    default: 0,
  },
  size: {
    type: Number,
    default: 1000,
  },
  showButton: {
    type: Boolean,
    default: false,
  },
});

//const jobs = ref([]);
let context = reactive({
  jobs: [],
  isLoading: true,
});

onMounted(async () => {
  // simulate loading
  setTimeout(async () => {
    try {
      const res = await axios.get("/api/jobs");
      context.jobs = res.data;
    } catch (ex) {
      console.error("Jobs fetch error: " + ex);
    } finally {
      context.isLoading = false;
    }
  }, 900);
});
</script>

<template>
  <section v-if="context.isLoading" class="text-center py-6">
    Loading... <Loading />
  </section>
  <section v-else class="bg-blue-50 px-4 py-10">
    <div class="container-xl lg:container m-auto">
      <h2 class="text-3xl font-bold text-sky-700 mb-6 text-center">
        Browse Jobs
      </h2>

      <div
        class="grid grid-cols-1 gap-6 xl:grid-cols-4 lg:grid-cols-3 md:grid-cols-2"
      >
        <JobListingItem
          v-for="job in context.jobs.splice(
            current,
            size || context.jobs.length
          )"
          :key="job.id"
          :job="job"
        />

        <div v-if="showButton">
          <RouterLink to="/jobs" title="View All Jobs">
            <div class="bg-white rounded-xl shadow-md relative">
              <div class="p-4">
                <div class="mb-6 text-6xl py-15">
                  <i class="fa-solid fa-angles-right float-right text-9xl"></i>
                  More Jobs
                </div>
              </div>
            </div>
          </RouterLink>
        </div>
      </div>
    </div>
    <!--
    <div v-if="showButton" class="m-auto max-w-lg my-10 px-6">
      <RouterLink
        to="/jobs"
        class="block bg-sky-700 text-white text-center py-4 px-6 rounded-xl hover:bg-sky-400"
        >View All Jobs</RouterLink
      >
    </div>
    -->
  </section>
</template>
