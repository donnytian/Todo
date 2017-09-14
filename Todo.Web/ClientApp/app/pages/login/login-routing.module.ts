import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthLayoutComponent } from '../../layouts/auth/auth-layout.component';
import { LoginComponent } from './login.component';

export const loginRoutes: Routes = [
    {
        path: '', pathMatch: 'full', component: AuthLayoutComponent,
        children: [
            { path: '', pathMatch: 'full', component: LoginComponent }
        ]
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(loginRoutes)
    ],
    exports: [RouterModule]
})
export class LoginRoutingModule { }