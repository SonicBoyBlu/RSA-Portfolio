<script setup lang="ts">
onMounted(() => {
  localStorage.clear();
  navigateTo("/login");
});
</script>
