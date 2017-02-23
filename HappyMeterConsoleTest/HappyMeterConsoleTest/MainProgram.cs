﻿using CognitiveServiceProxy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMeterConsoleTest
{
    public class MainProgram
    {
        private IServiceProxy _serviceProxy;
        public MainProgram(IServiceProxy serviceproxy)
        {
            _serviceProxy = serviceproxy;
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
            var apiCognitiveEmotionLink = ConfigurationManager.AppSettings["ApiCognitiveEmotionLink"].ToString();
            var apiContentType = ConfigurationManager.AppSettings["ApiContentType"].ToString();
            var apiKeyName = ConfigurationManager.AppSettings["ApiKeyName"].ToString();
            var apiKey = ConfigurationManager.AppSettings["ApiKey"].ToString();
            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();
            _serviceProxy.SetParameters(apiCognitiveEmotionLink, apiKeyName, apiKey);
            
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            var files = Directory.GetFiles(sourceFolder);
            foreach (var file in files)
            {
                Console.WriteLine(string.Format("Processing {0}", file));
                byte[] imgdata = System.IO.File.ReadAllBytes(file);

                var response = _serviceProxy.PostImageStream(imgdata);
                //FaceEmotion[] account = JsonConvert.DeserializeObject<FaceEmotion[]>(response);
                //var emotionStr = JsonConvert.SerializeObject(response);
                var filename = Path.Combine(saveFolder, Path.GetFileName(file)) + ".txt";
                File.WriteAllText(filename, response);
            }

        }

        private void ComputeEmotionsFromLink()
        {
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
            //FaceEmotion[] account = JsonConvert.DeserializeObject<FaceEmotion[]>(response);
            //var emotionStr = JsonConvert.SerializeObject(response);
            if (!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);
            var filename = Path.Combine(saveFolder, file) + ".txt";
            File.WriteAllText(filename, response);
        }

        private void ShowOptions()
        {
            Console.WriteLine("Options: ");
            Console.WriteLine("1 - Compute emotions from folder: ");
            Console.WriteLine("2 - Compute emotions from link: ");
            Console.WriteLine("0 - Exit: ");
        }
    }
}
