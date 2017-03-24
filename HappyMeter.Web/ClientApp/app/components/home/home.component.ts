import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ChartModule } from 'primeng/primeng';
import { ImageGridRequest } from './dto/ImageGridRequest'
import { ImageRequest } from './dto/ImageRequest';
import { ImageGrid } from './dto/ImageGrid';
import { Image } from './dto/Image'
import { ImageEmotion } from './dto/ImageEmotion'
import { CategoryGrid } from './dto/CategoryGrid'

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent {
    lastImages: ImageGrid[];
    hapyiestImages: ImageGrid[];
    mostSadImages: ImageGrid[];
    currentImage: Image;
    http: Http;
    events: CategoryGrid[];
    constructor(http: Http) {
        this.http = http;

        this.http.get('/api/Image/GetImageCategories').subscribe(result => {
            this.events = result.json();
            var firstEvent = this.events[0];
            this.showImages(firstEvent.category, http);
        });
    }

    viewImage(category: String, id: String) {
        var request = new ImageRequest();
        request.category = category;
        request.id = id;
        this.http.post('/api/Image/GetImage', request)
            .subscribe(result => {
                this.currentImage = result.json();
            });
    }

    showImagesInterface(category: String)
    {
        this.showImages(category, this.http);
    }

    showImages(category: String, http: Http)
    {
        var link = '/api/Image/GetImagesPerCategory/';
        var request = new ImageGridRequest();
        request.category = category;
        request.option = '1';

        http.post(link, request).subscribe(result => {
            this.lastImages = result.json();
        });

        request.option = '2';
        http.post(link, request).subscribe(result => {
            this.hapyiestImages = result.json();
        });

        request.option = '3';
        http.post(link, request).subscribe(result => {
            this.mostSadImages = result.json();
        });
    }
} 
