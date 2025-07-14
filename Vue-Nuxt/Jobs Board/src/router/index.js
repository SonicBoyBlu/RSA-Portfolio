import { createRouter, createWebHistory } from "vue-router";
import HomeView from "@/views/HomeView.vue";
import JobsView from "@/views/JobListView.vue";
import JobsDetailView from "@/views/JobDetailView.vue";
import JobModifyListingView from "@/views/JobModifyListingView.vue";
import LoginView from "@/views/LoginView.vue";
import DashboardAdminView from "../views/DashboardAdminView.vue";
import NotFoundView from "@/views/_NotFoundView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
    {
      path: "/jobs",
      name: "jobs",
      component: JobsView,
    },
    {
      path: "/jobs/:id",
      name: "job",
      component: JobsDetailView,
    },
    {
      path: "/Dashboard",
      name: "dashboard-admin",
      component: DashboardAdminView,
      meta: { requiresAuth: true },
    },
    {
      path: "/jobs/add",
      name: "job-add",
      component: JobModifyListingView,
      meta: { requiresAuth: true },
    },
    {
      path: "/jobs/edit/:id",
      name: "job-edit",
      component: JobModifyListingView,
      meta: { requiresAuth: true },
    },
    {
      path: "/login",
      name: "login",
      component: LoginView,
    },
    {
      path: "/:catchAll(.*)",
      name: "not-found",
      component: NotFoundView,
    },
  ],
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    // Check if the user is logged in
    if (localStorage.getItem("token")) {
      next(); // Proceed to the route
    } else {
      next("/login"); // Redirect to login page
    }
  } else {
    next(); // Allow access to other routes
  }
});

export default router;
