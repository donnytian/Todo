import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MdModule } from '../../../components/md/md.module';

import { DashboardComponent } from './dashboard.component';
import { dashboardRoutes } from './dashboard.routing';

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(dashboardRoutes),
        FormsModule,
        MdModule
    ],
    declarations: [DashboardComponent]
})

export class DashboardModule {}
