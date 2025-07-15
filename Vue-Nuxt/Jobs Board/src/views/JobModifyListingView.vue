<script setup>
import Router from "@/router";
import { reactive, onMounted } from "vue";
import { useRoute } from "vue-router";
import Loading from "vue-spinner/src/PulseLoader.vue";
import ButtonGoBack from "@/components/buttons/ButtonGoBack.vue";
import axios from "axios";
import { useToast } from "vue-toastification";

const toast = useToast();
const route = useRoute();
const jobId = route.params.id;

let form = reactive({
  id: null,
  type: "Full-Time",
  title: "",
  description: "",
  salary: 2,
  location: "",
  company: {
    name: "",
    description: "",
    contactEmail: "",
    contactPhone: "",
  },
});

let context = reactive({
  types: [],
  salaries: [],
  job: {},
  action: "Add",
  isLoading: true,
});
//console.log(context);

const handleSubmit = async () => {
  try {
    if (jobId > 0) {
      const res = await axios.put(`/api/jobs/${jobId}`, form);
      toast.success("Updated Job:" + res.data.title);
      Router.push(`/jobs/${res.data.id}`);
    } else {
      const res = await axios.post("/api/jobs", form);
      toast.success("Created Job:" + res.data.title);
      Router.push(`/jobs/${res.data.id}`);
    }
  } catch (ex) {
    console.error("ERROR: " + ex);
    toast.error("Oops, something went wrong!");
  }
};

const handleDelete = async () => {
  try {
    const confirm = window.confirm("Are you sure you want to delete this job?");
    if (confirm) {
      const res = await axios.delete(`/api/jobs/${jobId}`);
      toast.success("Success! Job was removed.");
      Router.push("/jobs");
    }
  } catch (ex) {
    console.error(ex);
    toast.error("Oops, an error has occurred!");
  }
};

onMounted(async () => {
  // simulate loading
  setTimeout(async () => {
    try {
      /*
      const types = await axios.get("/api/types");
      const salaries = await axios.get("/api/salaries");
    */
      const res = await axios.get("/json/jobs.json");

      context.types = res.data.types;
      context.salaries = res.data.salaries;

      if (jobId) {
        context.action = "Update";
        const job = await axios.get(`/api/jobs/${jobId}`);
        context.job = job.data;
        form = job.data;
      }
    } catch (ex) {
      console.error("ERROR: " + ex);
      toast.error("Oops, something went wrong!");
    } finally {
      context.isLoading = false;
    }
  }, 900);
});
</script>
<template class="bg-sky-100">
  <section v-if="context.isLoading">
    <div class="text-center py-6">Loading... <Loading /></div>
  </section>

  <section v-else class="bg-sky-50">
    <div v-if="jobId > 0">
      <ButtonGoBack />
    </div>

    <form @submit.prevent="handleSubmit">
      <div class="container m-auto py-10 px-6">
        <div class="grid grid-cols-1 lg:grid-cols-[70%_28%] w-full gap-6">
          <main>
            <!-- Job Basics -->
            <div
              class="bg-white p-6 py-3 rounded-lg shadow-md text-center md:text-left mb-6 text-sky-700"
            >
              <h2 class="text-3xl font-semibold mb-6 text-sky-700">
                {{ context.action }}
                Job
              </h2>

              <div class="mb-4 text-sky-700">
                <label class="block font-bold mb-2">Job Listing Name</label>
                <input
                  v-model="form.title"
                  type="text"
                  id="name"
                  name="name"
                  class="border rounded w-full py-2 px-3 mb-2"
                  placeholder="eg. Beautiful Apartment In Miami"
                  required
                />

                <label for="description" class="block font-bold mb-2"
                  >Description</label
                >
                <textarea
                  v-model="form.description"
                  id="description"
                  name="description"
                  class="border rounded w-full py-2 px-3"
                  rows="4"
                  placeholder="Add any job duties, expectations, requirements, etc"
                ></textarea>
              </div>
            </div>

            <!-- Job Details -->
            <div
              class="bg-white p-6 rounded-lg shadow-md text-center md:text-left text-sky-700 py-3"
            >
              <div class="mb-4">
                <label for="type" class="block font-bold mb-2">Job Type</label>
                <select
                  v-model="form.type"
                  id="type"
                  name="type"
                  class="border rounded w-full py-2 px-3"
                  required
                >
                  <option
                    v-for="t in context.types"
                    :key="t.id"
                    :value="t.label"
                  >
                    {{ t.label }}
                  </option>
                </select>
              </div>

              <div class="mb-4">
                <label for="type" class="block font-bold mb-2">Salary</label>
                <select
                  v-model="form.salary"
                  id="salary"
                  name="salary"
                  class="border rounded w-full py-2 px-3"
                  required
                >
                  <option
                    v-for="s in context.salaries"
                    :value="s.label"
                    :key="s.id"
                  >
                    {{ s.label }}
                  </option>
                </select>
              </div>

              <div class="mb-4">
                <label class="block font-bold mb-2"> Location </label>
                <input
                  v-model="form.location"
                  type="text"
                  id="location"
                  name="location"
                  class="border rounded w-full py-2 px-3 mb-2"
                  placeholder="Company Location"
                  required
                />
              </div>
            </div>
          </main>

          <aside>
            <div
              class="bg-white p-6 rounded-lg shadow-md text-center md:text-left text-sky-700 py-3 mb-6"
            >
              <h3 class="text-2xl mb-5">Company Info</h3>

              <div class="mb-4">
                <label for="company" class="block font-bold mb-2"
                  >Company Name</label
                >
                <input
                  v-model="form.company.name"
                  type="text"
                  id="company"
                  name="company"
                  class="border rounded w-full py-2 px-3"
                  placeholder="Company Name"
                />
              </div>

              <div class="mb-4">
                <label for="company_description" class="block font-bold mb-2"
                  >Company Description</label
                >
                <textarea
                  v-model="form.company.description"
                  id="company_description"
                  name="company_description"
                  class="border rounded w-full py-2 px-3"
                  rows="4"
                  placeholder="What does your company do?"
                ></textarea>
              </div>

              <div class="mb-4">
                <label for="contact_email" class="block font-bold mb-2"
                  >Contact Email</label
                >
                <input
                  v-model="form.company.contactEmail"
                  type="email"
                  id="contact_email"
                  name="contact_email"
                  class="border rounded w-full py-2 px-3"
                  placeholder="Email address for applicants"
                  required
                />
              </div>
              <div class="mb-4">
                <label for="contact_phone" class="block font-bold mb-2"
                  >Contact Phone</label
                >
                <input
                  v-model="form.company.contactPhone"
                  type="tel"
                  id="contact_phone"
                  name="contact_phone"
                  class="border rounded w-full py-2 px-3"
                  placeholder="Optional phone for applicants"
                />
              </div>
            </div>

            <!-- Manage -->
            <div
              class="bg-white p-6 rounded-lg shadow-md text-center md:text-left text-sky-700 py-3"
            >
              <button
                class="bg-green-700 hover:bg-green-400 text-white font-bold py-2 px-4 rounded-lg w-full focus:outline-none focus:shadow-outline"
                type="submit"
              >
                {{ context.action }} Job
              </button>
              <div v-if="jobId > 0">
                <button
                  @click.prevent="handleDelete"
                  class="bg-red-600 hover:bg-red-800 text-white font-bold py-2 px-4 rounded-lg w-full focus:outline-none focus:shadow-outline mt-4 block"
                >
                  Delete Job
                </button>
              </div>
            </div>
          </aside>
        </div>
      </div>
    </form>
  </section>
</template>
