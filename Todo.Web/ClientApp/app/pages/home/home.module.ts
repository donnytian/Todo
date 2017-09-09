import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { HomeRoutingModule } from "./home-routing.module";
import { AdminLayoutModule } from '../../layouts/admin/admin-layout.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        AdminLayoutModule,
        HomeRoutingModule,
    ],
})
export class HomeModule { }
