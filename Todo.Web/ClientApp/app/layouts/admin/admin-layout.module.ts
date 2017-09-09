import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AdminLayoutComponent } from './admin-layout.component';
import { SidebarModule } from '../../components/sidebar/sidebar.module';
import { FooterModule } from '../../components/footer/footer.module';
import { NavbarModule } from '../../components/navbar/navbar.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        SidebarModule,
        FooterModule,
        NavbarModule,
    ],
    declarations: [AdminLayoutComponent],
})
export class AdminLayoutModule {}
