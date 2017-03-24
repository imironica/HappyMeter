using Newtonsoft.Json;

namespace HappyMeter.Model
{
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
 
}
