<template>
  <div>
    <h2 class="text-fuchsia-600">
      <span class="material-symbols-outlined"> manage_accounts </span>
      Account Settings
    </h2>

    <SectionCard title="👤 My Deets:">
      <ul>
        <li>
          <blockquote>
            Name:
            <strong>{{ me.firstName }} {{ me.lastName }}</strong>
          </blockquote>
        </li>
        <li>
          <blockquote>
            Phone: <strong>{{ me.phone }}</strong>
          </blockquote>
        </li>
        <li>
          <blockquote>
            Email: <strong>{{ me.username }}</strong>
          </blockquote>
        </li>
        <li>
          <blockquote>
            UserID: <strong>{{ me.userId }}</strong>
          </blockquote>
        </li>
      </ul>
    </SectionCard>

    <SectionCard title="🛒 My Orders:">
      <ul>
        <li v-if="orders.length === 0">
          <blockquote>
            <h2>No past orders.</h2>
          </blockquote>
        </li>
        <li v-for="o in orders">
          <blockquote>
            <OrderSummary :order="o" />
          </blockquote>
        </li>
      </ul>
    </SectionCard>

    <SectionCard title="🏠 Addresses:">
      <ul>
        <li v-if="addresses.length === 0">
          <blockquote><h2>No saved addresses.</h2></blockquote>
        </li>
        <li v-for="a in addresses" class="mb-4">
          <blockquote>
            <address>
              <span v-if="a.isDefault == true">* </span>
              <strong>{{ a.label }}</strong
              >: <strong>{{ a.recipient }}</strong
              ><br />
              {{ a.street + (a.unitNum ? ", " + a.unitNum : "") }}<br />
              {{ a.city }}, {{ a.state }} {{ a.zip }}
            </address>
          </blockquote>
        </li>
      </ul>
    </SectionCard>

    <SectionCard title="🤑 My Benjamins:">
      <ul>
        <li v-if="paymentCards.length === 0">
          <blockquote><h2>No saved credit cards.</h2></blockquote>
        </li>
        <li v-for="c in paymentCards" class="mb-4">
          <blockquote>
            <span v-if="c.isDefault == true">* </span>
            <strong>{{ c.label }}</strong> {{ c.nameOnCard }}<br />
            {{ c.cardType }} ending in: ... {{ c.accountDisplay }}<br />
            Expires: {{ c.expMonth }}/{{ c.expYear }}
          </blockquote>
        </li>
      </ul>
    </SectionCard>
  </div>
</template>

<script setup lang="ts">
import type { IOrder } from "~/types/IOrder";

const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;
useHead({
  title: siteTitle + " | Account Settings",
});

definePageMeta({
  layout: "shop",
});

const _me = await useMyAccount();
const me = _me.user;

let orders = _me.orders; // ref(await useMyOrders());
const paymentCards = _me.paymentCards; // await useMyPaymentCards();
const addresses = _me.addresses; // await useMyAddresses();

//*
onMounted(async () => {
  //me.value = await useMyAccount();
  if (me.userId < 1) {
    navigateTo("/login?return=MyAccount");
    console.warn("Redirect to Login");
  }
});
//*/
</script>
