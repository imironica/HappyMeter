import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './components/app/app.component'
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { EventChartComponent } from './components/eventchart/eventchart.component';
import { MoodChartComponent } from './components/moodchart/moodchart.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarMenuComponent } from './components/sidebarmenu/sidebarmenu.component';
import { TeamComponent } from './components/team/team.component';
import { ImageAnalizerComponent } from './components/imageanalizer/imageanalizer.component';
import { ButtonModule, GrowlModule } from 'primeng/primeng';
import { ChartModule } from 'primeng/primeng';


@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        EventChartComponent,
        FetchDataComponent,
        HomeComponent,
        NavbarComponent,
        SidebarMenuComponent,
        MoodChartComponent,
        TeamComponent,
        ImageAnalizerComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'event-chart', component: EventChartComponent },
            { path: 'mood-chart', component: MoodChartComponent },
            { path: 'team', component: TeamComponent },
            { path: 'imageanalizer', component: ImageAnalizerComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        FormsModule,
        ButtonModule,
        GrowlModule,
        ChartModule
    ]
})
export class AppModule {
}
