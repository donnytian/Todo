import { HttpClient } from '@angular/common/http';

export abstract class HttpServiceBase {
    constructor(protected http: HttpClient) { }
}