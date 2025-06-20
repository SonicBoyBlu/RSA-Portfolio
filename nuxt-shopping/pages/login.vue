<template>
  <div
    class="min-h-screen bg-cover bg-center bg-no-repeat px-4 py-12"
    style="
      background-image: url(https://i.ytimg.com/vi/G2wuOPlfbew/maxresdefault.jpg);
    "
  >
    <div
      style="margin-top: 13%"
      class="max-w-md mx-auto backdrop-blur-md bg-white/70 rounded-2xl shadow-xl p-8 animate-fade-blur border border-white/40"
      :class="{ 'animate-shake': loginError }"
    >
      <h2
        class="text-2xl font-extrabold text-center text-gray-900 mb-6 tracking-wide"
      >
        Sign In to Continue
      </h2>

      <form @submit.prevent="handleLogin" class="space-y-4">
        <!-- Email -->
        <div>
          <p class="text-[#e0218a] text-center">
            {{ loginErrorMessage }}
          </p>
          <label class="block text-sm font-semibold text-gray-700 mb-1"
            >Email</label
          >
          <input
            v-model="email"
            type="email"
            placeholder="Enter your email"
            class="w-full px-4 py-2 border border-gray-300 rounded-lg bg-white/80 backdrop-blur-sm placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-[#e0218a]"
            required
          />
        </div>

        <!-- Password -->
        <div>
          <label class="block text-sm font-semibold text-gray-700 mb-1"
            >Password</label
          >
          <input
            v-model="password"
            type="password"
            placeholder="Enter your password"
            class="w-full px-4 py-2 border border-gray-300 rounded-lg bg-white/80 backdrop-blur-sm placeholder-gray-500 focus:outline-none focus:ring-2 focus:ring-[#e0218a]"
            required
          />
        </div>

        <!-- Submit Button -->
        <div>
          <button
            type="submit"
            class="w-full bg-[#e0218a] text-white py-2 rounded-lg hover:bg-[#c81a76] transition flex justify-center items-center font-semibold"
            :disabled="loading"
          >
            <svg
              v-if="loading"
              class="animate-spin h-5 w-5 text-white"
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
            >
              <circle
                class="opacity-25"
                cx="12"
                cy="12"
                r="10"
                stroke="currentColor"
                stroke-width="4"
              ></circle>
              <path
                class="opacity-75"
                fill="currentColor"
                d="M4 12a8 8 0 018-8v4l3-3-3-3v4a8 8 0 00-8 8z"
              ></path>
            </svg>
            <span v-else>Sign In</span>
          </button>
        </div>
      </form>

      <div class="text-center text-sm text-gray-700 mt-4">
        <NuxtLink to="/forgot-password" class="text-[#e0218a] hover:underline">
          Forgot your password?
        </NuxtLink>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;
useHead({
  title: siteTitle + " | Signin",
});

if (userIsAuth()) {
  navigateTo("/MyAccount");
}

const email = ref("demo@email.com");
const password = ref("password");
const loading = ref(false);
const loginError = ref(false);
let loginErrorMessage = ref("");

const handleLogin = async () => {
  loading.value = true;
  loginError.value = false;

  const data = await $fetch("/api/auth", {
    method: "POST",
    body: { username: email.value, password: password.value },
  });

  loading.value = false;
  loginError.value = !data.status;
  loginErrorMessage.value = data.message;

  if (data.status === true) {
    saveCurrentUser(data.user);
    navigateTo("/MyAccount");
  }
};
</script>

<style scoped>
@keyframes fade-blur {
  0% {
    opacity: 0;
    transform: translateY(30px);
    filter: blur(10px);
  }
  100% {
    opacity: 1;
    transform: translateY(0);
    filter: blur(0);
  }
}

.animate-fade-blur {
  animation: fade-blur 0.8s ease-out forwards;
}

@keyframes shake {
  0%,
  100% {
    transform: translateX(0);
  }
  20%,
  60% {
    transform: translateX(-8px);
  }
  40%,
  80% {
    transform: translateX(8px);
  }
}

.animate-shake {
  animation: shake 0.5s ease-in-out;
}
</style>
