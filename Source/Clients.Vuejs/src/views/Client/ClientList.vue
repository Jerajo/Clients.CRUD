<template>
  <div class="container">
    <h1 class="my-4">Client List</h1>
    <button
      @click="goToCreateClient"
      id="createButton"
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
            <th scope="col">#</th>
            <th scope="col">Full Name</th>
            <th scope="col">User Name</th>
            <th scope="col">EMail</th>
            <th scope="col">Birth Day</th>
            <th scope="col">Marriage Status</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="client in clients" :key="client.id">
            <th scope="row">{{ client.id }}</th>
            <td>{{ client.fullName }}</td>
            <td>{{ client.userName }}</td>
            <td>{{ client.email }}</td>
            <td>{{ client.birthday }}</td>
            <td>{{ client.marriageStatus }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Client } from "../../models/Client";
import { makeId } from "../../helpers";

@Component
export default class ClientList extends Vue {
  clients: Client[] = [];

  get hasClients() {
    const result = this.clients.length > 0;

    return result;
  }

  mounted(): void {
    this.getClients();
  }

  getClients(): void {
    this.clients.push(this.getClient());
    this.clients.push(this.getClient());
    this.clients.push(this.getClient());
    this.clients.push(this.getClient());
    this.clients.push(this.getClient());
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

  goToCreateClient() {
    this.$router.push({
      path: "/client-create",
      query: { operation: "Create" }
    });
  }

  clearList(): void {
    this.clients = [];
  }
}
</script>
