import "./assets/main.css";
import { createApp } from "vue";
import App from "./App.vue";
//import App from "./App-composition.vue";

import router from "./router";
import store from "@/store";

import toast from "vue-toastification";
import "vue-toastification/dist/index.css";

const app = createApp(App);
app.use(router);
app.use(store);
app.use(toast);

/*
// primitive User object
app.config.globalProperties.User = {
  type: 0,
  profile: {
    firstName: "Guest",
    lastName: "",
  },
};
app.config.globalProperties.$testFunction = () => {
  alert("Global function triggered");
};
*/
app.mount("#app");
