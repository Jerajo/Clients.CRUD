import Vue from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import store from "./store";
// import { ValidationProvider } from "vee-validate/dist/vee-validate.full.esm";
import { ValidationProvider, ValidationObserver } from "vee-validate";

Vue.component("ValidationProvider", ValidationProvider);

Vue.component("ValidationObserver", ValidationObserver);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
