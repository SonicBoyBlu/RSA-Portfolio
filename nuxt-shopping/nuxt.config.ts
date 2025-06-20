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
        {
          rel: "icon",
          type: "image/x-icon",
          href: "/favicon.ico",
        },
        {
          rel: "icon",
          type: "image/x-icon",
          sizes: "64x64",
          href: "/haute_sucre_favicon_64x64.ico",
        },
        {
          rel: "icon",
          type: "image/x-icon",
          sizes: "128x128",
          href: "/haute_sucre_favicon_128x128.ico",
        },
        {
          rel: "icon",
          type: "image/x-icon",
          sizes: "256x256",
          href: "/haute_sucre_favicon_256x256.ico",
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