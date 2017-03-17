import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { SidebarMenuComponent } from './components/sidebarmenu/sidebarmenu.component';
import { PrimeComponent } from './components/prime/prime.component';
import { ButtonModule, GrowlModule } from 'primeng/primeng';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        PrimeComponent,
        NavbarComponent,
        SidebarMenuComponent,
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'prime', component: PrimeComponent },
            { path: '**', redirectTo: 'home' }
        ]),
        FormsModule,
        ButtonModule,
        GrowlModule
    ]
})
export class AppModule {
}
