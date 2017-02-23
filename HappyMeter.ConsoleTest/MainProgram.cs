using CognitiveServiceProxy;
using HappyMeter.Models;
using HappyMeter.Services;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;

namespace HappyMeterConsoleTest
{
    public class MainProgram
    {
        private IServiceProxy _serviceProxy;
        private IEmotionService _emotionService;
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
                Console.WriteLine(string.Format("Processing {0}", file));
                byte[] imgdata = System.IO.File.ReadAllBytes(file);

                var response = _serviceProxy.PostImageStream(imgdata);
                FaceEmotion[] emotions = JsonConvert.DeserializeObject<FaceEmotion[]>(response);
                string lastFolderName = Path.GetFileName(Path.GetDirectoryName(file));

                InfoDTO dto = new InfoDTO()
                {
                    Emotions = emotions,
                    Category = "Web",
                    Image = file
                };
                _emotionService.AddEmotion(dto);
            }

        }

        private void ComputeEmotionsFromLink()
        {
            if (!VerifyWebConfigVariables()) return;

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
                Emotions = emotions,
                Category = "Web",
                Image = file
            };
            _emotionService.AddEmotion(dto);
        }

        private void ShowOptions()
        {
            Console.WriteLine("Options: ");
            Console.WriteLine("1 - Compute emotions from folder: ");
            Console.WriteLine("2 - Compute emotions from link: ");
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
