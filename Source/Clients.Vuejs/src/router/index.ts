import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: () => import("../views/Home.vue")
  },
  {
    path: "/client-list",
    name: "ClientList",
    component: () => import("../views/Client/ClientList.vue")
  },
  {
    path: "/client-create/",
    name: "CreateClient",
    component: () => import("../views/Client/ClientForm.vue")
  },
  {
    path: "/client-update",
    name: "UpdateClient",
    component: () => import("../views/Client/ClientForm.vue")
  },
  {
    path: "/address-list",
    name: "AddressList",
    component: () => import("../views/Address/AddressList.vue")
  },
  {
    path: "/address-create",
    name: "CreateAddress",
    component: () => import("../views/Address/AddressForm.vue")
  },
  {
    path: "/address-update",
    name: "UpdateAddress",
    component: () => import("../views/Address/AddressForm.vue")
  },
  {
    path: "/about",
    name: "About",
    component: () => import("../views/About.vue")
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes
});

export default router;
