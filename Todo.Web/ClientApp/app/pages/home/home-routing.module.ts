import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminLayoutComponent } from '../../layouts/admin/admin-layout.component';

export const homeRoutes: Routes = [
    {
        path: 'dashboard', component: AdminLayoutComponent,
        children: [{ path: '', pathMatch: 'full', loadChildren: './dashboard/dashboard.module#DashboardModule' }]
    },
    {
        path: 'todo', component: AdminLayoutComponent,
        children: [{ path: '', pathMatch: 'full', loadChildren: './todo/todo.module#TodoModule' }]
    },
    //{
    //  path: 'xxx', component: AdminLayoutComponent,
    //  children: [{path: '', pathMatch; 'full', loadChildren: '../pages/pages.module#PagesModule'}]
    //}
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
];


@NgModule({
    imports: [
        RouterModule.forChild(homeRoutes)
    ],
    exports: [RouterModule],
})
export class HomeRoutingModule { }