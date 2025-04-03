<script setup>
import { reactive } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
const router = useRouter();
const store = useStore();

let form = reactive({
  username: "Admin",
  password: "Test1234",
});

const user = reactive({
  userId: 1000123,
  firstName: "Test",
  lastName: "Devstein",
  userType: 2,
});
const handleLogin = async () => {
  if (
    form.username.toLocaleLowerCase() === "admin" &&
    form.password === "Test1234"
  ) {
    localStorage.setItem("token", JSON.stringify(user));
    store.commit("setAuth", true);
    store.commit("setUser", user);
    router.push("/dashboard");
  } else {
    alert("Invalid login");
  }
};
</script>
<template>
  <section class="bg-sky-100 min-h-lvh">
    <form @submit.prevent="handleLogin">
      <div class="container m-auto max-w-2xl py-24">
        <div
          class="bg-white px-6 py-8 mb-4 shadow-md rounded-md border m-4 md:m-0"
        >
          <h2 class="text-3xl text-center font-semibold mb-6 text-sky-700">
            Login
          </h2>
          <div class="mb-4 text-sky-700">
            <label class="block text-sky-700 font-bold mb-2">Username</label>
            <input
              v-model="form.username"
              type="text"
              id="username"
              name="username"
              class="border rounded w-full py-2 px-3 mb-2"
              placeholder="Username"
              required
            />
          </div>
          <div class="mb-4 text-sky-700">
            <label class="block text-sky-700 font-bold mb-2">Password</label>
            <input
              v-model="form.password"
              type="password"
              id="password"
              name="password"
              class="border rounded w-full py-2 px-3 mb-2"
              placeholder="Password"
              required
            />
          </div>
          <div>
            <button
              class="bg-sky-700 hover:bg-sky-400 text-white font-bold py-2 px-4 rounded-lg w-full focus:outline-none focus:shadow-outline"
              type="submit"
            >
              Login
            </button>
          </div>
        </div>
      </div>
    </form>
  </section>
</template>
