using CognitiveServiceProxy;
using HappyMeter.Models;
using HappyMeter.Models.Mapper;
using HappyMeter.Services;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace HappyMeterConsoleTest
{
    public class MainProgram
    {
        private IServiceProxy _serviceProxy;
        private IEmotionService _emotionService;
        private int _pauseParameter = 3100;
        public MainProgram(IServiceProxy serviceproxy, IEmotionService emotionService)
        {
            _serviceProxy = serviceproxy;
            _emotionService = emotionService;
        }

        public void Run()
        {
            var option = string.Empty;
            while (option != "0")
            {
                ShowOptions();
                option = Console.ReadLine();
                if (option == "0")
                    return;
                if (option == "1")
                    ComputeEmotionsFromFolder();
                if (option == "2")
                    ComputeEmotionsFromLink();
            }
        }

        private void ComputeEmotionsFromFolder()
        {
            if(!VerifyWebConfigVariables()) return;
            var emotionMapper = new EmotionMapper();

            var apiCognitiveEmotionLink = ConfigurationManager.AppSettings["ApiCognitiveEmotionLink"].ToString();
            var apiContentType = ConfigurationManager.AppSettings["ApiContentType"].ToString();
            var apiKeyName = ConfigurationManager.AppSettings["ApiKeyName"].ToString();
            var apiKey = ConfigurationManager.AppSettings["ApiKey"].ToString();
            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            _serviceProxy.SetParameters(apiCognitiveEmotionLink, apiKeyName, apiKey);
            
            var files = Directory.GetFiles(sourceFolder);
            foreach (var file in files)
            {
                Thread.Sleep(_pauseParameter);
                string lastFolderName = Path.GetFileName(Path.GetDirectoryName(file));
                InfoDTO dto = new InfoDTO()
                {
                    Category = lastFolderName,
                    Image = file
                };
                Console.WriteLine(string.Format("Processing {0}", file));

                if (!_emotionService.EmotionAllreadyComputed(dto))
                {
                    byte[] imgdata = System.IO.File.ReadAllBytes(file);
                    var response = _serviceProxy.PostImageStream(imgdata);
                    FaceEmotion[] emotions = JsonConvert.DeserializeObject<FaceEmotion[]>(response);

                    var emotionDTOs = emotionMapper.GetEmotionDTOListFromEmotionList(emotions);
                    if (emotionDTOs != null)
                    {
                        dto.Emotions = emotionDTOs.ToArray();
                        _emotionService.AddEmotion(dto);
                    }
                }
            }

        }

        private void ComputeEmotionsFromLink()
        {
            if (!VerifyWebConfigVariables()) return;
            var emotionMapper = new EmotionMapper();
            var apiCognitiveEmotionLink = ConfigurationManager.AppSettings["ApiCognitiveEmotionLink"].ToString();
            var apiContentType = ConfigurationManager.AppSettings["ApiContentType"].ToString();
            var apiKeyName = ConfigurationManager.AppSettings["ApiKeyName"].ToString();
            var apiKey = ConfigurationManager.AppSettings["ApiKey"].ToString();
            var imageLink = ConfigurationManager.AppSettings["ImageLink"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            _serviceProxy.SetParameters(apiCognitiveEmotionLink, apiKeyName, apiKey);

            Uri uri = new Uri(imageLink);
            string file = System.IO.Path.GetFileName(uri.AbsolutePath);
            Console.WriteLine(string.Format("Processing {0}", imageLink));

            var jsonPost = "{ \"url\": \"" + imageLink + "\" }";
            var response = _serviceProxy.PostJson(jsonPost);
            FaceEmotion[] emotions = JsonConvert.DeserializeObject<FaceEmotion[]>(response);

  
            InfoDTO dto = new InfoDTO()
            {
                Category = "Web",
                Image = file
            };
            var emotionDTOs = emotionMapper.GetEmotionDTOListFromEmotionList(emotions);
            if (emotionDTOs != null)
            {
                dto.Emotions = emotionDTOs.ToArray();
                _emotionService.AddEmotion(dto);
            }
        }

        private void ShowOptions()
        {
            Console.WriteLine("Options: ");
            Console.WriteLine("1 - Compute emotions from folder: ");
            Console.WriteLine("2 - Compute emotions from link: ");
            Console.WriteLine("3 - Import json files to MongoDb: ");
            Console.WriteLine("0 - Exit: ");
        }

        private bool VerifyWebConfigVariables()
        {
            var valid = true;
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiCognitiveEmotionLink"]))
            {
                Console.WriteLine("ApiCognitiveEmotionLink not set in Webconfig");
                valid = false;
            }

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiContentType"]))
            {
                Console.WriteLine("ApiContentType not set in Webconfig");
                valid = false;
            }

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiKeyName"]))
            {
                Console.WriteLine("ApiKeyName not set in Webconfig");
                valid = false;
            }

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SourceFolder"]))
            {
                Console.WriteLine("SourceFolder not set in Webconfig");
                valid = false;
            }

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiKey"]))
            {
                Console.WriteLine("ApiKey not set in Webconfig");
                valid = false;
            }

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SourceFolder"]))
            {
                Console.WriteLine("SourceFolder not set in Webconfig");
                valid = false;
            }
            return valid;
        }
    }
}
