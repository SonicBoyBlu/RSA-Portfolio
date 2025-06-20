<template>
  <div>
    <h2>
      <span class="material-symbols-outlined"> stars_2 </span>
      Book the Look
    </h2>
    <SectionCard>
      <h2>
        <span class="material-symbols-outlined"> home_pin </span
        ><span> Send to:</span>
        <!--Your Runway:-->
        <select v-model="shippingId" @change="changeShippingAddress">
          <option
            v-for="x in addresses"
            :value="x.addressId"
            :key="x.addressId"
          >
            {{ x.label }}
          </option>
        </select>
      </h2>

      <blockquote>
        <address>
          <strong>{{ shipTo.recipient }}</strong
          ><br />
          {{ shipTo.street + (shipTo.unitNum ? ", " + shipTo.unitNum : "")
          }}<br />
          {{ shipTo.city }}, {{ shipTo.state }} {{ shipTo.zip }}
        </address>
      </blockquote>
    </SectionCard>

    <SectionCard>
      <h2>
        <span class="material-symbols-outlined"> wallet </span>
        <span> Pay with:</span>
        <!--Your Benjamins:-->
        <select v-model="paymentCardId" @change="changePaymentMethod">
          <option v-for="x in paymentCards" :value="x.cardId" :key="x.cardId">
            {{ x.label }}
          </option>
        </select>
      </h2>
      <blockquote>
        <p>
          <strong>{{ payWith.nameOnCard }}</strong
          ><br />
          {{ payWith.cardType }} ending in: ... {{ payWith.accountDisplay
          }}<br />
          Expires: {{ payWith.expMonth }}/{{ payWith.expYear }}
        </p>
      </blockquote>
    </SectionCard>

    <SectionCard class="hidden md:block lg:block">
      <MyCart></MyCart>
    </SectionCard>
  </div>
</template>
<script setup lang="ts">
definePageMeta({
  layout: "shop",
});

//const me = await useMyAccount();

// Shipping Address
const addresses = await useMyAddresses();
const shippingId = ref(0);
const shipTo = useState("cart-address", () => addresses[0]);
const changeShippingAddress = async () => {
  shipTo.value =
    addresses.find((a) => a.addressId === shippingId.value) || _defaultAddress;
  cart.value.address = shipTo.value;
  saveCart(cart.value);
};

// Payment Details
const paymentCards = await useMyPaymentCards();
const paymentCardId = ref(0);
let payWith = useState("cart-payment", () => paymentCards[0]);
const changePaymentMethod = async () => {
  payWith.value =
    paymentCards.find((c) => c.cardId === paymentCardId.value) ||
    _defaultPaymentCard;
  cart.value.paymentCard = payWith.value;
  saveCart(cart.value);
};

const cart = useState<ICart>("my-cart");

onMounted(async () => {
  shippingId.value =
    addresses.find((x) => x.addressId === cart.value.address?.addressId)
      ?.addressId ||
    addresses.find((x) => x.isDefault === true)?.addressId ||
    addresses[0].addressId;
  paymentCardId.value =
    paymentCards.find((x) => x.cardId === cart.value.paymentCard?.cardId)
      ?.cardId ||
    paymentCards.find((x) => x.isDefault === true)?.cardId ||
    paymentCards[0].cardId;

  // Cart - setup defaults in case of refresh
  cart.value.address = shipTo.value;
  cart.value.paymentCard = payWith.value;
  saveCart(cart.value);
});
</script>
