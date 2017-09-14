import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthLayoutComponent } from '../../layouts/auth/auth-layout.component';
import { RegisterComponent } from './register.component';


export const registerRoutes: Routes = [
    {
        path: '', pathMatch: 'full', component: AuthLayoutComponent,
        children: [
            { path: '', pathMatch: 'full', component: RegisterComponent }
        ]
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(registerRoutes)
    ],
    exports: [RouterModule]
})
export class RegisterRoutingModule { }