import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

const apiPrefix = "api/";

@Injectable()
export class UrlPrefixInterceptor implements HttpInterceptor {

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Does not touch request for a domain name.
        if (req.url.includes("://")) {
            return next.handle(req);
        }

        const url = req.url.startsWith('/') ? req.url.substring(1) : req.url;

        // Clone the request to add the prefix.
        const newReq = req.clone({ url: apiPrefix + url });

        return next.handle(newReq);
    }
}