using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeter.Model
{
    public class ImageInfoDTO: Entity
    {
        public ImageInfoDTO()
        {
            Objects = new ObjectInfoDTO();
        }
        public FaceEmotionDTO[] Emotions { get; set; }
        public FaceInfoDTO[] Faces { get; set; }
        public ObjectInfoDTO Objects { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string Id { get; set; }
        public float Anger { get; set; }
        public float Contempt { get; set; }
        public float Disgust { get; set; }
        public float Fear { get; set; }
        public float Happiness { get; set; }
        public float Neutral { get; set; }
        public float Sadness { get; set; }
        public float Surprise { get; set; }
    }
}
