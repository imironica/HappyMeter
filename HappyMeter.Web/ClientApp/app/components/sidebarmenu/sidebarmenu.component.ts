import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'sidebar-menu',
    template: require('./sidebarmenu.component.html')
})
export class SidebarMenuComponent {

    constructor(http: Http) {
       
    }
}

