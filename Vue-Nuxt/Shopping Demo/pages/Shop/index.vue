<template>
  <div>
    <h2 class="text-fuchsia-600">
      <span class="material-symbols-outlined"> apparel </span>
      Products
    </h2>

    <div class="grid grid-cols-1 md:grid-cols-3 gap-3">
      <div class="relative col-span-2">
        <input
          type="search"
          v-model="query"
          placeholder="Search..."
          class="w-full pl-10 pr-4 py-2 rounded-lg bg-gray-800 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-fuchsia-600"
          @input="handleKeywordSearch"
        />
        <span
          class="material-symbols-outlined absolute inset-y-0 left-0 pl-3 sm:mt-[-15px] flex items-center text-gray-400 pointer-events-none"
        >
          search
        </span>
      </div>
      <div class="relative col-span-1 mb-5">
        <select
          v-model="category"
          @change="handleCategoryChange"
          class="w-full pl-10 pr-4 py-2 rounded-lg bg-gray-800 text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-fuchsia-600"
        >
          <option>All</option>
          <option v-for="c in categories" class="pr-5" :key="c" :value="c">
            {{ capitalize(c) }}
          </option>
        </select>
        <span
          class="material-symbols-outlined absolute inset-y-0 left-0 pl-3 flex items-center text-gray-400 pointer-events-none"
        >
          tune
        </span>
      </div>
    </div>

    <div class="flex-x items-center justify-center text-fuchsia-600 mb-5">
      <div v-if="isLoading" class="flex flex-col items-center space-y-4 card">
        <Loading />
        <p class="text-2xl">Loading your experience...</p>
      </div>
    </div>
    <div
      class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4"
    >
      <div v-if="products.length === 0 && !isLoading" class="card col-span-4">
        <h2>No items found...</h2>
      </div>
      <div v-if="!isLoading" v-for="p in products">
        <ProductCard :product="p" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { capitalize } from "vue";
definePageMeta({
  layout: "shop",
});
const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;
useHead({
  title: siteTitle + " | Merch",
  meta: [{ name: "description", content: "Nuxt 3 shopping app merch page." }],
});

let products = useState("products", () => []);
let category = useState("category", () => "All");
let query = useState("query", () => "");
let isLoading = useState("loading-products", () => true);

const categories = await useGetProductCategories();

const handleSearch = async () => {
  isLoading.value = true;
  products.value = [];
  products.value = await $fetch("/api/shop/fetchItems", {
    method: "post",
    body: {
      category: category.value,
      query: query.value,
    },
  });
  isLoading.value = false;
};

const handleCategoryChange = () => {
  localStorage.setItem("shop-category", category.value);
  handleSearch();
};

const _keywordSearch = () => {
  const _query: string = query.value || "";
  console.log("Query product: " + _query);
  handleSearch();
  /*
    products.value = products.value.filter((i: IProduct) =>
      i.title.toLowerCase().includes(_query.toLowerCase())
    );
    */
  console.log(products.value);
};

let keywordSearch = setTimeout(_keywordSearch, 300);

const handleKeywordSearch = () => {
  clearTimeout(keywordSearch);
  localStorage.setItem("shop-keyword", query.value);
  keywordSearch = setTimeout(_keywordSearch, 300);
};

//*/

onMounted(() => {
  category.value = localStorage.getItem("shop-category") || "All";
  query.value = localStorage.getItem("shop-keyword") || "";
  handleSearch();
});
</script>
