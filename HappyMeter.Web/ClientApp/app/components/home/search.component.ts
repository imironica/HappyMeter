import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ChartModule } from 'primeng/primeng';
import { ImageGridRequest } from './dto/ImageGridRequest'
import { ImageRequest } from './dto/ImageRequest';
import { ImageGrid } from './dto/ImageGrid';
import { Image } from './dto/Image'
import { ImageEmotion } from './dto/ImageEmotion'
import { CategoryGrid } from './dto/CategoryGrid'
import { QuerySearchRequest } from './dto/QuerySearchRequest'
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UniversalModule } from 'angular2-universal';
@Component({
    selector: 'search',
    templateUrl: './search.component.html'
})

export class SearchComponent {
    listImages: ImageGrid[];
    currentImage: Image;
    age: number;
    glasses: string;
    smile: string;
    gender: string;
    http: Http;
    events: CategoryGrid[];
    constructor(http: Http) {
        this.http = http;

         
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

    searchImages()
    {
        var request = new QuerySearchRequest();
        request.age = this.age;
        request.glasses = this.glasses;
        request.smile = this.smile;
        request.gender = this.gender;
 
        this.http.post('/api/Image/SearchImage', request)
            .subscribe(result => {
                this.listImages = result.json();
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
            this.listImages = result.json();
        });
    }
} 
