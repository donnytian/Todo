import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { AuthService } from '../auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private injector: Injector) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Gets AuthService from injector, why not inject it in constructor? see issue below:
        // https://github.com/angular/angular/issues/18224
        const auth = this.injector.get(AuthService);

        // Get the auth header from the service.
        const authHeader = auth.getAuthorizationHeader();
        // Clone the request to add the new header.
        const authReq = req.clone({ headers: req.headers.set('Authorization', authHeader) });
        // Pass on the cloned request instead of the original request.
        return next.handle(authReq);
    }
}