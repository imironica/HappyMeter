using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServiceProxy.Models
{



    public class FaceEmotion
    {
        [JsonProperty(PropertyName = "faceRectangle")]
        public FaceRectangle FaceRectangle { get; set; }
        [JsonProperty(PropertyName = "scores")]
        public Score Scores { get; set; }
    }



    [JsonObject(Title = "Facerectangle")]
    public class FaceRectangle
    {
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }
        [JsonProperty(PropertyName = "left")]
        public int Left { get; set; }
        [JsonProperty(PropertyName = "top")]
        public int Top { get; set; }
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }
    }

    [JsonObject(Title = "Scores")]
    public class Score
    {
        [JsonProperty(PropertyName = "anger")]
        public float Anger { get; set; }
        [JsonProperty(PropertyName = "contempt")]
        public float Contempt { get; set; }
        [JsonProperty(PropertyName = "disgust")]
        public float Disgust { get; set; }
        [JsonProperty(PropertyName = "fear")]
        public float Fear { get; set; }
        [JsonProperty(PropertyName = "happiness")]
        public float Happiness { get; set; }
        [JsonProperty(PropertyName = "neutral")]
        public float Neutral { get; set; }
        [JsonProperty(PropertyName = "sadness")]
        public float Sadness { get; set; }
        [JsonProperty(PropertyName = "surprise")]
        public float Surprise { get; set; }

    }




    /*
        public class Rootobject
        {
            public FaceEmotion[] FaceEmotions { get; set; }
        }

        public class FaceEmotion
        {
            [JsonProperty(PropertyName = "faceRectangle")]
            public FaceRectangle FaceRectangle { get; set; }
            [JsonProperty(PropertyName = "scores")]
            public Score[] Scores { get; set; }
        }

        [JsonObject(Title = "Facerectangle")]
        public class FaceRectangle
        {
            [JsonProperty(PropertyName = "height")]
            public int Height { get; set; }
            [JsonProperty(PropertyName = "left")]
            public int Left { get; set; }
            [JsonProperty(PropertyName = "top")]
            public int Top { get; set; }
            [JsonProperty(PropertyName = "width")]
            public int Width { get; set; }
        }

        [JsonObject(Title = "Scores")]
        public class Score
        {
            [JsonProperty(PropertyName = "anger")]
            public float Anger { get; set; }
            [JsonProperty(PropertyName = "contempt")]
            public float Contempt { get; set; }
            [JsonProperty(PropertyName = "disgust")]
            public float Disgust { get; set; }
            [JsonProperty(PropertyName = "fear")]
            public float Fear { get; set; }
            [JsonProperty(PropertyName = "happiness")]
            public float Happiness { get; set; }
            [JsonProperty(PropertyName = "neutral")]
            public float Neutral { get; set; }
            [JsonProperty(PropertyName = "sadness")]
            public float Sadness { get; set; }
            [JsonProperty(PropertyName = "surprise")]
            public float Surprise { get; set; }

        }*/

}
