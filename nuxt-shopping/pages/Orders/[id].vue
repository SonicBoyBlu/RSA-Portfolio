<template>
  <h2 class="text-fuchsia-600">
    <span class="material-symbols-outlined"> search_activity </span>
    Order Summary: #{{ o.orderId }}
  </h2>
  <SectionCard title="The Deets"> <OrderSummary :order="o" /></SectionCard>
  <SectionCard title="The Merch">
    <blockquote>
      <ul>
        <li v-for="i in o.items">
          <CartItem :product="i" />
        </li>
      </ul>
    </blockquote>
  </SectionCard>

  <SectionCard>
    <NuxtLink to="/MyAccount" class="btn btn-next flex text-center"
      ><span class="material-symbols-outlined mr-auto"> history </span>Go To
      Oder History</NuxtLink
    >
  </SectionCard>
</template>
<script setup lang="ts">
const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;
useHead({
  title: siteTitle + " | Order Details",
});

definePageMeta({
  layout: "shop",
});

const { id } = useRoute().params;
const orders: ICart[] = await useMyOrders();
const o = orders.find((x) => x.orderId.toString() == id);
</script>

<style scoped>
.card {
  height: auto;
}
</style>
