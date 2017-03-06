using HappyMeter.Models;

namespace CognitiveServiceProxy
{
    public interface IMLService
    {
        FaceEmotionDTO[] GetEmotionsFromImage(byte[] imgdata);
        FaceEmotionDTO[] GetEmotionsFromLink(string imgLink);
        FaceInfoDTO[] GetFacesFromImage(byte[] imgdata);

        ObjectInfoDTO GetObjectsFromImage(byte[] imgdata);
    }
}