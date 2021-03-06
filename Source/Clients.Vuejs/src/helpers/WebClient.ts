import axios, { AxiosInstance, AxiosResponse } from "axios";

export enum Endpoints {
  Clients = "Clients",
  Addresses = "Addresses"
}

export class WebClient {
  instance: AxiosInstance;

  constructor() {
    this.instance = axios.create({
      baseURL: 'http://10.0.0.2:8080/api/',
      timeout: 30000,
      headers: {
        "Access-Control-Allow-Origin": "*",
        accept: "*/*",
        "Content-Type": "application/json",
        mode: "no-cors"
      }
    });
  }

  public async GET(uri: string): Promise<AxiosResponse<any>> {
    return this.instance.get(uri);
  }

  public async PUT(uri: string, data: object): Promise<AxiosResponse<any>> {
    return this.instance.put(uri, data);
  }

  public async POST(uri: string, data: object): Promise<AxiosResponse<any>> {
    return this.instance.post(uri, data);
  }

  public async DELETE(uri: string): Promise<AxiosResponse<any>> {
    return this.instance.delete(uri);
  }
}
