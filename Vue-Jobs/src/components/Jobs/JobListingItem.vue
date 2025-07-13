<script setup>
import { defineProps, ref, computed } from "vue";
import { RouterLink } from "vue-router";

const props = defineProps({
  job: Object,
});

let showFullDetail = ref(false);
const toggleFullDescription = () => {
  showFullDetail.value = !showFullDetail.value;
};
const truncateDescription = computed(() => {
  let desc = props.job.description,
    max = 90;
  if (!showFullDetail.value) {
    desc = desc.substring(0, max) + "...";
  }
  return desc;
});
//*/
</script>
<template>
  <div class="bg-white rounded-xl shadow-md relative">
    <div class="p-4">
      <div class="mb-6">
        <h3 class="text-xl font-bold">{{ job.title }}</h3>
        <div class="text-gray-600 my-2">{{ job.type }}</div>
      </div>

      <div class="mb-5">
        {{ truncateDescription }}
        <button
          @click="toggleFullDescription"
          class="text-sky-700 hover:text-sky-400"
        >
          Read {{ showFullDetail ? "Less" : "More" }}
        </button>
      </div>

      <h3 class="text-green-700">
        <i class="fa-solid fa-dollar-sign text-lg"></i> {{ job.salary }}/yr
      </h3>

      <div class="flex flex-col lg:flex-row justify-between mb-4">
        <div class="text-orange-700 mb-3">
          <i class="fa-solid fa-location-dot text-lg"></i>
          {{ job.location }}
        </div>
        <RouterLink
          :to="/jobs/ + job.id"
          class="h-[36px] bg-sky-700 hover:bg-sky-400 text-white px-4 py-2 rounded-lg text-center text-sm"
        >
          See Details
        </RouterLink>
      </div>
    </div>
  </div>
</template>
