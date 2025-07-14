<template>
  <div>
    <h2 class="text-fuchsia-600">
      <span class="material-symbols-outlined"> currency_exchange </span> Placing
      Order...
    </h2>

    <div class="card mb-5">
      <div class="flex items-center justify-center text-fuchsia-600">
        <div class="flex flex-col items-center space-y-4">
          <div
            class="animate-spin rounded-full h-16 w-16 border-t-4 border-pink-500 border-solid"
          ></div>
          <p class="text-2xl">Prepping your experience...</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
definePageMeta({
  layout: "shop",
});

const cart = useState("my-cart"); // useMyCart();
const orders = await useMyOrders();
let history: ICart[] = orders;
history.push(cart);

const generateRandomTimeout = (min = 900, max = 3000) => {
  return Math.floor(Math.random() * (max - min + 1)) + min;
};

onMounted(() => {
  setTimeout(() => {
    //navigateTo("./Complete");
  }, generateRandomTimeout());
});
</script>
