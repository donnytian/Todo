import { Routes } from '@angular/router';

import { DashboardComponent } from './dashboard.component';

export const dashboardRoutes: Routes = [
    {
        path: '', pathMatch: 'full', component: DashboardComponent
    }
];
