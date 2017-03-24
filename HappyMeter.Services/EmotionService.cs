using HappyMeter.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace HappyMeter.Services
{
    public class EmotionService : IEmotionService
    {
        public void AddEmotion(InfoDTO infoDTO)
        {
            var repo = new MDRepository<InfoDTO>();
            repo.InsertOne(infoDTO);
        }

        public List<InfoDTO> GetEmotionsPerCategory(string category)
        {
            var repo = new MDRepository<InfoDTO>();
            var values = repo.GetAllList();
            return values;
        }

        public bool EmotionAllreadyComputed(InfoDTO infoDTO) 
        {
            throw new NotImplementedException();
        }

        public void AddEmotion(ImageInfoDTO infoDTO)
        {
            var repo = new MDRepository<ImageInfoDTO>();
            repo.InsertOne(infoDTO);
        }
        public List<string> GetCategories()
        {
            string[] categories = new string[] { "Halloween BHD" };
            return categories.ToList();
        }
    }
}
