export enum RequestType {
  POST = "POST",
  GET = "GET",
  UPDATE = "UPDATE",
  DELETE = "DELETE"
}

export class WebClient {
  headers: Headers;

  constructor() {
    this.headers = new Headers({
      Accept: "application/json",
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Methods": "POST,GET,UPDATE,DELETE"
    });
  }

  public async fetch(
    url: string,
    verb: RequestType,
    data: ArrayBuffer | undefined = undefined
  ): Promise<Response> {
    const request = new Request(url, {
      method: verb,
      headers: this.headers,
      body: data
    });

    return fetch(request);
  }
}
