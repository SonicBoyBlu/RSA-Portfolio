<template>
  <NuxtLink :to="`/Orders/${o.orderId}`" title="View Order">
    <strong>OrderID: {{ o.orderId }}</strong>
    <span class="float-right" v-if="id != o.orderId">
      <a class="btn btn-submit" href="#">View Details</a>
    </span>
    <br /><br />
    <div class="grid grid-cols-3 gap-3">
      <div>
        <strong>Shipped to: {{ o.address.recipient }}</strong
        ><br />
        {{ o.address.street }}<br />
        {{ o.address.city }}, {{ o.address.state }} {{ o.address.zip }}
      </div>
      <div>
        <strong>Total: <Money :amount="o.total" /></strong><br />
        <strong>Card: </strong>{{ o.paymentCard.cardType }} ending in
        {{ o.paymentCard.accountDisplay }}<br />
        <strong>Exp:</strong> {{ o.paymentCard.expMonth }}/{{
          o.paymentCard.expYear
        }}
      </div>
      <div>
        <strong>Items:</strong
        ><span class="float-right">{{ o.items.length }}</span
        ><br />
        <strong>Subtotal:</strong>
        <span class="float-right"><Money :amount="o.subtotal" /></span><br />
        <strong>Tax:</strong>
        <span class="float-right"><Money :amount="o.taxTotal" /></span><br />
        <strong>Shipping:</strong>
        <span class="float-right"><Money :amount="o.shipping" /></span>
      </div>
    </div>
  </NuxtLink>
</template>
<script setup lang="ts">
const props = defineProps(["order"]);
const { id } = useRoute().params;
const o = props.order;
</script>
