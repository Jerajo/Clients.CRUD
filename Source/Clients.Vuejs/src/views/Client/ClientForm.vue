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
            <label for="fullName" class="control-label">Full Name</label>
            <div class="col-md-6">
              <input
                id="fullName"
                v-model="fullName"
                class="form-control"
                placeholder="Full Name"
                required
              />
              <span id="fullNameMessage" class="text-danger">
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
            <label for="userName" class="control-label">User Name</label>
            <div class="col-md-6">
              <input
                id="userName"
                v-model="userName"
                class="form-control"
                placeholder="User Name"
                required
              />
              <span id="userNameMessage" class="text-danger">
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
            <label for="email" class="control-label">Email</label>
            <div class="col-md-6">
              <input
                id="email"
                type="email"
                v-model="email"
                class="form-control"
                placeholder="E-Mail"
                required
              />
              <span id="emailMessage" class="text-danger">
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
            <label for="birthday" class="control-label">Birth Day</label>
            <div class="col-md-6">
              <input
                id="birthday"
                type="date"
                v-model="birthday"
                class="form-control"
                required
              />
              <span id="birthdayMessage" class="text-danger">
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
            <label for="marriageStatus" class="control-label">
              Marriage Status
            </label>
            <div class="col-md-6">
              <select
                v-model="marriageStatus"
                name="marriageStatus"
                id="marriageStatus"
                class="form-control"
              >
                <option value="S">Single</option>
                <option value="M">Married</option>
              </select>
              <span id="marriageStatusMessage" class="text-danger">
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
  client: Client | undefined;
  webClient = new WebClient();
  title = "";
  id = "";

  mounted() {
    this.title = this.$route.query["operation"] + " Client";
    this.id = this.$route.query.id as string;
    if (this.id && typeof this.id == "string") this.fetchClient(this.id);
    else this.client = this.getClient();
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
