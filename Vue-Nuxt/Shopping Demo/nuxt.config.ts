// https://nuxt.com/docs/api/configuration/nuxt-config
const siteTitle: string = "Haute Sucre";
export default defineNuxtConfig({
  compatibilityDate: "2024-11-01",
  devtools: {
    enabled: true,

    timeline: {
      enabled: true,
    },
  },
  modules: ["@nuxtjs/tailwindcss"],
  app: {
    head: {
      title: siteTitle,
      meta: [
        {
          name: "description",
          content: "Demo shopping chart using Nuxt 3 and FakeStoreAPI",
        },
      ],
      link: [
        {
          rel: "stylesheet",
          href: "https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200",
        },
        { rel: "icon", type: "image/x-icon", href: "/favicon.ico" },
        {
          rel: "apple-touch-icon",
          sizes: "180x180",
          href: "/apple_icon_180x180.png",
        },
        {
          rel: "apple-touch-icon",
          sizes: "152x152",
          href: "/apple_icon_152x152.png",
        },
        {
          rel: "apple-touch-icon",
          sizes: "120x120",
          href: "/apple_icon_120x120.png",
        },
        {
          rel: "apple-touch-icon",
          sizes: "76x76",
          href: "/apple_icon_76x76.png",
        },
      ],
    },
  },
  runtimeConfig: {
    public: {
      siteTitle: siteTitle,
    },
    apiKeys: {
      currencies: "API_Key_Sugar", //process.env.API_KEY_CURRENCIES
    },
  },
});
