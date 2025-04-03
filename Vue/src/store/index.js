import { createStore } from "vuex";

//Vue.use(Vuex);

export default createStore({
  state: {
    isAuthenticated: false,
    // Other state variables
    user: {
      firstName: "Guest",
    },
  },
  mutations: {
    setAuth(state, value) {
      state.isAuthenticated = value;
      //saveState();
      sessionStorage.setItem("__state", JSON.stringify(state));
    },
    setUser(state, user) {
      state.user = user;
      //saveState();
      sessionStorage.setItem("__state", JSON.stringify(state));
    },
    loadState(state) {
      var json = JSON.parse(sessionStorage.getItem("__state"));
      state.isAuthenticated = json.isAuthenticated;
      state.user = json.user;
    },
    // Other mutations
  },
  actions: {
    login({ commit }) {
      // Perform login logic (e.g., API call)
      // On success:
      commit("setAuth", true);
      // On failure:
      // commit('setAuth', false);
    },
    logout({ commit }) {
      // Perform logout logic
      commit("setAuth", false);
    },
    // Other actions
  },
  getters: {
    isAuthenticated: (state) => state.isAuthenticated,
    // Other getters
  },
});
