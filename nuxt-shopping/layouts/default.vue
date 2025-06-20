<template>
  <div>
    <SiteHeader />
    <slot />
  </div>
</template>
<style lang="scss" scoped>
header {
  /*margin-bottom: -98px;*/
  z-index: 999;
  position: fixed;
  right: 0;
  left: 0;
  background-color: transparent !important;

  &.shadow-lg {
    box-shadow: none !important;
  }
}
</style>
