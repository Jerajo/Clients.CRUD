<template>
  <div class="container text-start">
    <h1 class="my-4">{{ title }}</h1>
    <ValidationObserver v-slot="{ handleSubmit }">
      <form action="none" @submit.prevent="handleSubmit(onSubmitForm)">
        <ValidationProvider
          name="Full Name"
          rules="required|alpha|max:150"
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
                class="form-control"
                placeholder="User Name"
                required
              />
              <span class="text-danger">
                {{ errors[1] }}
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
                class="form-control"
                placeholder="E-Mail"
                required
              />
              <span class="text-danger">
                {{ errors[2] }}
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
                v-model="client.birthday"
                class="form-control"
                required
              />
              <span class="text-danger">
                {{ errors[3] }}
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
                {{ errors[4] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <input
          type="submit"
          value="Create cliente"
          class="btn btn-outline-primary my-3"
        />
      </form>
    </ValidationObserver>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Client } from "../../models/Client";
import { WebClient, RequestType } from "../../helpers/WebClient";

@Component
export default class ClientForm extends Vue {
  client = this.getClient();
  webClient = new WebClient();
  title = "";
  id = "";

  mounted() {
    this.title = this.$route.query["operation"] + " Client";
    this.id = this.$route.query.id as string;
    if (this.id && typeof this.id == "string") this.fetchClient(this.id);
  }

  fetchClient(id: string) {
    this.webClient
      .fetch(`api/clients/${id}`, RequestType.GET)
      .then(async value => {
        this.client = (await value.json()) as Client;
      })
      .catch(reason => {
        console.error(reason);
      });
  }

  getClient(): Client {
    const client = {
      id: "",
      fullName: "",
      userName: "",
      email: "",
      birthday: new Date(),
      marriageStatus: ""
    };

    return client;
  }

  onSubmitForm() {
    console.log(this.client);
  }
}
</script>
