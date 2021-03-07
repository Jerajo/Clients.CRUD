<template>
  <div class="container text-start">
    <h1>{{ title }}</h1>
    <ValidationObserver v-slot="{ handleSubmit }">
      <form action="none" @submit.prevent="handleSubmit(onSubmitForm)">
        <ValidationProvider
          name="County"
          rules="required|alpha_spaces|max:100"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Country</label>
            <div class="col-md-6">
              <input
                type="text"
                v-model="address.country"
                class="form-control"
                placeholder="County Name"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="State"
          rules="required|alpha_spaces|max:100"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">State</label>
            <div class="col-md-6">
              <input
                type="text"
                v-model="address.state"
                class="form-control"
                placeholder="State Name"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="City"
          rules="required|alpha_spaces|max:100"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">City</label>
            <div class="col-md-6">
              <input
                type="text"
                v-model="address.city"
                class="form-control"
                placeholder="City Name"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Postal Code"
          rules="required|digits:5|between:10000,99999"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Postal Code</label>
            <div class="col-md-6">
              <input
                type="number"
                v-model="address.postalCode"
                class="form-control"
                placeholder="Postal Code"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Address Line One"
          rules="required|max:250"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Address Line One</label>
            <div class="col-md-6">
              <input
                type="text"
                v-model="address.addressLineOne"
                class="form-control"
                placeholder="Address Line One"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <ValidationProvider
          name="Address Line Two"
          rules="required|max:250"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Address Line Two</label>
            <div class="col-md-6">
              <input
                type="text"
                v-model="address.addressLineTwo"
                class="form-control"
                placeholder="Address Line Two"
                required
              />
              <span class="text-danger">
                {{ errors[0] }}
              </span>
            </div>
          </div>
        </ValidationProvider>

        <div class="d-flex fle-row">
          <a @click="goToClientList" class="btn btn-outline-secondary my-3">
            {{ cancelButtonText }}
          </a>
          <input
            type="submit"
            class="btn btn-outline-primary my-3 mx-3"
            v-model="saveButtonText"
          />
        </div>
      </form>
    </ValidationObserver>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import { Guid } from "guid-typescript";
import { Address } from "../../models/Address";
import { handleError } from "../../helpers";
import { WebClient, Endpoints } from "../../helpers/WebClient";

@Component
export default class AddressForm extends Vue {
  title: string;
  cancelButtonText: string;
  saveButtonText: string;
  isEditing: boolean;
  address: Address;
  webClient: WebClient;
  addressId: string | undefined;

  constructor() {
    super();

    this.title = "";
    this.cancelButtonText = "Cancel";
    this.saveButtonText = "Register Address";
    this.isEditing = false;

    this.webClient = new WebClient();
    this.address = this.getAddress();
  }

  created() {
    this.title = this.$route.query.operation + " Address";
    const clientId = this.$route.query.clientId as string;

    const addressId = this.$route.query.addressId;

    if (addressId && Guid.isGuid(addressId)) {
      this.isEditing = true;
      this.saveButtonText = "Save Changes";
      this.addressId = addressId as string;
      this.fetchAddress(this.addressId);
    } else {
      this.address.clientId = clientId;
    }
  }

  fetchAddress(addressId: string): void {
    this.webClient
      .GET(`${Endpoints.Addresses}/${addressId}`)
      .then(value => {
        this.address = value.data as Address;
      })
      .catch(handleError);
  }

  getAddress(): Address {
    const address = {
      id: Guid.parse(Guid.EMPTY),
      country: "",
      state: "",
      city: "",
      postalCode: 10000,
      addressLineOne: "",
      addressLineTwo: "",
      clientId: Guid.EMPTY
    };

    return address;
  }

  onSubmitForm() {
    if (this.isEditing) {
      this.webClient
        .PUT(`${Endpoints.Addresses}/${this.addressId}`, this.address)
        .then(() => {
          this.goToClientList();
        })
        .catch(handleError);
    } else {
      this.webClient
        .POST(Endpoints.Addresses, this.address)
        .then(() => {
          this.goToClientList();
        })
        .catch(handleError);
    }
  }

  goToClientList() {
    if (this.isEditing) {
      this.$router.back();
    } else {
      this.$router.push({ path: "/client-list" });
    }
  }
}
</script>
