import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    images: ImageValue[];

    constructor(http: Http) {
        this.images = [{ src: "img/1.jpg", mood: "happy people" }, { src: "img/2.jpg", mood: "happy people" }, { src: "img/3.jpg", mood: "happy people" }, { src: "img/4.jpg", mood: "happy people" },
                       { src: "img/1.jpg", mood: "happy people" }, { src: "img/2.jpg", mood: "happy people" }, { src: "img/3.jpg", mood: "happy people" }, { src: "img/4.jpg", mood: "happy people" }];
    }
}

export class ImageValue
{
    src: String;
    mood: String
}
