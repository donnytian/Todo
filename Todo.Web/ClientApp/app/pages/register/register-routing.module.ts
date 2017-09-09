import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthLayoutComponent } from '../../layouts/auth/auth-layout.component';

export const registerRoutes: Routes = [
    {
        path: '', component: AuthLayoutComponent
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(registerRoutes)
    ],
    exports: [RouterModule]
})
export class RegisterRoutingModule { }