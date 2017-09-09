import { Component, OnInit, AfterViewInit } from '@angular/core';

declare var $: any;

@Component({
    selector: 'login-cmp',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit, AfterViewInit {
    test: Date = new Date();

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
}
