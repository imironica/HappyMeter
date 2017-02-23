import { Component } from '@angular/core';

@Component({
    selector: 'my-app',
    template: `<h1>Happy {{name}}</h1>`,
})
export class AppComponent { name = 'Meter'; }