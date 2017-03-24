using System;

namespace HappyMeter.Model
{
    public class InfoDTO : Entity
    {
        public FaceEmotionDTO[] Emotions { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public Guid Id { get; set; }
    }
}
