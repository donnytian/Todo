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

    addOrUpdate(item: TodoModel): Observable<TodoModel> {
        return this.http.post<TodoModel>(urlPrefix, item);
    }

    delete(id: string): Observable<string> {
        return this.http.delete<string>(urlPrefix + id);
    }
}