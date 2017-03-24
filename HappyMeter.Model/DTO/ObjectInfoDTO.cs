
namespace HappyMeter.Model
{

    public class ObjectInfoDTO
    {
        public CategoryDTO[] Categories { get; set; }

        public AdultDTO Adult { get; set; }

        public TagDTO[] Tags { get; set; }

        public DescriptionDTO Description { get; set; }

        public string RequestId { get; set; }

        public MetadataDTO Metadata { get; set; }

        public FaceDTO[] Faces { get; set; }

        public ColorDTO Color { get; set; }

        public ImagetypeDTO ImageType { get; set; }
    }

    public class AdultDTO
    {
        public bool IsAdultContent { get; set; }

        public bool IsRacyContent { get; set; }

        public float AdultScore { get; set; }

        public float RacyScore { get; set; }
    }

    public class DescriptionDTO
    {
        public string[] Tags { get; set; }

        public CaptionDTO[] Captions { get; set; }
    }

    public class CaptionDTO
    {
        public string Text { get; set; }

        public float Confidence { get; set; }
    }

    public class MetadataDTO
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string Format { get; set; }
    }

    public class ColorDTO
    {
        public string DominantColorForeground { get; set; }

        public string DominantColorBackground { get; set; }

        public string[] DominantColors { get; set; }

        public string AccentColor { get; set; }

        public bool IsBWImg { get; set; }
    }

    public class ImagetypeDTO
    {
        public int ClipArtType { get; set; }

        public int LineDrawingType { get; set; }
    }

    public class CategoryDTO
    {
        public string Name { get; set; }

        public float Score { get; set; }
        public DetailDTO Detail { get; set; }
    }

    public class DetailDTO
    {
        public object[] Celebrities { get; set; }
    }

    public class TagDTO
    {
        public string Name { get; set; }
        public float Confidence { get; set; }
    }

    public class FaceDTO
    {
        public int Age { get; set; }

        public string Gender { get; set; }

        public FaceRectangle FaceRectangle { get; set; }
    }
}
