<script setup>
import { reactive, onMounted } from "vue";
import { useRoute, RouterLink } from "vue-router";
import Loading from "vue-spinner/src/PulseLoader.vue";
import axios from "axios";
import ButtonGoBack from "@/components/buttons/ButtonGoBack.vue";

const route = useRoute();
const jobId = route.params.id;
//console.log("JobID: " + jobId);

let context = reactive({
  job: {},
  isLoading: true,
});

onMounted(async () => {
  // simulate loading
  setTimeout(async () => {
    try {
      //const res = await axios.get(`/api/jobs/${jobId}`);
      const res = await axios.get("/json/jobs.json");
      /* Uncomment for debugging
      console.log("Jobs response:", res.data.jobs);
      console.log(
        "Job detail response:",
        res.data.jobs.find((j) => j.id == jobId)
      );
      debugger;
      */
      context.job = res.data.jobs.find((j) => j.id == jobId);
    } catch (ex) {
      console.error("Job detail fetch error: " + ex);
    } finally {
      context.isLoading = false;
    }
  }, 600);
});

const j = context.job;
console.log("Job:");
console.log(j);
</script>
<template>
  <div v-if="context.isLoading" class="text-center py-6">
    Loading... <Loading />
  </div>

  <section v-else class="bg-sky-100 min-h-lvh">
    <!-- Go Back -->
    <div>
      <ButtonGoBack />
    </div>
    <div class="container m-auto py-10 px-6">
      <div class="grid grid-cols-1 lg:grid-cols-[70%_28%] w-full gap-6">
        <main>
          <div
            class="bg-white p-6 rounded-lg shadow-md text-center md:text-left"
          >
            <h1 class="text-3xl font-bold mb-4 text-sky-700">
              {{ context.job.title }}
            </h1>
            <div class="text-gray-500 mb-4">{{ context.job.type }}</div>
            <div
              class="text-gray-500 mb-4 flex align-middle justify-center md:justify-start"
            >
              <i
                class="fa-solid fa-location-dot text-lg text-orange-700 mr-2"
              ></i>
              <p class="text-orange-700">{{ context.job.location }}</p>
            </div>
          </div>

          <div class="bg-white p-6 rounded-lg shadow-md mt-6">
            <h3 class="text-sky-700 text-lg font-bold mb-6">Job Description</h3>

            <p class="mb-4">
              {{ context.job.description }}
            </p>

            <h3 class="text-sky-700 text-lg font-bold mb-2">Salary</h3>

            <p class="mb-4">{{ context.job.salary }}</p>
          </div>
        </main>

        <!-- Sidebar -->
        <aside>
          <!-- Company Info -->
          <div class="bg-white p-6 rounded-lg shadow-md">
            <h3 class="text-xl font-bold mb-6 text-sky-700">Company Info</h3>

            <h2 class="text-2xl text-sky-400">
              {{ context.job.company.name }}
            </h2>

            <p class="my-2">
              {{ context.job.company.description }}
            </p>

            <hr class="my-4 bg-sky-700" />

            <h3
              class="text-xl"
              :title="`Email ${context.job.company.contactEmail}?`"
            >
              Contact Email:
              <a
                :href="`mailto:${context.job.company.contactEmail}`"
                class="text-sky-400 hover:text-sky-700"
                >{{ context.job.company.contactEmail }}</a
              >
            </h3>

            <h3
              class="text-xl"
              :title="`Call ${context.job.company.contactPhone}?`"
            >
              Contact Phone:
              <a
                :href="`tel:${context.job.company.contactPhone}`"
                class="text-sky-400 hover:text-sky-700"
                >{{ context.job.company.contactPhone }}</a
              >
            </h3>
          </div>

          <!-- Manage -->
          <div
            v-if="$store.getters.isAuthenticated === true"
            class="bg-white p-6 rounded-lg shadow-md mt-6"
          >
            <h3 class="text-xl font-bold mb-6 text-green-700">Manage Job</h3>
            <RouterLink
              :to="`/jobs/edit/${context.job.id}`"
              class="bg-green-700 hover:bg-green-900 text-white text-center font-bold py-2 px-4 rounded-lg w-full focus:outline-none focus:shadow-outline mt-4 block"
              >Edit Job</RouterLink
            >
          </div>
          <div class="text-gray-600 my-2 text-right">
            JobID: {{ context.job.id }}
          </div>
        </aside>
      </div>
    </div>
  </section>
</template>
