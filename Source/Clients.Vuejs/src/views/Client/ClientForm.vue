/* eslint-disable no-debugger */

<template>
  <div class="container text-start">
    <h1 class="my-4">{{ title }}</h1>
    <ValidationObserver v-slot="{ handleSubmit }">
      <form action="none" @submit.prevent="handleSubmit(onSubmitForm)">
        <ValidationProvider
          name="Full Name"
          rules="required|max:150"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Full Name</label>
            <div class="col-md-6">
              <input
                v-model="client.fullName"
                class="form-control"
                placeholder="Full Name"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="User Name"
          rules="required|max:35"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">User Name</label>
            <div class="col-md-6">
              <input
                v-model="client.userName"
                :disabled="isEditing"
                :class="isEditing ? 'form-control disabled' : 'form-control'"
                placeholder="User Name"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="E-Mail"
          rules="required|email|max:150"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Email</label>
            <div class="col-md-6">
              <input
                type="email"
                v-model="client.email"
                :disabled="isEditing"
                :class="isEditing ? 'form-control disabled' : 'form-control'"
                class="form-control"
                placeholder="E-Mail"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Birthday"
          rules="required"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Birth Day</label>
            <div class="col-md-6">
              <input
                type="date"
                id="datePicker"
                v-model="client.birthDay"
                class="form-control"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Marriage Status"
          rules="required|alpha"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">
              Marriage Status
            </label>
            <div class="col-md-6">
              <select
                v-model="client.marriageStatus"
                name="marriageStatus"
                class="form-control"
              >
                <option value="S">Single</option>
                <option value="M">Married</option>
              </select>
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <div class="d-flex fle-row">
          <a @click="goBack" class="btn btn-outline-secondary my-3">
            Go Back
          </a>
          <input
            type="submit"
            class="btn btn-outline-primary my-3 mx-3"
            v-model="buttonText"
          />
        </div>
      </form>
    </ValidationObserver>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Client } from "../../models/Client";
import { handleError } from "../../helpers";
import { WebClient, Endpoints } from "../../helpers/WebClient";
import { Guid } from "guid-typescript";

@Component
export default class ClientForm extends Vue {
  client = this.getClient();
  webClient = new WebClient();
  title = "";
  buttonText = "Continue";
  clientId: string | undefined;
  isEditing = false;

  mounted() {
    this.title = this.$route.query.operation + " Client";

    const clientId = this.$route.query.clientId;

    if (clientId && typeof clientId === "string") {
      this.clientId = clientId;
      this.buttonText = "Save changes";
    }

    this.isEditing = this.clientId != undefined;

    if (this.clientId) this.fetchClient(this.clientId);
  }

  fetchClient(id: string) {
    this.webClient
      .GET(`${Endpoints.Clients}/${id}`)
      .then(value => {
        this.client = value.data as Client;
        console.log(this.client);
      })
      .catch(handleError);
  }

  getClient(): Client {
    const client = {
      id: Guid.parse(Guid.EMPTY),
      fullName: "",
      userName: "",
      email: "",
      birthDay: new Date(),
      marriageStatus: ""
    };

    return client;
  }

  onSubmitForm() {
    if (this.isEditing) {
      this.webClient
        .PUT(`${Endpoints.Clients}/${this.clientId}`, this.client)
        .then(() => {
          this.continue();
        })
        .catch(handleError);
    } else {
      this.webClient
        .POST(Endpoints.Clients, this.client)
        .then(() => {
          this.continue();
        })
        .catch(handleError);
    }
  }

  goBack() {
    this.$router.back();
  }

  continue() {
    if (this.isEditing) {
      this.$router.back();
    } else {
      const clientId = this.client.id.toString();
      this.$router.push({
        path: "/address-create",
        query: { clientId }
      });
    }
  }
}
</script>

<style scoped>
.disabled {
  background-color: lightgrey;
}
</style>
