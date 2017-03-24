import { ImageEmotion} from './ImageEmotion'
export class Image {
    imageUrl: String;
    id: String;
    category: String;
    labels: String[];
    adultContent: Boolean;
    racyContent: Boolean;
    imageEmotions: ImageEmotion[];
}
