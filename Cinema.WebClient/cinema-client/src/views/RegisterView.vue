<template>
    <div>
        <h2>Реєстрація</h2>
        <form @submit.prevent="register">
            <div>
                <label for="username">Ім'я користувача:</label>
                <input type="text" id="username" v-model="formData.username" required>
            </div>
            <div>
                <label for="email">Email:</label>
                <input type="email" id="email" v-model="formData.email" required>
            </div>
            <div>
                <label for="password">Пароль:</label>
                <input type="password" id="password" v-model="formData.password" required>
            </div>
            <div>
                <label for="repeatPassword">Повторіть пароль:</label>
                <input type="password" id="repeatPassword" v-model="formData.repeatPassword" required>
            </div>
            <button type="submit">Зареєструватися</button>
        </form>
    </div>
</template>

<script>
export default {
  data() {
    return {
      formData: {
        username: '',
        email: '',
        password: '',
        repeatPassword: ''
      }
    };
  },
  methods: {
    register() {
      // Ваш код для обробки реєстрації тут
      // Наприклад, ви можете відправити дані на сервер або зберегти їх локально
      console.log('Дані реєстрації:', this.formData);
    }
  }
};
</script>

<style scoped>
    /* Оформлення стилів форми тут */
</style>
