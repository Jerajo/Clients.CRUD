import { Guid } from "guid-typescript";

export type Address = {
  id: Guid;
  country: string;
  state: string;
  city: string;
  postalCode: number;
  addressLineOne: string;
  addressLineTwo: string;
  clientId: string;
};
