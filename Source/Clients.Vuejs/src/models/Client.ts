import { Guid } from "guid-typescript";
import { Address } from "./Address";

export type Client = {
  id: Guid;
  fullName: string;
  userName: string;
  email: string;
  birthDay: Date | null;
  marriageStatus: string;
  addresses: Address[];
};
