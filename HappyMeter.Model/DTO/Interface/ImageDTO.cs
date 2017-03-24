using System;
using System.Collections.Generic;
using System.Text;

namespace HappyMeter.Model
{
    public class ImageDTO
    {
        public ImageDTO()
        {
        }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string Id { get; set; }
        public string[] Labels { get; set; }
        public bool AdultContent { get; set; }
        public bool RacyContent { get; set; }
        public ImageEmotionDTO[] ImageEmotions { get; set; }
    }

    public class ImageEmotionDTO
    {
        public string PrimaryEmotion { get; set; }
        public string SecondaryEmotion { get; set; }
        public float PrimaryEmotionPercent { get; set; }
        public float SecondaryEmotionPercent { get; set; }
    }
}
