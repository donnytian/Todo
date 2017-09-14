import { Component, OnInit, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

import { AuthService, homePath } from '../../services/auth.service';
import { LoginModel } from '../../shared/login.model';

declare var $: any;
export const generalError = "Sorry you cannot register!";

export class RegisterModel {
    userName: string;
    password: string;
    confirmPassword: string;
    message: string;
}

@Component({
    selector: 'register-cmp',
    templateUrl: './register.component.html',
    styleUrls: ['register.component.css']
})

export class RegisterComponent implements OnInit, AfterViewInit {
    constructor(private auth: AuthService, private router: Router) {
        this.model = new RegisterModel();
    }

    model: RegisterModel;

    ngOnInit() {

    }

    ngAfterViewInit() {
        // Seems there is no hook to trigger when all child views initialized, so we do materila.init in each page.
        $.material.init();
    }

    onSubmit() {
        this.model.message = null;

        this.auth.register(this.model).subscribe(
            data => {
                const login = new LoginModel();
                login.userName = this.model.userName;
                login.password = this.model.password;

                this.auth.login(login).subscribe(
                    res => {
                        this.auth.saveToken(res);
                        this.router.navigate([homePath]);
                    },
                    (err: HttpErrorResponse) => {
                        console.log(err);
                        this.model.message = err.message;
                    });
            },
            (err: HttpErrorResponse) => {
                console.log(err);
                let msg = generalError;
                if (Array.isArray(err.error)) {
                    if (err.error[0].hasOwnProperty("description")) {
                        msg = err.error[0].description;
                    }
                } else {
                    let array = Object.getOwnPropertyNames(err.error);
                    msg = err.error[array[0]];
                }
                this.model.message = msg;
            });
    }
}
