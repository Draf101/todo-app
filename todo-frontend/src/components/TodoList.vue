<template>
  <div class="todo-list">
    <div
      v-for="todo in sortedTodos"
      :key="todo.id"
      class="todo-item"
      :class="{ expanded: todo.expanded }"
    >
      <div class="todo-header">
        <h3 class="todo-title">{{ todo.title }}</h3>
        <span class="todo-state">{{ getStateText(todo.state) }}</span>
      </div>
      <p class="todo-content">
        {{ todo.expanded ? todo.content : truncatedContent(todo.content) }}
      </p>
      <div class="btn-group">
        <button
          v-if="isTextOverflowing(todo.content)"
          class="arrow"
          @click="toggleExpand(todo)"
        >
          {{ todo.expanded ? "▲ Show less" : "▼ Show more" }}
        </button>
        <div class="btn-right-group">
          <button class="btn-edit" @click="editTodo(todo)">Edit</button>
          <button class="btn-delete" @click="deleteTodo(todo.id)">Delete</button>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    todos: Array,
  },
  data() {
    return {
      sortBy: "creationDate",
    };
  },
  computed: {
    sortedTodos() {
      return [...this.todos].sort((a, b) => new Date(b.creationDate) - new Date(a.creationDate));
    },
  },
  methods: {
    deleteTodo(id) {
      this.$emit("delete-todo", id);
    },
    editTodo(todo) {
      this.$emit("edit-todo", todo);
    },
    getStateText(state) {
      const stateMap = {
        1: "Open",
        2: "InProgress",
        3: "Finished",
      };
      return stateMap[state];
    },
    toggleExpand(todo) {
      todo.expanded = !todo.expanded;
    },
    truncatedContent(content) {
      const maxLength = 150;
      return content.length > maxLength
        ? content.substring(0, maxLength) + "..."
        : content;
    },
    isTextOverflowing(content) {
      const maxLength = 100; 
      return content.length > maxLength;
    },
  },
};
</script>

<style lang="scss" scoped>
.todo-list {
  width: 600px;
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.todo-item {
  background-color: #424242;
  border: 1px solid #000000;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  max-width: 600px;
  width: 90%;
  margin: 0 auto;
  display: flex;
  flex-direction: column;

  &.expanded {
    max-height: unset;
  }

  .todo-header {
    display: flex;
    justify-content: space-between;
    align-items: center;

    .todo-title {
      font-size: 1.5rem;
      font-weight: bold;
      margin: 0;
      text-overflow: ellipsis;
      text-transform: uppercase;
      white-space: nowrap;
      overflow: hidden;
      max-width: 50%;
    }

    .todo-state {
      font-weight: 500;
      font-size: 1rem;
      color: #f6ff79;
    }
  }

  .todo-content {
    text-align: left;
    font-size: 1rem;
    margin: 12px 0;
    color: #ffffff;
    word-wrap: break-word;
    overflow: hidden;
    line-height: 1.5;
    max-height: 400px; 
  }

  .btn-group {
    display: flex;
    align-items: center;

    .arrow {
      background: none;
      font-size: 1rem;
      color: #ffffff;
      cursor: pointer;
      border: none;
      padding: 0;
    }

    .btn-right-group {
      margin-left: auto;
      display: flex;
      gap: 8px;
    }
  }
}
</style>

