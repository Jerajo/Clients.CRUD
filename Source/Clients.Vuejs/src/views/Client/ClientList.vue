<template>
  <div class="container">
    <h1 class="my-4">Client List</h1>
    <button
      id="createButton"
      @click="goToClientForm(null)"
      class="btn btn-success mx-auto mb-4"
    >
      Create new client
    </button>
    <div v-if="loading">
      <h2>Loading clients...</h2>
      <b-icon icon="arrow-clockwise" font-scale="7.5" animation="spin"></b-icon>
    </div>
    <div v-else-if="hasClients === false" class="text-md-center">
      <h2>There are not clients on the list yet.</h2>
    </div>
    <div v-else>
      <b-container>
        <!-- Client Header -->
        <b-row cols="5" align-v="center" class="bg-secondary text-light py-2">
          <b-col cols="2">
            <span class="d-none d-lg-block">Expand</span>
          </b-col>
          <b-col cols="2" class="text-break">User Name</b-col>
          <b-col cols="4" class="text-break">E-Mail</b-col>
          <b-col cols="3" class="text-break">Birth Day</b-col>
          <b-col cols="1">
            <span class="d-none d-lg-block">Actions</span>
          </b-col>
        </b-row>

        <!-- Client List -->
        <div v-for="client in clients" :key="client.id">
          <b-row cols="5" align-v="center" class="py-2">
            <b-col cols="2" class="text-break">
              <div class="d-flex flex-row justify-content-center">
                <button
                  v-show="client.addresses.length > 0"
                  v-b-toggle="client.id"
                  title="Show Addresses"
                  class="btn btn-outline-secondary"
                >
                  <b-icon icon="caret-up-fill"></b-icon>
                </button>
                <button
                  title="Add Address"
                  @click="goToAddressForm(client.id, null)"
                  class="btn btn-outline-secondary ms-2"
                >
                  <b-icon icon="geo-alt-fill"></b-icon>
                </button>
              </div>
            </b-col>
            <b-col cols="2" class="text-break">
              {{ client.userName }}
            </b-col>
            <b-col cols="4" class="text-break">
              {{ client.email }}
            </b-col>
            <b-col cols="3" class="text-break">
              {{ client.birthDay }}
            </b-col>
            <b-col cols="1">
              <b-dropdown variant="outline-secondary" no-caret right>
                <template #button-content>
                  <b-icon
                    icon="three-dots-vertical"
                    title="Show Actions"
                  ></b-icon>
                </template>
                <b-card class="menu-card">
                  <button
                    @click="goToClientForm(client.id)"
                    class="btn btn-success w-100 mb-2"
                  >
                    <b-icon icon="pencil-fill"></b-icon>
                    <span class="ms-2">Edit</span>
                  </button>
                  <button
                    @click="deleteClient(client.id)"
                    class="btn btn-danger w-100"
                  >
                    <b-icon icon="trash-fill"></b-icon>
                    <span class="ms-2">Delete</span>
                  </button>
                </b-card>
              </b-dropdown>
            </b-col>
          </b-row>

          <!-- Collapse -->
          <b-collapse :id="client.id" class="mx-3">
            <!-- Address Header -->
            <b-row
              cols="4"
              align-v="center"
              class="bg-secondary text-light py-2"
            >
              <b-col cols="6" class="text-break">Address Line One</b-col>
              <b-col cols="2" class="text-break">Postal Code</b-col>
              <b-col cols="3" class="text-break">Country</b-col>
              <b-col cols="1">
                <span class="d-none d-lg-block">Actions</span>
              </b-col>
            </b-row>

            <!-- Address List -->
            <div v-for="address in client.addresses" :key="address.id">
              <b-row cols="4" align-v="center" class="addresses py-2">
                <b-col cols="6" class="text-break">
                  {{ address.addressLineOne }}
                </b-col>
                <b-col cols="2" class="text-break">
                  {{ address.postalCode }}
                </b-col>
                <b-col cols="3" class="text-break">
                  {{ address.country }}
                </b-col>
                <b-col cols="1">
                  <b-dropdown variant="outline-secondary" no-caret right>
                    <template #button-content>
                      <b-icon
                        icon="three-dots-vertical"
                        title="Show Actions"
                      ></b-icon>
                    </template>
                    <b-card class="menu-card">
                      <button
                        @click="goToAddressForm(client.id, address.id)"
                        class="btn btn-success w-100 mb-2"
                      >
                        <b-icon icon="pencil-fill"></b-icon>
                        <span class="ms-2">Edit</span>
                      </button>
                      <button
                        @click="deleteAddress(client.id, address.id)"
                        class="btn btn-danger w-100"
                      >
                        <b-icon icon="trash-fill"></b-icon>
                        <span class="ms-2">Delete</span>
                      </button>
                    </b-card>
                  </b-dropdown>
                </b-col>
              </b-row>
            </div>
          </b-collapse>
        </div>
      </b-container>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Client } from "../../models/Client";
import { handleError } from "../../helpers";
import { WebClient, Endpoints } from "../../helpers/WebClient";
import { Guid } from "guid-typescript";

@Component
export default class ClientList extends Vue {
  loading: boolean;
  clients: Client[];
  webClient = new WebClient();

  constructor() {
    super();

    this.loading = true;
    this.clients = [];
    this.webClient = new WebClient();
  }

  get hasClients() {
    const result = this.clients.length > 0;

    return result;
  }

  mounted(): void {
    this.getClients();
  }

  getClients(): void {
    this.webClient
      .GET(Endpoints.Clients)
      .then(value => {
        this.clients = value.data as Client[];
      })
      .catch(handleError)
      .then(() => {
        this.loading = false;
      });
  }

  goToClientForm(id: Guid | null) {
    const clientId = id ? id.toString() : null;
    const operation = clientId ? "Edit" : "Create";

    this.$router.push({
      path: "/client-form",
      query: { operation, clientId }
    });
  }

  goToAddressForm(targetClientId: Guid, id: Guid | null) {
    const addressId = id ? id.toString() : null;
    const operation = addressId ? "Edit" : "Create";
    const clientId = targetClientId.toString();

    this.$router.push({
      path: "/address-form",
      query: { operation, clientId, addressId }
    });
  }

  deleteClient(clientId: Guid) {
    this.webClient
      .DELETE(`${Endpoints.Clients}/${clientId}`)
      .then(() => {
        this.clients = this.clients.filter(x => x.id !== clientId);
      })
      .catch(handleError);
  }

  deleteAddress(clientId: Guid, addressId: Guid) {
    this.webClient
      .DELETE(`${Endpoints.Addresses}/${addressId}`)
      .then(() => {
        const targetClient = this.clients.find(x => x.id === clientId);
        if (targetClient)
          targetClient.addresses = targetClient.addresses.filter(
            x => x.id !== addressId
          );
      })
      .catch(handleError);
  }

  clearList(): void {
    this.clients = [];
  }
}
</script>

<style lang="scss" scoped>
.menu-card {
  background-color: rgb(212, 212, 212);
}
.addresses {
  background-color: rgb(202, 230, 255);
}
</style>
