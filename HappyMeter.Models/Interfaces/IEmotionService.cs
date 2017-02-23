using System.Collections.Generic;
using HappyMeter.Models;

namespace HappyMeter.Services
{
    public interface IEmotionService
    {
        void AddEmotion(InfoDTO infoDTO);
        List<InfoDTO> GetEmotionsPerCategory(string category);
    }
}