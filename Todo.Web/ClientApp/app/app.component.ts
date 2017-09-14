import { Component, OnInit, ElementRef, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';

declare var $: any;

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(@Inject(PLATFORM_ID) private platformId: Object, private elRef: ElementRef) { }

    ngOnInit() {
        if (!isPlatformBrowser(this.platformId)) {
            return;
        }
        
        $.material.init();
        let body = document.getElementsByTagName('body')[0];
        var isWindows = navigator.platform.indexOf('Win') > -1;
        if (isWindows) {
            // if we are on windows OS we activate the perfectScrollbar function
            body.classList.add("perfect-scrollbar-on");
        } else {
            body.classList.add("perfect-scrollbar-off");
        }

        $.material.init();
        console.log("init in app component");
    }
}