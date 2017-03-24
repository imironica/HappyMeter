using HappyMeter.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace HappyMeter.Services
{
    public class EmotionFileService : IEmotionService
    {
        public EmotionFileService()
        {

        }

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
        public void AddEmotion(ImageInfoDTO infoDTO)
        {
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();
            var folder = Path.Combine(saveFolder, infoDTO.Category);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var filename = Path.Combine(folder, Path.GetFileName(infoDTO.ImageUrl)) + ".txt";
            var emotionStr = JsonConvert.SerializeObject(infoDTO);
            File.WriteAllText(filename, emotionStr);
        }
        public List<InfoDTO> GetEmotionsPerCategory(string category)
        {
            throw new NotImplementedException();
        }

        public List<string> GetCategories()
        {
            string[] categories = new string[] { "Halloween BHD" };
            return categories.ToList();
        }
    }
 
}
