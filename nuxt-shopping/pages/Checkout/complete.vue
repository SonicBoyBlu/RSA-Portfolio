<template>
  <div>
    <h2 class="text-fuchsia-600">
      <span class="material-symbols-outlined"> receipt_long </span> Order
      Complete
    </h2>

    <SectionCard :title="`Success! Order ID: ${orderIdCurrent}`"> </SectionCard>

    <SectionCard title="The Drip">
      <ul class="border-l-4 border-gray-500 pl-4 mr-4 italic text-gray-700">
        <li v-if="order.items.length === 0">
          <h2>No items in cart</h2>
        </li>
        <li v-for="i in order.items">
          <CartItem :product="i" />
        </li>
      </ul>
    </SectionCard>

    <SectionCard title="The Destination">
      <blockquote
        class="border-l-4 border-gray-500 pl-4 mr-4 italic text-gray-700"
      >
        <strong
          >{{ order.address?.label }}: {{ order.address?.recipient }}</strong
        >
        <address>
          {{
            cart.address?.street +
            (cart.address?.unitNum ? ", " + cart.address?.unitNum : "")
          }}<br />
          {{ cart.address?.city }}, {{ cart.address?.state }}
          {{ cart.address?.zip }}
        </address>
      </blockquote>
    </SectionCard>

    <SectionCard title="The Creds">
      <blockquote>
        <p>
          <strong
            >{{ cart.paymentCard?.label }}:
            {{ cart.paymentCard?.nameOnCard }}</strong
          ><br />
          {{ cart.paymentCard?.cardType }} ending in: ...
          {{ cart.paymentCard?.accountDisplay }}<br />
          Expires: {{ cart.paymentCard?.expMonth }}/{{
            cart.paymentCard?.expYear
          }}
        </p>
      </blockquote>
    </SectionCard>

    <SectionCard>
      <NuxtLink to="/Shop" class="btn btn-next flex text-center">
        <i class="material-symbols-outlined mr-auto">shopping_bag_speed</i>
        Keep Shopping</NuxtLink
      >
    </SectionCard>
  </div>
</template>
<script setup lang="ts">
definePageMeta({
  layout: "shop",
});

const cart = await useMyCart();

let orders: ICart[] = [];

const order = cart.value;

const orderIdLast = 25000562;
const orderIdCurrent = orderIdLast + getRandomNumber();

function getRandomNumber(min = 10, max = 500) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

/*
let order: ICart = JSON.parse(localStorage.getItem("cart-current"));

try {
  orders = JSON.parse(localStorage.getItem("order-history"));
  if (!orders) orders = [];
  orders.push(order);
  localStorage.setItem("order-history", JSON.stringify(orders));
} catch {}

*/
//cart.value.items = [];
//updateCart();
</script>
