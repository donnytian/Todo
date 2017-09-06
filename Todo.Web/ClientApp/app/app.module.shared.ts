import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth/auth-layout.component';
import { SidebarModule } from './layouts/sidebar/sidebar.module';
import { FooterModule } from './layouts/footer/footer.module';
import { NavbarModule } from './layouts/navbar/navbar.module';

@NgModule({
    declarations: [
        AppComponent,
        AdminLayoutComponent,
        AuthLayoutComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        AppRoutingModule,
        SidebarModule,
        NavbarModule,
        FooterModule
    ]
})
export class AppModuleShared {
}
