import { Component, OnInit, AfterViewInit, AfterViewChecked, AfterContentInit, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser, isPlatformServer } from '@angular/common';
import { ROUTES } from './sidebar-routes.config';

declare var $: any;
var sidebarTimer;

@Component({
    selector: 'sidebar-cmp',
    templateUrl: 'sidebar.component.html',
})

export class SidebarComponent implements OnInit{
    public menuItems: any[];
    
    constructor(@Inject(PLATFORM_ID) private platformId: Object) { }
    
    isNotMobileMenu() {
        if (!isPlatformBrowser(this.platformId)) {
            return false;
        }
        
        if($(window).width() > 991){
            return false;
        }
        return true;
    }

    ngOnInit() {
        this.menuItems = ROUTES.filter(menuItem => menuItem);
        
        if (!isPlatformBrowser(this.platformId)) {
            return;
        }
        
        var isWindows = navigator.platform.indexOf('Win') > -1;
        if (isWindows){
           // if we are on windows OS we activate the perfectScrollbar function
            var $sidebar = $('.sidebar-wrapper');
            $sidebar.perfectScrollbar();
           $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar();
           $('html').addClass('perfect-scrollbar-on');
        } else {
           $('html').addClass('perfect-scrollbar-off');
       }
    }

    ngAfterViewInit() {
        if (!isPlatformBrowser(this.platformId)) {
            return;
        }
    }
}

