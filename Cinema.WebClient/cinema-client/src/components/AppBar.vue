<script>
export default {
  data() {
    return {
      showNavbar: true
    };
  },
  watch: {
    // Show navbar dynamicaly based on current route
    $route(to) {
      this.showNavbar = !["/login", "/register"].includes(to.path);
    }
  },

  methods: {
    login() {
      this.$router.push("/login");
      // Simulating logged in/out session logic
      // Remove after implementation
      this.isLoggedIn = true;
    },
    logout() {
      this.$router.push("/");
      // Simulating logged in/out session logic
      // Remove after implementation
      this.isLoggedIn = false;
      this.$forceUpdate();
    },
    register() {
      this.$router.push("/register");
    },
    openAccount() {
      this.$router.push('/account');
    }
  }
};
</script>

<template>
  <div class="app-bar" v-if="showNavbar">
    <nav>
      <ul>
        <li><router-link to="/">Movies</router-link></li>
        <li><router-link to="/about">About</router-link></li>
      </ul>
    </nav>
    <div class="registration-buttons">
      <template v-if="isLoggedIn">
        <button class="logout-button" @click="logout">Logout</button>
        <button class="account-button" @click="openAccount">Account</button>
      </template>
      <template v-else>
        <button class="login-button" @click="login">Login</button>
        <button class="signup-button" @click="register">Sign Up</button>
      </template>
    </div>
  </div>
</template>

<style scoped>
.app-bar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
  margin: 15px;

  nav {
    color: #20232a;
    padding: 10px 20px;
  }

  ul {
    list-style-type: none;
    margin: 0;
    padding: 0;
  }

  li {
    display: inline;
    margin-right: 20px;
  }

  a {
    font-weight: bold;
    color: #20232a;
    text-decoration: none;
  }

  a:hover {
    color: #8b8bc2;
  }

  .router-link-exact-active {
    color: #8b8bc2;
  }

  .registration-buttons {
    display: flex;
    gap: 15px;
  }

  button {
    width: 90px;
    height: 40px;
    font-weight: bold;
    color: #20232a;
    border: none;
    border-radius: 20px
  }

  .signup-button, .account-button {
    color: white;
    background-color: #20232a;
  }

  button:hover {
    opacity: 0.8;
  }
}
</style>