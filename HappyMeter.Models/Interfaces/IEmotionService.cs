using System.Collections.Generic;
using HappyMeter.Models;

namespace HappyMeter.Services
{
    public interface IEmotionService
    {
        void AddEmotion(InfoDTO infoDTO);
        void AddEmotion(ImageInfoDTO img);
        bool EmotionAllreadyComputed(InfoDTO infoDTO);
        List<InfoDTO> GetEmotionsPerCategory(string category);
        List<string> GetCategories();
    }
}