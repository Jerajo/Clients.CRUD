import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Client List",
    component: () => import("../views/Client/ClientList.vue")
  },
  {
    path: "/client-form",
    name: "Client Form",
    component: () => import("../views/Client/ClientForm.vue")
  },
  {
    path: "/address-form",
    name: "Address Form",
    component: () => import("../views/Address/AddressForm.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
