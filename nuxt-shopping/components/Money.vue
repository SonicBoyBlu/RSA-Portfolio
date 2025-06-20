<template>
  {{ formatMoney.format(amount) }}
</template>

<script setup>
const { amount } = defineProps(["amount"]);
const formatMoney = new Intl.NumberFormat("en-us", {
  style: "currency",
  currency: "USD",
  minimumFractionDigits: 2,
});
</script>
