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
    <div v-else class="table-responsive">
      <table class="table table-striped">
        <thead>
          <tr class="thead-light">
            <th scope="col">User Name</th>
            <th scope="col">EMail</th>
            <th scope="col">Birth Day</th>
            <th scope="col">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="client in clients" :key="client.id">
            <td>{{ client.userName }}</td>
            <td>{{ client.email }}</td>
            <td>{{ client.birthDay }}</td>
            <td>
              <div class="column">
                <button
                  @click="goToClientForm(client.id)"
                  class="btn btn-outline-success d-inline"
                >
                  <b-icon icon="pencil-fill"></b-icon>
                </button>
                <button
                  @click="deleteClient(client.id)"
                  class="btn btn-outline-danger ms-2 d-inline"
                >
                  <b-icon icon="trash-fill"></b-icon>
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
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
      path: "/client-create",
      query: { operation, clientId }
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

  clearList(): void {
    this.clients = [];
  }
}
</script>

<style lang="scss"></style>
