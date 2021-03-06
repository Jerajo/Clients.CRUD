<template>
  <div class="container">
    <h1 class="my-4">Client List</h1>
    <button
      id="createButton"
      @click="goToClientForm"
      class="btn btn-success mx-auto mb-4"
    >
      Create new client
    </button>
    <div v-if="loading">
      <h2>Loading clients...</h2>
    </div>
    <div v-else-if="hasClients === false" class="text-md-center">
      <h2>There are not clients on the list yet.</h2>
    </div>
    <div v-else class="table-responsive">
      <table class="table table-striped">
        <thead>
          <tr class="thead-light">
            <th scope="col">Full Name</th>
            <th scope="col">User Name</th>
            <th scope="col">EMail</th>
            <th scope="col">Birth Day</th>
            <th scope="col">Marriage Status</th>
            <th scope="col">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="client in clients" :key="client.id">
            <td>{{ client.fullName }}</td>
            <td>{{ client.userName }}</td>
            <td>{{ client.email }}</td>
            <td>{{ client.birthDay }}</td>
            <td>{{ client.marriageStatus }}</td>
            <td>
              <div class="flex-column">
                <button
                  @click="goToClientForm(client.id)"
                  class="btn btn-outline-success"
                >
                  <i class="bi bi-pencil-fill"></i>
                </button>
                <button
                  @click="deleteClient(client.id)"
                  class="btn btn-outline-danger ms-2"
                >
                  <i class="bi bi-trash-fill"></i>
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

@Component
export default class ClientList extends Vue {
  loading = true;
  clients: Client[] = [];
  webClient = new WebClient();

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

  goToClientForm(clientId: string | undefined = undefined) {
    const operation = typeof clientId === "string" ? "Edit" : "Create";

    this.$router.push({
      path: "/client-create",
      query: { operation, clientId }
    });
  }

  deleteClient(clientId: string) {
    this.webClient
      .DELETE(`${Endpoints.Clients}/${clientId}`)
      .then(() => {
        this.clients = this.clients.filter(x => x.id.toString() !== clientId);
      })
      .catch(handleError);
  }

  clearList(): void {
    this.clients = [];
  }
}
</script>

<style lang="scss"></style>
