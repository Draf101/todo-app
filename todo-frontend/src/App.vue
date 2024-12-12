<template>
  <div class="page-container">
      <div class="main-content">
        <h1>TODO APP</h1>
        <div>
          <TodoCreate
            @todo-added="handleAddTodo" 
          />
          <TodoList
            :todos="todos"
            @edit-todo="startEditing"
            @delete-todo="startDeleting"
          />
          <TodoEdit
            v-if="editingTodo"
            :todo="editingTodo"
            :isVisible="isEditModalVisible"
            @save="updateTodo"
            @cancel="cancelEditing"
          />
          <TodoDelete
            v-if="deletingTodoId"
            :isVisible="isDeleteModalVisible"
            @confirm="deleteTodo"
            @cancel="cancelDeleting"
          />
        </div>
      </div>
  </div>
</template>

<script>
import TodoCreate from "./components/TodoCreate.vue";
import TodoList from "./components/TodoList.vue";
import TodoEdit from "./components/TodoEdit.vue";
import TodoDelete from "./components/TodoDelete.vue";
import axios from 'axios';

export default {
  components: { TodoCreate, TodoList, TodoEdit, TodoDelete },
  data() {
    return {
      todos: [],
      editingTodo: null,
      deletingTodoId: null,
      isEditModalVisible: false,
      isDeleteModalVisible: false,
    };
  },
  methods: {
    async handleAddTodo(newTodo) {
    try {
      const response = await axios.post("http://localhost:7277/todo", newTodo, {
        headers: {
          "Content-Type": "application/json",
        },
      });
      this.fetchTodos(); // Reload the list after the item is added
    } catch (error) {
      console.error("Error adding new todo:", error);
    }
  },
    async fetchTodos() {
      try {
        const response = await axios.get('http://localhost:7277/todo');
        this.todos = response.data; // Ensure this gets populated
        } catch (error) {
        console.error('Error fetching todos:', error);
      }
    },
    startEditing(todo) {
      this.editingTodo = { ...todo }; // Clone the todo for editing
      this.isEditModalVisible = true;
    },
    cancelEditing() {
      this.editingTodo = null;
      this.isEditModalVisible = false;
    },
    async updateTodo(updatedTodo) {
      try {
        updatedTodo.state = Number(updatedTodo.state);
        await axios.put(`http://localhost:7277/todo/${updatedTodo.id}`, updatedTodo, {
          headers: {
            'Content-Type': 'application/json',
          },
        });
        this.fetchTodos();
        this.cancelEditing();
      } catch (error) {
        console.error('Error updating todo:', error);
      }
    },
    startDeleting(id) {
      this.deletingTodoId = id;
      this.isDeleteModalVisible = true;
    },
    cancelDeleting() {
      this.deletingTodoId = null;
      this.isDeleteModalVisible = false;
    },
    async deleteTodo() {
      try {
        await axios.delete(`http://localhost:7277/todo/${this.deletingTodoId}`);
        this.fetchTodos();
        this.cancelDeleting();
      } catch (error) {
        console.error('Error deleting todo:', error);
      }
    },
  },
  mounted() {
    this.fetchTodos();
  },
};
</script>