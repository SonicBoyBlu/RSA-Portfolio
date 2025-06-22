<template>
  <div class="card mt-7 max-w-sm mx-auto text-center">
    <p class="mt-7 text-6xl">{{ errorTitle() }}!</p>
    <p class="mt-7 text-3xl font-bold">
      Error {{ error.statusCode }} occurred.
    </p>

    <p class="mt-7">Unfortunately: {{ error.message }}</p>
    <button class="btn btn-submit my-7" @click="handleErrorClear">
      Go Back?
    </button>
  </div>
</template>

<script lang="ts" setup>
defineProps(["error"]);
const route: string[] = useRoute().fullPath.toLowerCase().split("/");
const errors = ["Whoopsie", "Oops", "Oh No", "Oh Snap", "Uh Oh..."];
const errorTitle = () => {
  return errors[Math.floor(Math.random() * errors.length)];
};

const handleErrorClear = () => {
  let uri = "/";
  if (route.some((i) => ["products", "shop"].includes(i))) uri = "/Shop";
  else if (route.some((i) => ["checkout", "myaccount"].includes(i)))
    uri = "/MyAccount";
  clearError({ redirect: uri });
};
</script>
