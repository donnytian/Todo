import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AuthLayoutComponent } from './auth-layout.component';
import { HeaderModule } from '../../components/header/header.module';
import { FooterModule } from '../../components/footer/footer.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        HeaderModule,
        FooterModule
    ],
    declarations: [AuthLayoutComponent]
})
export class AuthLayoutModule {}
