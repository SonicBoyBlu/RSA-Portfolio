<template>
  <header class="shadow-lg bg-white sm:flex sm:justify-between">
    <div class="flex items-center justify-between">
      <div>
        <NuxtLink to="/" class="whitespace-nowrap">
          <img
            src="/assets/images/logo.png"
            id="logo-main"
            class="rounded-lg"
          />
          <h1
            class="font-bold pt-5 mb-5 text-lg:flex lg:text-6xl md:text-3xl sm:text-2xl:flex text-fuchsia-600 inline-block"
          >
            {{ siteTitle }}
          </h1>
        </NuxtLink>
      </div>

      <div class="sm:hidden">
        <button type="button" class="btn block" @click="isOpen = !isOpen">
          <span v-if="!isOpen" class="material-symbols-outlined text-6xl">
            menu
          </span>
          <span v-if="isOpen" class="material-symbols-outlined text-6xl">
            close
          </span>
        </button>
      </div>
    </div>

    <div
      :class="isOpen ? 'block bg-white' : 'hidden'"
      class="sm:flex mt-5"
      id="nav-main"
    >
      <NuxtLink to="/" class="btn" title="Home">Home</NuxtLink>
      <NuxtLink to="/About" class="btn" title="About">About</NuxtLink>
      <NuxtLink to="/Lookbook" class="btn" title="Lookbook">Lookbook</NuxtLink>
      <NuxtLink to="/Shop" class="btn" title="Shop">Shop</NuxtLink>
      <NuxtLink v-if="!isAuth" to="/Login" class="btn" title="Signin"
        >Login</NuxtLink
      >
      <NuxtLink v-if="isAuth" to="/MyAccount" class="btn" title="My Account"
        ><span class="material-symbols-outlined"> manage_accounts </span>
        <span class="sm:hidden sm:ml-3 ml-3">My Account</span></NuxtLink
      >
      <NuxtLink v-if="isAuth" to="/Logout" class="btn" title="Signoff"
        ><span class="material-symbols-outlined"> mode_off_on </span>
        <span class="sm:hidden ml-3">Logout</span></NuxtLink
      >
    </div>
  </header>
</template>

<script setup lang="ts">
const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;
let user = useCurrentUser();
const isAuth = ref(false); //user.isAuth);

const isOpen = ref(false);

const handleResize = () => {
  isOpen.value = false;
};

onMounted(() => {
  window.addEventListener("resize", handleResize);
  document.querySelector("#nav-main")?.addEventListener("click", handleResize);

  try {
    console.log("Looking up previously saved user");
    const savedUser = localStorage.getItem("user-current");
    if (savedUser) {
      user = JSON.parse(savedUser);
      isAuth.value = user.isAuth;
    }
  } catch {}
});
</script>

<style lang="scss" scoped>
#logo-main {
  height: 75px;
  display: inline-block;
  vertical-align: middle;
  /*margin: -25px 5px 0px -10px;*/
  margin: -25px 5px 0px 10px;
}
/*
h1 {
  display: inline-block;
  margin: 10px 0;
}
*/

a {
  @apply text-xl font-semibold rounded hover:bg-gray-600 text-left block;
}
header {
  > div {
    padding: 5px;
  }
  li > a {
    @apply mt-5 ml-5 px-2 py-2 sm:my-5;
  }
}
</style>
