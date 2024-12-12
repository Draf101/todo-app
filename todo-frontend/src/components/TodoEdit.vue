<template>
  <div v-if="isVisible" class="popup-overlay">
    <div class="popup">
      <h2>Edit Item</h2>
      <form @submit.prevent="submit">
        <div class="btn-group-middle">
          <select id="state" v-model="todo.state">
            <option value="1">Open</option>
            <option value="2">InProgress</option>
            <option value="3">Finished</option>
          </select>
            <button type="submit" class="btn-submit">Save</button>
            <button type="button" class="btn-cancel" @click="cancel">Cancel</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  props: ["todo", "isVisible"],
  emits: ["save", "cancel"],
  methods: {
    submit() {
      this.$emit("save", this.todo);
    },
    cancel() {
      this.$emit("cancel");
    },
  },
};
</script>
  
<style lang="scss" scoped>
select {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 1rem;
}
</style>
