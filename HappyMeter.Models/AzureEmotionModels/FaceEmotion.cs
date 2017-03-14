using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Models
{
    public class FaceEmotion
    {
        [JsonProperty(PropertyName = "faceRectangle")]
        public FaceRectangle FaceRectangle { get; set; }
        [JsonProperty(PropertyName = "scores")]
        public Score Scores { get; set; }
    }
 
}
