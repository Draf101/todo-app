<template>
  <div class="todo-create">
    <button class="btn" @click="openPopup">Add Item</button>
    <div v-if="isPopupVisible" class="popup-overlay">
      <div class="popup">
        <h2>Add New Item</h2>
        <form @submit.prevent="addTodo">
          <div class="form-group title-state-group">
            <div class="input-title">
              <input
                id="title"
                type="text"
                v-model="newTodo.title"
                required
                placeholder="Enter title"
              />
            </div>
            <div class="input-state">
              <select id="state" v-model="newTodo.state">
                <option :value="1">Open</option>
                <option :value="2">InProgress</option>
                <option :value="3">Finished</option>
              </select>
            </div>
          </div>
          <div class="form-group">
            <textarea
              id="content"
              v-model="newTodo.content"
              required
              placeholder="Enter content"
            ></textarea>
          </div>
          <div class="btn-group-middle">
            <button type="submit" class="btn-submit">Add</button>
            <button type="button" class="btn-cancel" @click="closePopup">
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isPopupVisible: false,
      newTodo: {
        title: "",
        state: 1,
        content: "",
      },
    };
  },
  methods: {
    openPopup() {
      this.isPopupVisible = true;
    },
    closePopup() {
      this.isPopupVisible = false;
      this.resetForm();
    },
    async addTodo() {
      try {
        this.$emit("todo-added", this.newTodo); 
        this.closePopup();
      } catch (error) {
        console.error("Error adding todo:", error);
      }
    },
    resetForm() {
      this.newTodo = {
        title: "",
        state: 1,
        content: "",
      };
    },
  },
};
</script>

<style lang="scss" scoped>
.todo-create {
  padding-bottom: 2rem;
}

.form-group {
  margin-bottom: 16px;

  &.title-state-group {
    display: flex;
    justify-content: space-between;
    gap: 20px;

    .input-title {
      flex: 2;
    }

    .input-state {
      flex: 1;
    }
  }

  input,
  select {
    width: 100%;
    padding: 10px 0px 10px 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 1rem;
    resize: none;
  }
  
  textarea {
    width: 97%;
    padding: 10px 0px 10px 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 1rem;
    resize: none;
  }
}
</style>