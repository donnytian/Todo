import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AdminLayoutComponent } from '../../layouts/admin/admin-layout.component';
import { AuthGuard } from '../../guards/auth.guard';

export const homeRoutes: Routes = [
    {
        path: '', component: AdminLayoutComponent,
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        children: [
            {
                path: '', redirectTo: 'dashboard'
            },
            {
                path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule'
            },
            //{
            //  path: 'forms', loadChildren: './forms/forms.module#Forms'
            //},
            //{
            //  path: 'tables', loadChildren: './tables/tables.module#TablesModule'
            //},
            //{
            //  path: 'maps', loadChildren: './maps/maps.module#MapsModule'
            //},
            //{
            //  path: 'widgets', loadChildren: './widgets/widgets.module#WidgetsModule'
            //},
            //{
            //  path: 'charts', loadChildren: './charts/charts.module#ChartsModule'
            //},
            //{
            //  path: 'calendar', loadChildren: './calendar/calendar.module#CalendarModule'
            //},
            //{
            //  path: '', loadChildren: './userpage/user.module#UserModule'
            //},
            //{
            //  path: '', loadChildren: './timeline/timeline.module#TimelineModule'
            //}
        ]
    },
    //{
    //  path: '', component: AuthLayoutComponent,
    //  children: [{path: 'pages', loadChildren: '../pages/pages.module#PagesModule'}]
    //}
];


@NgModule({
    imports: [
        RouterModule.forChild(homeRoutes)
    ],
    exports: [RouterModule],
})
export class HomeRoutingModule { }