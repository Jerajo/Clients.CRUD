<template>
  <div id="app">
    <div id="nav" class="nav nav-container">
      <router-link class="nav nav-link" to="/">Client List</router-link>
    </div>
    <router-view />
  </div>
</template>

<style lang="scss">
@use "./node_modules/bootstrap/scss/bootstrap.scss";

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
