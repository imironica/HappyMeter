namespace HappyMeter.Models
{
    public class InfoDTO : Entity
    {
        public FaceEmotion[] Emotions { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }
}
