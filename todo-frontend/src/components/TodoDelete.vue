<template>
  <div v-if="isVisible" class="popup-overlay">
    <div class="popup">
      <h2>Delete?</h2>
      <div class="btn-group-middle">
        <button @click="confirm" class="btn-submit">Delete</button>
        <button @click="cancel" class="btn-cancel">Cancel</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["isVisible"],
  emits: ["confirm", "cancel"],
  methods: {
    confirm() {
      this.$emit("confirm");
    },
    cancel() {
      this.$emit("cancel");
    },
  },
};
</script>