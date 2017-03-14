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
        private IEmotionService _emotionService;
        private IMLService _mlService;
        private int _pauseParameter = 3100;
        public MainProgram(IServiceProxy serviceproxy, IEmotionService emotionService, IMLService mlService)
        {
            _emotionService = emotionService;
            _mlService = mlService;
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
                if (option == "3")
                    ComputeFacesFromFolder();
                if (option == "4")
                    ComputeObjectsFromFolder();
            }
        }

        private void ComputeEmotionsFromFolder()
        {
            if(!VerifyWebConfigVariables()) return;
            var emotionMapper = new MLMapper();
           
            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            var files = Directory.GetFiles(sourceFolder);
            foreach (var file in files)
            {
                string lastFolderName = Path.GetFileName(Path.GetDirectoryName(file));
                InfoDTO dto = new InfoDTO()
                {
                    Category = lastFolderName,
                    Image = file
                };
                Console.WriteLine(string.Format("Processing {0}", file));

                if (!_emotionService.EmotionAllreadyComputed(dto))
                {
                    Thread.Sleep(_pauseParameter);
                    byte[] imgdata = System.IO.File.ReadAllBytes(file);
                    FaceEmotionDTO[] emotionDTOs = _mlService.GetEmotionsFromImage(imgdata);
                     
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

            var imageLink = ConfigurationManager.AppSettings["ImageLink"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            Uri uri = new Uri(imageLink);
            string file = System.IO.Path.GetFileName(uri.AbsolutePath);
            Console.WriteLine(string.Format("Processing {0}", imageLink));

            var jsonPost = "{ \"url\": \"" + imageLink + "\" }";
            FaceEmotionDTO[] emotionDTOs = _mlService.GetEmotionsFromLink(jsonPost);

            InfoDTO dto = new InfoDTO()
            {
                Category = "Web",
                Image = file
            };
             
            if (emotionDTOs != null)
            {
                dto.Emotions = emotionDTOs;
                _emotionService.AddEmotion(dto);
            }
        }

        private void ComputeFacesFromFolder()
        {
            if (!VerifyWebConfigVariables()) return;
            var emotionMapper = new MLMapper();

            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            var files = Directory.GetFiles(sourceFolder);
            foreach (var file in files)
            {
                string lastFolderName = Path.GetFileName(Path.GetDirectoryName(file));
                InfoDTO dto = new InfoDTO()
                {
                    Category = lastFolderName,
                    Image = file
                };
                Console.WriteLine(string.Format("Processing {0}", file));

                 
                    Thread.Sleep(_pauseParameter);
                    byte[] imgdata = System.IO.File.ReadAllBytes(file);
                    var emotionDTOs = _mlService.GetFacesFromImage(imgdata);

                /*
                    if (emotionDTOs != null)
                    {
                        dto.Emotions = emotionDTOs.ToArray();
                        _emotionService.AddEmotion(dto);
                    }*/
                
            }
        }

        private void ComputeObjectsFromFolder()
        {
            if (!VerifyWebConfigVariables()) return;
            var emotionMapper = new MLMapper();

            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            var files = Directory.GetFiles(sourceFolder);
            foreach (var file in files)
            {
                string lastFolderName = Path.GetFileName(Path.GetDirectoryName(file));
                InfoDTO dto = new InfoDTO()
                {
                    Category = lastFolderName,
                    Image = file
                };
                Console.WriteLine(string.Format("Processing {0}", file));

                Thread.Sleep(_pauseParameter);
                byte[] imgdata = System.IO.File.ReadAllBytes(file);
                ObjectInfoDTO objectsDTOs = _mlService.GetObjectsFromImage(imgdata);
            }
        }

        private void ShowOptions()
        {
            Console.WriteLine("Options: ");
            Console.WriteLine("1 - Compute emotions from folder: ");
            Console.WriteLine("2 - Compute emotions from link: ");
            Console.WriteLine("3 - Compute face characteristics from folder: ");
            Console.WriteLine("4 - Compute objects from folder: ");
            Console.WriteLine("5 - Import json files to MongoDb: ");
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

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiKeyEmotions"]))
            {
                Console.WriteLine("ApiKey for EmotionAPI not set in Webconfig");
                valid = false;
            }
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiKeyFace"]))
            {
                Console.WriteLine("ApiKey for FaceAPI not set in Webconfig");
                valid = false;
            }
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ApiKeyCV"]))
            {
                Console.WriteLine("ApiKeyCV for CVAPI not set in Webconfig");
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
