<template>
  <h2 class="hidden md:block">
    <span class="material-symbols-outlined mr-auto"> shopping_bag </span>
    My Bag
  </h2>
  <div class="card h-auto mt-4">
    <h2 class="sm:block md:hidden lg:hidden">
      <span class="material-symbols-outlined mr-auto"> shopping_bag </span>
      My Bag
    </h2>
    <ul>
      <li v-if="cart.items.length === 0">
        <h2>No items in cart</h2>
      </li>
      <li v-for="i in cart.items">
        <CartItem :product="i" :edit-qty="true" />
      </li>
    </ul>
    <hr class="h-px my-2 bg-gray-200 border-0 dark:bg-gray-700" />
    <p class="font-bold">Subtotal: <Money :amount="cart.subtotal" /></p>
    <p class="font-bold">
      Tax ({{ cart.taxRate * 100 }}%): <Money :amount="cart.taxTotal" />
    </p>
    <p class="font-bold">Shipping: <Money :amount="cart.shipping" /></p>
    <hr class="h-px my-2 bg-gray-200 border-0 dark:bg-gray-700" />
    <h2 class="font-bold">Total: <Money :amount="cart.total" /></h2>

    <div v-if="$route.name?.toString().toLowerCase() == 'checkout'">
      <NuxtLink to="/Checkout/Processing" class="btn btn-cta flex text-center">
        <i class="material-symbols-outlined mr-auto">shopping_bag_speed</i>
        Book It!</NuxtLink
      >
    </div>
    <div v-else-if="cart.items.length > 0">
      <NuxtLink to="/Checkout" class="btn btn-submit flex text-center">
        <i class="material-symbols-outlined mr-auto">shopping_cart_checkout</i>
        Checkout</NuxtLink
      >
    </div>
  </div>
</template>

<script setup lang="ts">
const _cart = await useMyCart();
const cart = useState<ICart>("my-cart", () => _cart);
</script>
