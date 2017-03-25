using HappyMeter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace HappyMeter.Service
{
    public class EmotionService : IEmotionService
    {
        public EmotionService()
        {

        }


        private static readonly ConnectionPolicy ConnectionPolicy = new ConnectionPolicy
        {
            ConnectionMode = ConnectionMode.Direct,
            ConnectionProtocol = Protocol.Tcp,
            RequestTimeout = new TimeSpan(1, 0, 0),
            MaxConnectionLimit = 1000,
            RetryOptions = new RetryOptions
            {
                MaxRetryAttemptsOnThrottledRequests = 10,
                MaxRetryWaitTimeInSeconds = 60
            }
        };

        public async Task<List<ImageGridDTO>> GetEmotionsPerCategory(string option, string category)
        {
            var order = string.Empty;

            if (option == "2")
                order = " ORDER BY c.Happiness desc";
            if (option == "3")
                order = " ORDER BY c.Sadness desc";

            var _repository = new Repository<ImageInfoDTO>();
            var sql = string.Format("SELECT c.ImageUrl, c.Category, c.Id FROM c where c.Category = '{0}' {1}", category, order);
            var lstImages = await _repository.FindMultiple(sql, "faces-happy-meter");
            return lstImages.Select(x => new ImageGridDTO { Category = x.Category, ImageUrl = x.ImageUrl, Id = x.Id }).ToList();
        }

        public async Task<List<ImageGridDTO>> SearchImages(QuerySearchRequest options)
        {
            var _repository = new Repository<ImageInfoDTO>();
            var sql = string.Format("SELECT * FROM c ");
            var lstImages = await _repository.FindMultiple(sql, "faces-happy-meter");

            var lstReturnedImages = new List<ImageGridDTO>();
            int count = 0;
            foreach (var image in lstImages)
            {
                var valid = true;
                var faceAttributes = image.Faces.Select(x => x.FaceAttributes);

                if (options.Age > 0)
                {
                    if (!faceAttributes.Any(x => x.Age >= options.Age - 1 && x.Age <= options.Age + 1))
                        valid = false;
                }

                if (!faceAttributes.Any(x => x.Gender.Equals(options.Gender,StringComparison.CurrentCultureIgnoreCase)))
                    valid = false;
                if (!faceAttributes.Any(x => x.Glasses.Equals(options.Glasses, StringComparison.CurrentCultureIgnoreCase)))
                    valid = false;

                if (options.Smile.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
                {
                    if(!faceAttributes.Any(x => x.Smile > 0.1))
                    valid = false;
                }

                if (valid)
                {
                    lstReturnedImages.Add(new ImageGridDTO
                    {
                        Category = image.Category,
                        ImageUrl = image.ImageUrl,
                        Id = image.Id
                    });
                    count++;
                }
                if (count > 50)
                    return lstReturnedImages;
            }
            return lstReturnedImages;
        }

        public async Task<ImageDTO> GetImage(string category, string id)
        {
            var _repository = new Repository<ImageInfoDTO>();
            var sql = string.Format("SELECT * FROM c where c.Category = '{0}' AND c.ImageUrl = '{1}' ", category, id);
            var img = await _repository.FindOne(sql, "faces-happy-meter");
            var imageDTO = new ImageDTO
            {
                Category = img.Category,
                ImageUrl = img.ImageUrl,
                Id = img.Id,
                Labels = img.Objects.Description.Tags,
                AdultContent = img.Objects.Adult.IsAdultContent,
                RacyContent = img.Objects.Adult.IsRacyContent,

            };
            var lstEmotions = new List<ImageEmotionDTO>();
            var lstFaceRectangle = new List<FaceRectangleDTO>();
            foreach (var emotion in img.Emotions)
            {
                ImageEmotionDTO emotionDTO = new ImageEmotionDTO();

                double[] emotionScores = { emotion.Scores.Anger,
                                    emotion.Scores.Contempt,
                                    emotion.Scores.Disgust,
                                    emotion.Scores.Fear,
                                    emotion.Scores.Happiness,
                                    emotion.Scores.Sadness,
                                    emotion.Scores.Surprise };
                var maxEmotionValue = emotionScores.Max();

                if (maxEmotionValue == emotion.Scores.Anger)
                {
                    emotionDTO.PrimaryEmotion = "Anger";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Anger;
                }
                if (maxEmotionValue == emotion.Scores.Contempt)
                {
                    emotionDTO.PrimaryEmotion = "Contempt";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Contempt;
                }
                if (maxEmotionValue == emotion.Scores.Disgust)
                {
                    emotionDTO.PrimaryEmotion = "Disgust";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Disgust;
                }
                if (maxEmotionValue == emotion.Scores.Fear)
                {
                    emotionDTO.PrimaryEmotion = "Fear";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Fear;
                }
                if (maxEmotionValue == emotion.Scores.Happiness)
                {
                    emotionDTO.PrimaryEmotion = "Happiness";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Happiness;
                }
                if (maxEmotionValue == emotion.Scores.Neutral)
                {
                    emotionDTO.PrimaryEmotion = "Neutral";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Neutral;
                }
                if (maxEmotionValue == emotion.Scores.Surprise)
                {
                    emotionDTO.PrimaryEmotion = "Surprise";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Surprise;
                }
                if (maxEmotionValue == emotion.Scores.Sadness)
                {
                    emotionDTO.PrimaryEmotion = "Sadness";
                    emotionDTO.PrimaryEmotionPercent = emotion.Scores.Sadness;
                }

                lstEmotions.Add(emotionDTO);
                lstFaceRectangle.Add(emotion.FaceRectangle);
            }
            imageDTO.ImageEmotions = lstEmotions.ToArray();
            imageDTO.FaceRectangles = lstFaceRectangle.ToArray();

            return imageDTO;
        }


        public async Task<List<CategoryGridDTO>> GetCategories()
        {
            var order = string.Empty;
            var _repository = new Repository<ImageInfoDTO>();
            var sql = string.Format("SELECT c.Category, c.Happiness FROM c");
            var lstCategories = await _repository.FindMultipleDynamic(sql, "faces-happy-meter");
            var lstTempCategories = new List<CategoryGridDTO>();

            foreach (var category in lstCategories)
            {
                lstTempCategories.Add(new CategoryGridDTO()
                {
                    Category = category.Category,
                    HappinessPercent = category.Happiness
                });
            }
            var results = from p in lstTempCategories
                          group p by p.Category into g
                          select new CategoryGridDTO()
                          {
                              Category = g.Key,
                              HappinessPercent = Math.Round(g.Average(x => x.HappinessPercent) * 100, 2)
                          };

            return results.OrderByDescending(x => x.HappinessPercent).ToList();
        }

        public async Task<List<CategoryGridDTO>> GetCategoriesChart()
        {
            var order = string.Empty;
            var _repository = new Repository<ImageInfoDTO>();
            var sql = string.Format("SELECT c.Category, " +
                "                           c.Happiness, " +
                "                           c.Anger, " +
                "                           c.Contempt," +
                "                           c.Disgust, " +
                "                           c.Fear, " +
                "                           c.Sadness, " +
                "                           c.Surprise, " +
                "                           c.Neutral  " +
                "FROM c");

            var lstCategories = await _repository.FindMultipleDynamic(sql, "faces-happy-meter");
            var lstTempCategories = new List<CategoryGridDTO>();

            foreach (var category in lstCategories)
            {
                lstTempCategories.Add(new CategoryGridDTO()
                {
                    Category = category.Category,
                    HappinessPercent = category.Happiness,
                    AngerPercent = category.Anger,
                    ContemptPercent = category.Contempt,
                    DisgustPercent = category.Disgust,
                    FearPercent = category.Fear,
                    SadnessPercent = category.Sadness,
                    SurprizePercent = category.Surprise,
                    NeutralPercent = category.Neutral
                });
            }
            var results = from p in lstTempCategories
                          group p by p.Category into g
                          select new CategoryGridDTO()
                          {
                              Category = g.Key,
                              HappinessPercent = Math.Round(g.Average(x => x.HappinessPercent) * 100, 2),
                              AngerPercent = Math.Round(g.Average(x => x.AngerPercent) * 100, 2),
                              FearPercent = Math.Round(g.Average(x => x.FearPercent) * 100, 2),
                              SadnessPercent = Math.Round(g.Average(x => x.SadnessPercent) * 100, 2),
                              SurprizePercent = Math.Round(g.Average(x => x.SurprizePercent) * 100, 2),
                              NeutralPercent = Math.Round(g.Average(x => x.NeutralPercent) * 100, 2),
                              ContemptPercent = Math.Round(g.Average(x => x.ContemptPercent) * 100, 2)
                          };

            return results.OrderByDescending(x => x.HappinessPercent).ToList();
        }
    }

}
