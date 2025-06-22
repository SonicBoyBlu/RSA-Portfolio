<template>
  <div class="card">
    <h2 class="text-2xl my-0">{{ product.title }}</h2>
    <div class="grid sm:grid-cols-2 gap-10">
      <div class="p-7 transform transition duration-500 hover:scale-125">
        <img
          :src="product.image"
          :title="product.title"
          class="mx-auto my-7 max-h-[350px]"
        />
      </div>
      <div class="p-7">
        <strong>Category:</strong> {{ capitalize(product.category)
        }}<br /><br />
        <strong>Details:</strong> {{ product.description }}
      </div>
    </div>

    <div v-if="product.id > 0" class="grid sm:grid-cols-2 gap-10">
      <div class="px-7">
        <div>Avg Rating: {{ product.rating.rate }}</div>
        <div class="ratings">
          <div class="empty-stars"></div>
          <div
            class="full-stars"
            :style="`width: ${(product.rating.rate / 5) * 100}%`"
          ></div>
        </div>

        <div>Reviews: {{ product.rating.count }}</div>
      </div>

      <div class="px-7">
        <p class="text-xl mt-0">Price: <Money :amount="product.price" /></p>
        <p>
          Qty:
          <select
            name="quantity"
            id="quantity"
            v-model="qty"
            @change="updateCartItem"
          >
            <option v-for="n in 10" :key="n" :value="n">{{ n }}</option>
          </select>
        </p>
        <ButtonAddToCart :item="product" :qty="qty" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { capitalize } from "vue";
const { product } = defineProps(["product"]);

let qty = ref<number>(1);

onMounted(async () => {
  const _cart = await useMyCart();
  const x = _cart.items.findIndex((i) => i.id == product.id);
  if (x !== -1) {
    qty.value = _cart.items[x].qty;
  }
});

const updateCartItem = () => {
  product.qty = qty.value;
  //updateItemQty(product);
};
</script>
<style>
.ratings {
  position: relative;
  vertical-align: middle;
  display: inline-block;
  color: #b1b1b1;
  overflow: hidden;
}
.full-stars {
  position: absolute;
  left: 0;
  top: 0;
  white-space: nowrap;
  overflow: hidden;
  color: #fde16d;
}
.empty-stars:before,
.full-stars:before {
  content: "\2605\2605\2605\2605\2605";
  font-size: 14pt;
}
.empty-stars:before {
  -webkit-text-stroke: 1px #848484;
}
.full-stars:before {
  -webkit-text-stroke: 1px orange;
}
</style>
