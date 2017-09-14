import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/delay';

import { HttpServiceBase } from './http-service.base';
import { IAuthResult } from '../shared/auth-result.model';
import { TodoModel } from '../pages/home/todo/todo.component';

export const accessTokenName = "access_token";
export const accessTokenExpireName = "access_token_expire";
export const rememberMeName = "remember_me";
export const loginPath = "/login";
export const homePath = "/home/dashboard";

const urlPrefix = "todo/";

@Injectable()
export class TodoService extends HttpServiceBase {
    constructor(protected http: HttpClient) {
        super(http);
    }

    getItems(): Observable<TodoModel[]> {
        return this.http.get<TodoModel[]>(urlPrefix);
    }

    addItem(item: TodoModel): Observable<TodoModel> {
        return this.http.post<TodoModel>(urlPrefix, item);
    }

    isLoggedIn(): boolean {
        const token = localStorage.getItem(accessTokenName);
        const expire = localStorage.getItem(accessTokenExpireName);
        const now = new Date();
        let result = token && expire && new Date(expire) > now;

        return result;
    }

    getAuthorizationHeader(): string {
        return "Bearer " + localStorage.getItem(accessTokenName);
    }

    forgetMe(): void {
        localStorage.removeItem(accessTokenName);
        localStorage.removeItem(accessTokenExpireName);
        localStorage.removeItem(rememberMeName);
    }

    tryRenew(): boolean {
        let result = false;

        this.http.post<IAuthResult>(urlPrefix + "renew", null, { observe: 'response' })
            .subscribe(res => {
                if (res.ok && res.body.expiration && res.body.token) {
                    console.log(res);
                    this.saveToken(res.body);
                    result = true;
                }
            },
            (err: HttpErrorResponse) => {
                console.log(err);
            });

        return result;
    }

    saveToken(authResult: IAuthResult): void {
        localStorage.setItem(accessTokenName, authResult.token);
        localStorage.setItem(accessTokenExpireName, authResult.expiration.toLocaleString());
    }
}