<template>
  <div v-if="!isLoading">
    <button class="btn btn-cta flex" @click="addItemToCart">
      <span class="material-symbols-outlined mr-auto"> shopping_bag </span>
      <span>Add to Cart</span>
    </button>
  </div>
  <div v-else class="flex flex-col items-center">
    <Loading :size="5" /><small class="text-fuchsia-600">adding...</small>
  </div>
</template>

<script setup>
const { item, qty } = defineProps(["item", "qty"]);
const isLoading = ref(false);
if (qty) item.qty = qty;

const addItemToCart = () => {
  isLoading.value = true;
  // simulate API call
  setTimeout(() => {
    addToCart(item).value;
    isLoading.value = false;
  }, 250);
};
</script>
