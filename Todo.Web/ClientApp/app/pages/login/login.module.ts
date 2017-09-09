import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { LoginRoutingModule } from './login-routing.module';
import { AuthLayoutModule } from '../../layouts/auth/auth-layout.module';
import { LoginComponent } from './login.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AuthLayoutModule,
        LoginRoutingModule,
    ],
    declarations: [
        LoginComponent
    ]
})
export class LoginModule { }
