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
        public bool EmotionAllreadyComputed(InfoDTO infoDTO)
        {
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();
            var folder = Path.Combine(saveFolder, infoDTO.Category);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var filename = Path.Combine(folder, Path.GetFileName(infoDTO.Image)) + ".txt";

            return File.Exists(filename);
        }
        public void AddEmotion(InfoDTO infoDTO)
        {
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();
            var folder = Path.Combine(saveFolder, infoDTO.Category);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var filename = Path.Combine(folder, Path.GetFileName(infoDTO.Image)) + ".txt";
            var emotionStr = JsonConvert.SerializeObject(infoDTO);
            File.WriteAllText(filename, emotionStr);
        }

        public List<InfoDTO> GetEmotionsPerCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
 
}
