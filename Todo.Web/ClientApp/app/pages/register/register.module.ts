import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RegisterRoutingModule } from './register-routing.module';
import { AuthLayoutModule } from '../../layouts/auth/auth-layout.module';
import { RegisterComponent } from "./register.component";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AuthLayoutModule,
        RegisterRoutingModule
    ],
    declarations: [RegisterComponent]
})
export class RegisterModule {}
