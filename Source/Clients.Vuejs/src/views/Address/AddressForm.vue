<template>
  <div class="container text-start">
    <h1>{{ title }}</h1>
    <ValidationObserver v-slot="{ handleSubmit }">
      <form action="none" @submit.prevent="handleSubmit(onSubmitForm)">
        <ValidationProvider
          name="County"
          rules="required|max:150"
          v-slot="{ errors }"
        >
          <div class="form-group mb-3">
            <label class="control-label">Country</label>
            <div class="col-md-6">
              <input
                type="text"
                v-model="address.county"
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
          rules="required|max:35"
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
          rules="required|email|max:150"
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
          rules="required|max(250)"
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
          rules="required|max(250)"
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
          <a @click="cancel" class="btn btn-outline-secondary my-3">
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
  address = this.getAddress();
  title = "Create Address";
  cancelButtonText = "Cancel";
  saveButtonText = "Add Address";
  webClient = new WebClient();
  isEditing = false;

  mounted() {
    const clientId = this.$route.query.clientId as string;
    const addressId = this.$route.query.addressId as string;

    if (addressId) {
      this.isEditing = true;
      this.title = "Edit Address";
      this.saveButtonText = "Save Changes";
      this.fetchAddress(addressId);
    } else {
      this.address.clientId = Guid.parse(clientId);
    }
  }

  fetchAddress(addressId: string): void {
    this.webClient
      .GET(`${Endpoints.Addresses}/${addressId}`)
      .then(value => {
        this.address = value.data as Address;
        console.log(this.address);
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
      clientId: Guid.parse(Guid.EMPTY)
    };

    return address;
  }

  onSubmitForm() {
    console.log("form submitted", this.address);
  }

  cancel() {
    if (this.isEditing) {
      this.$router.back();
    } else {
      this.$router.push({ path: "/client-list" });
    }
  }
}
</script>
