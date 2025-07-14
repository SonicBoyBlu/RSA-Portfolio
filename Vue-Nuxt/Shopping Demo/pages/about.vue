<template>
  <section class="bg-black text-white min-h-screen">
    <!-- Hero Section -->
    <div class="relative">
      <img
        src="https://images.pexels.com/photos/3394656/pexels-photo-3394656.jpeg"
        alt="Cyberpunk model in neon"
        class="w-full h-[60vh] object-cover brightness-75"
      />
      <div class="absolute inset-0 flex items-center justify-center">
        <h1 class="text-5xl font-bold text-white text-center">
          About Haute Sucre
        </h1>
      </div>
    </div>

    <!-- Main Content -->
    <div
      class="max-w-6xl mx-auto px-6 py-16 grid md:grid-cols-2 gap-12 items-center"
    >
      <div>
        <h2 class="text-3xl font-bold mb-4">Style with an Edge</h2>
        <p class="text-neutral-300 mb-4">
          Haute Sucre isn't just fashion — it's a rebellion. Born from a
          collision of queer identity, cyber aesthetics, and unapologetic
          sensuality, we craft looks that speak louder than words. This is not
          for the mainstream — it's for the bold, the electric, the beautiful
          outliers.
        </p>
        <p class="text-neutral-400">
          From baroque hangers to midnight fabrics, every detail is curated to
          embrace contrast: hard lines, soft edges, shadowed light. Haute Sucre
          celebrates desire and defiance, woven together into the seams of every
          piece.
        </p>
      </div>
      <div>
        <img
          src="https://images.pexels.com/photos/2795458/pexels-photo-2795458.jpeg"
          alt="Masculine model posing in cyberwear"
          class="w-full h-full object-cover rounded-lg shadow-lg"
        />
      </div>
    </div>

    <!-- Team / Mission Section -->
    <div class="bg-neutral-900 py-16 px-6">
      <div class="max-w-4xl mx-auto text-center">
        <h2 class="text-3xl font-bold mb-4">Our Pulse</h2>
        <p class="text-neutral-400 mb-6">
          We're a collective of dreamers, rebels, and romantics — designers and
          visionaries bringing masculine queer fashion into the neon-lit future.
          We blur the lines of gender and genre, stitched in confidence and
          sealed in sugar.
        </p>
        <img
          src="https://images.pexels.com/photos/1082528/pexels-photo-1082528.jpeg"
          alt="Androgynous model in cyberpunk look"
          class="mx-auto rounded-xl w-full max-w-xl object-cover shadow-xl"
        />
      </div>
    </div>
  </section>
</template>

<script setup>
const config = useRuntimeConfig();
const siteTitle = config.public.siteTitle;
useHead({
  title: siteTitle + " | About",
});
</script>
