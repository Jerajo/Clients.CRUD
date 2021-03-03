<template>
  <div class="container">
    <h1>Hello World</h1>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";

@Component
export default class Alert extends Vue {
  @Prop() private msg!: string;
}
</script>
