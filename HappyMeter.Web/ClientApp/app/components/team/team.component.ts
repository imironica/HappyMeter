import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ChartModule } from 'primeng/primeng';

@Component({
    selector: 'team',
    templateUrl: './team.component.html'
})
export class TeamComponent {

    header: string[];
    values: string[][];
    constructor(http: Http) {


    }

}

