import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './services/interceptors/auth.interceptor';
import { UrlPrefixInterceptor } from './services/interceptors/url-prefix.interceptor';

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        AppRoutingModule,
    ],
    providers: [
        AuthGuard,
        AuthService,
        // Adds Authorization header for every HTTP request.
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
        // Adds url prefix for every HTTP request.
        {
            provide: HTTP_INTERCEPTORS,
            useClass: UrlPrefixInterceptor,
            multi: true,
        },
    ]
})
export class AppModuleShared {
}
