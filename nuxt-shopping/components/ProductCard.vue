<template>
  <div class="card text-center">
    <NuxtLink :to="`/products/${p.id}`">
      <div
        class="min-h-[9em] flex justify-center items-center transform transition duration-500 hover:scale-125"
      >
        <img :src="p.image" class="thumb" :title="p.title" />
      </div>
      <p class="font-bold text-gray-500 m-4 truncate">
        {{ p.title }}
      </p>
      <p class="mb-0"><Money :amount="p.price" /></p>
    </NuxtLink>
    <p>
      <ButtonAddToCart :item="p" qty="1" />
    </p>
  </div>
</template>

<script setup lang="ts">
const { product } = defineProps(["product"]);
let p = product;
const cart = ref(await useMyCart()).value.items;
let qty: number = 0;
try {
  qty = cart[cart.findIndex((i) => i.id === p.id)].qty || 0;
  //console.log("ID: " + p.id + " Qty: " + qty);
} catch {}
</script>

<style scoped>
.thumb {
  max-height: 120px;
  max-width: 70%;
  margin: 0 auto;
}
</style>
