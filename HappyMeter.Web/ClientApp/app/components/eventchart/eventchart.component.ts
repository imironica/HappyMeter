import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ChartModule } from 'primeng/primeng';

@Component({
    selector: 'event-chart',
    templateUrl: './eventchart.component.html'
})
export class EventChartComponent {
    data: any;
    http: Http
    options: any;
    events: CategoryGrid[];
    constructor(http: Http) {
        this.http = http;

        this.http.get('/api/Image/GetCategoriesChart').subscribe(result => {
            this.events = result.json();
            var firstEvent = this.events[0];

            var labelsValues = [];
            var angerValues = [];
            var happinessValues = [];

            var fearValues = [];
            var disgustValues = [];
            var neutralValues = [];
            var sadnessValues = [];
            var surprizeValues = [];
            var contemptValues = [];
            for (let event of this.events) {
                labelsValues.push(event.category);
                angerValues.push(event.angerPercent);
                happinessValues.push(event.happinessPercent);
                fearValues.push(event.fearPercent);
                disgustValues.push(event.disgustPercent);
                neutralValues.push(event.neutralPercent);
                sadnessValues.push(event.sadnessPercent);
                contemptValues.push(event.contemptPercent);
            }

            this.data = {
                labels: labelsValues,
                datasets: [
                    {
                        label: 'Anger',
                        backgroundColor: '#42A5F5',
                        borderColor: '#1E88E5',
                        data: angerValues
                    },{
                        label: 'Contempt',
                        backgroundColor: '#9CCC65',
                        borderColor: '#7CB342',
                        data: contemptValues
                    },{
                        label: 'Disgust',
                        backgroundColor: '#FF99E6',
                        borderColor: '#7CB342',
                        data: disgustValues
                    },{
                        label: 'Fear',
                        backgroundColor: '#66C2FF',
                        borderColor: '#7CB342',
                        data: fearValues
                    },{
                        label: 'Happiness',
                        backgroundColor: '#FF5050',
                        borderColor: '#7CB342',
                        data: happinessValues
                    },{
                        label: 'Neutral',
                        backgroundColor: '#DDDDDD',
                        borderColor: '#7CB342',
                        data: neutralValues
                    }, {
                        label: 'Sadness',
                        backgroundColor: ' #8585AD',
                        borderColor: '#7CB342',
                        data: sadnessValues
                    }, {
                        label: 'Surprise',
                        backgroundColor: '#FF9966',
                        borderColor: '#7CB342',
                        data: surprizeValues
                    }
                ]
            };
        });
    }

}

export class Event
{
    name: string;
    year: number;
}
export class CategoryGrid {
    category: String;
    happinessPercent: Number;
    angerPercent: Number;
    fearPercent: Number;
    disgustPercent: Number;
    neutralPercent: Number;
    sadnessPercent: Number;
    contemptPercent: Number;
    surprizePercent: Number;
}

