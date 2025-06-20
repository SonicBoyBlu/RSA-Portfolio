<template>
  <div
    class="animate-spin rounded-full border-solid"
    :class="[_color, _size, _border]"
  ></div>
</template>
<script lang="ts" setup>
const props = defineProps(["color", "size"]);
const _color: string = props.color || "border-[#c026d3]";
let _size: string = props.size || "16";
const _border = _size == "5" ? "border-t-2" : "border-t-4";
_size = "h-" + _size + " w-" + _size;
</script>
