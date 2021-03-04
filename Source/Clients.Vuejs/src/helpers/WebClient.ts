export enum RequestType {
  GET = "GET",
  POST = "POST",
  PUT = "PUT",
  DELETE = "DELETE"
}

export enum Endpoints {
  Clients = "api/Clients",
  Addresses = "api/Addresses"
}

export class WebClient {
  baseURL = "http://10.0.0.2:8080/";
  headers: Headers;

  constructor() {
    this.headers = new Headers({
      Accept: "*/*",
      "Content-Type": "application/json",
      mode: "no-cors"
    });
  }

  public async GET(uri: string): Promise<Response> {
    const request = new Request(this.baseURL + uri, {
      method: RequestType.GET,
      headers: this.headers
    });

    return fetch(request);
  }

  public async PUT(uri: string, data: string): Promise<Response> {
    const request = new Request(this.baseURL + uri, {
      method: RequestType.PUT,
      headers: this.headers,
      body: data
    });

    return fetch(request);
  }

  public async POST(uri: string, data: string): Promise<Response> {
    const request = new Request(this.baseURL + uri, {
      method: RequestType.POST,
      headers: this.headers,
      body: data
    });

    return fetch(request);
  }

  public async DELETE(uri: string, data = ""): Promise<Response> {
    const request = new Request(this.baseURL + uri, {
      method: RequestType.DELETE,
      headers: this.headers,
      body: data
    });

    return fetch(request);
  }
}
