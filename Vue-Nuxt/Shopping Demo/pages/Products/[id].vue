<template>
  <div>
    <h2 class="text-fuchsia-600">
      <span class="material-symbols-outlined"> apparel </span>
      Product Details
    </h2>
    <ProductDetail :product="product" />
  </div>
</template>

<script setup>
const { id } = useRoute().params;
const uri = "https://fakestoreapi.com/products/" + id;

const { data: product } = await useFetch(uri);

if (!product.value) {
  ///*
  product.value = {
    id: 0,
    title: "Unknown",
    description: "Product not found.",
    category: "Unknown",
    image:
      "https://cdn.pixabay.com/photo/2024/07/20/17/12/warning-8908707_1280.png",
    price: 0,
    qty: 0,
  };
  //*/

  /*
  const errorMessage = "Product not found - '" + id + "'";
  throw createError({
    statusCode: 404,
    statusMessage: errorMessage,
    fatal: true,
  });
//*/
}

definePageMeta({
  layout: "shop",
});

const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;

useHead({
  title: siteTitle + " | " + product.value.title,
  meta: [
    {
      name: "description",
      content:
        "Buy " +
        product.value.title +
        "now from " +
        siteTitle +
        ". Details: " +
        product.value.description,
    },
  ],
});
</script>
