import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ChartModule } from 'primeng/primeng';

@Component({
    selector: 'mood-chart',
    templateUrl: './moodchart.component.html'
})
export class MoodChartComponent {

    header: string[];
    values: string[][];
    constructor(http: Http) {

        
        this.header = ['Dates','Ionut','Diana','Gabi'];
        this.values = [
                        ['18-03-2017', 'happy', 'happy', 'happy'],
                        ['19-03-2017', 'happy', 'happy', 'happy'],
                        ['20-12-2013', 'sad', 'happy', 'happy'],
                        ['21-03-2017', 'happy', 'happy', 'happy'],
                        ['22-12-2013', 'sad', 'happy', 'happy'],
                        ['23-12-2013', 'sad', 'happy', 'happy'],
                        ['24-12-2013', 'normal', 'happy', 'happy'],
                        ['25-12-2013', 'normal', 'happy', 'happy'],
                        ['26-12-2013', 'normal', 'happy', 'happy']
        ];


    }

}

