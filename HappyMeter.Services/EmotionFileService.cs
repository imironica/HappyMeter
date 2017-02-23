using HappyMeter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace HappyMeter.Services
{
    class EmotionFileService : IEmotionService
    {

        public void AddEmotion(InfoDTO infoDTO)
        {
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();
            var filename = Path.Combine(saveFolder, infoDTO.Image) + ".txt";
            var emotionStr = JsonConvert.SerializeObject(infoDTO);
            File.WriteAllText(filename, emotionStr);
        }

        public List<InfoDTO> GetEmotionsPerCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
 
}
