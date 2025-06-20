<template>
  <div class="flex gap-4 mb-7">
    <div class="flex-none">
      <NuxtLink :to="`/products/${product.id}`">
        <img :src="product.image" class="w-9 h-full" />
      </NuxtLink>
    </div>
    <div class="flex-1 w-10 truncate">
      <NuxtLink :to="`/products/${product.id}`">
        <strong>{{ product.title }}</strong>
        <div class="grid grid-cols-2">
          Price: <Money :amount="product.price" /> X {{ product.qty }}
          <!--id: {{ product.id }}-->
        </div>
      </NuxtLink>
    </div>

    <div v-if="editQty == true" class="flex-none">
      <button title="Remove item" @click="removeItemFromCart(product.id)">
        X
      </button>
    </div>
  </div>
</template>
<script setup>
const { product, editQty } = defineProps(["product", "editQty"]);
</script>
