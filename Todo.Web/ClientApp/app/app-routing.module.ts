import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
    {
        path: '', redirectTo: 'home', pathMatch: 'full',
    },
    {
        path: 'home',
        loadChildren: "./pages/home/home.module#HomeModule",
        canActivate: [AuthGuard]
    },
    {
        path: 'login',
        loadChildren: "./pages/login/login.module#LoginModule"
    },
    {
        path: 'register',
        loadChildren: "./pages/register/register.module#RegisterModule"
    },
];


@NgModule({
    imports: [
        RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }