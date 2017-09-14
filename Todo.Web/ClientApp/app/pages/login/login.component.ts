import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { AuthService, homePath } from '../../services/auth.service';
import { LoginModel } from '../../shared/login.model';

declare var $: any;
export const generalError = "Incorrect user name or password!";

@Component({
    selector: 'login-cmp',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit, AfterViewInit {
    constructor(private auth: AuthService, private router: Router) {
        this.model = new LoginModel();
    }

    returnUrl: string;
    model: LoginModel;
    

    ngOnInit() {
        setTimeout(function () {
            // after 1000 ms we add the class animated to the login/register card
            $('.card').removeClass('card-hidden');
        },
            700);
    }

    ngAfterViewInit() {
        // Seems there is no hook to trigger when all child views initialized, so we do materila.init in each page.
        $.material.init();
    }

    onSubmit() {
        this.model.message = null;

        this.auth.login(this.model).subscribe(
            data => {
                console.log(data);
                this.auth.saveToken(data);
                const url = this.auth.redirectUrl ? this.auth.redirectUrl : homePath;
                this.auth.redirectUrl = null;
                this.router.navigate([url]);
            },
            (err: HttpErrorResponse) => {
                console.log(err);
                this.model.message = generalError;
            });
    }
}
