using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Models
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
