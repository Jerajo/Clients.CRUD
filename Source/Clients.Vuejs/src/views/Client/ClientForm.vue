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
            <label for="birthDay" class="control-label">Birth Day</label>
            <div class="col-md-6">
              <b-form-datepicker
                id="birthDay"
                :min="minDate"
                :max="maxDate"
                :state="isDateValid"
                :show-decade-nav="true"
                locale="en"
                v-model="client.birthDay"
              ></b-form-datepicker>
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
  title: string;
  buttonText: string;
  isEditing: boolean;
  minDate: Date;
  maxDate: Date;
  client: Client;
  webClient: WebClient;
  clientId: string | undefined;

  constructor() {
    super();

    this.title = "";
    this.buttonText = "Continue";
    this.isEditing = false;
    this.minDate = new Date("01/01/1900");
    this.maxDate = new Date();

    this.webClient = new WebClient();
    this.client = this.getClient();
  }

  get isDateValid(): boolean | null {
    const isValid = this.client.birthDay != null;
    return isValid ? null : isValid;
  }

  created() {
    this.title = this.$route.query.operation + " Client";

    const clientId = this.$route.query.clientId;

    if (clientId && Guid.isGuid(clientId)) {
      this.isEditing = true;
      this.buttonText = "Save changes";
      this.clientId = clientId as string;
      this.fetchClient(this.clientId);
    }
  }

  fetchClient(id: string) {
    this.webClient
      .GET(`${Endpoints.Clients}/${id}`)
      .then(value => {
        this.client = value.data as Client;
      })
      .catch(handleError);
  }

  getClient(): Client {
    const client: Client = {
      id: Guid.parse(Guid.EMPTY),
      fullName: "",
      userName: "",
      email: "",
      birthDay: null,
      marriageStatus: "",
      addresses: []
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
        .then(value => {
          const client = value.data as Client;
          this.continue(client.id);
        })
        .catch(handleError);
    }
  }

  goBack() {
    this.$router.back();
  }

  continue(id: Guid | null = null) {
    if (id) {
      const clientId = id.toString();
      this.$router.push({
        path: "/address-form",
        query: { clientId }
      });
    } else {
      this.$router.back();
    }
  }
}
</script>

<style>
.disabled {
  background-color: lightgrey;
}
.row {
  margin: 0px;
}
</style>
