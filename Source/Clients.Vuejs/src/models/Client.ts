import { Guid } from "guid-typescript";

export type Client = {
  id: Guid;
  fullName: string;
  userName: string;
  email: string;
  birthDay: Date;
  marriageStatus: string;
};
