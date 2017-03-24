using Newtonsoft.Json;

namespace HappyMeter.Model
{
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
 
}
