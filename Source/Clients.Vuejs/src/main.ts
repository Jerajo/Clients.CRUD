import Vue from "vue";
import App from "./App.vue";
import "./registerServiceWorker";

import router from "./router";
import store from "./store";

// Bootstrap and Bootstrap-Vue
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
Vue.use(BootstrapVue);
Vue.use(IconsPlugin);

import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

// Vee-Validate Rules and
import * as rules from "../node_modules/vee-validate/dist/rules";
import { ValidationObserver, extend, ValidationProvider } from "vee-validate";

for (const [rule, validation] of Object.entries(rules)) {
  extend(rule, {
    ...validation
  });
}

Vue.component("ValidationProvider", ValidationProvider);
Vue.component("ValidationObserver", ValidationObserver);

Vue.config.productionTip = false;

// class-component-hooks.js
import Component from "vue-class-component";

// Register the router hooks with their names
Component.registerHooks([
  "beforeRouteEnter",
  "beforeRouteLeave",
  "beforeRouteUpdate"
]);

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
