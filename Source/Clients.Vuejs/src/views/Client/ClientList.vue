<template>
  <div class="container">
    <h1 class="my-4">Client List</h1>
    <button
      id="createButton"
      @click="goToCreateClient"
      class="btn btn-success mx-auto mb-4"
    >
      Create new client
    </button>
    <div v-if="hasClients === false" class="text-md-center">
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
                  @click="goToCreateClient(client.id)"
                  class="btn btn-outline-success"
                >
                  <i class="bi bi-pencil-fill"></i>
                </button>
                <button
                  @click="goToCreateClient"
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
import { WebClient, RequestType } from "../../helpers/WebClient";
import { makeId } from "../../helpers";

@Component
export default class ClientList extends Vue {
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
      .fetch(`api/Clients`, RequestType.GET)
      .then(async value => {
        this.clients = (await value.json()) as Client[];
      })
      .catch(reason => {
        console.error(reason);
      });
  }

  getClient(): Client {
    const client = {
      id: makeId(5),
      fullName: "Jesse Jose",
      userName: "jerajo",
      email: "jesse@volatileprogramming.org",
      birthday: new Date(),
      marriageStatus: "S"
    };

    return client;
  }

  goToCreateClient(clientId: string | undefined) {
    this.$router.push({
      path: "/client-create",
      query: { operation: "Create", id: clientId }
    });
  }

  deleteClient(clientId: string) {
    this.webClient
      .fetch(`api/Clients/${clientId}`, RequestType.DELETE)
      .then(async value => {
        this.clients = (await value.json()) as Client[];
      })
      .catch(reason => {
        console.error(reason);
      });
  }

  clearList(): void {
    this.clients = [];
  }
}
</script>

<style lang="scss"></style>
