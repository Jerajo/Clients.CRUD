import Vue from "vue";
import App from "./App.vue";
import "./registerServiceWorker";
import router from "./router";
import store from "./store";
import * as rules from "../node_modules/vee-validate/dist/rules";
import { ValidationObserver, extend, ValidationProvider } from "vee-validate";

for (const [rule, validation] of Object.entries(rules)) {
  console.log("rules", rule);
  extend(rule, {
    ...validation
  });
}

Vue.component("ValidationProvider", ValidationProvider);

Vue.component("ValidationObserver", ValidationObserver);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
