<script>
import AppBar from './components/AppBar.vue';

export default {
  components: {
    AppBar
  }
}
</script>

<template>
  <app-bar></app-bar>
  <router-view/>
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}
</style>
