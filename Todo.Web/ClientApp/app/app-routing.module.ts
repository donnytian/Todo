import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
    {
        path: 'home',
        loadChildren: "./pages/home/home.module#HomeModule",
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
    },
    {
        path: 'login',
        loadChildren: "./pages/login/login.module#LoginModule"
    },
    {
        path: 'register',
        loadChildren: "./pages/register/register.module#RegisterModule"
    },
    {
        path: '', redirectTo: '/home/dashboard', pathMatch: 'full',
    },
];


@NgModule({
    imports: [
        RouterModule.forRoot(routes,
            {
                preloadingStrategy: PreloadAllModules,
                enableTracing: false, // <-- debugging purposes only
            })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }