<template>
  <SectionCard>
    <h1>Testing... {{ _count }}</h1>
    <div class="grid grid-cols-4 gap-3">
      <button @click="btnCountClickDown" class="btn btn-submit">
        Click Me (-)
      </button>
      <button @click="btnCountClickUp" class="btn btn-submit">
        Click Me (+)
      </button>
      <button class="btn btn-cta" @click="btnCountRest">Reset</button>
      <NuxtLink to="Test2">Next >></NuxtLink>
    </div>
  </SectionCard>
</template>

<script lang="ts" setup>
definePageMeta({
  layout: "test",
});
const keyTesting = "key-testing";

const _state = useState<number>("state-test");
const _count = ref(0);

//*
watch(
  _count,
  (data) => {
    //localStorage.setItem(keyTesting, JSON.stringify(data));
    updateCount();
  },
  { deep: true }
);
//*/

const btnCountClickUp = () => {
  _count.value = (_count.value || 0) + 3;
};

const btnCountClickDown = () => {
  _count.value = (_count.value || 0) - 3;
};
const btnCountRest = () => {
  _count.value = 0;
};

const updateCount = () => {
  _state.value = _count.value;
  localStorage.setItem(keyTesting, JSON.stringify(_count.value));
};

onMounted(() => {
  _count.value = reloadSavedData();
});

const reloadSavedData = () => {
  const _saved = localStorage.getItem(keyTesting);
  _state.value = _saved ? JSON.parse(_saved) : 0;
  return _state.value;
};
</script>
