using CognitiveServiceProxy;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using HappyMeter.Models;
using HappyMeter.Models.Mapper;
using HappyMeter.Services;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HappyMeterConsoleTest
{
    public class MainProgram
    {
        private IEmotionService _emotionService;
        private IMLService _mlService;
        private int _pauseParameter = 3100;
        string endpointUri = "https://happy-meter.documents.azure.com:443/";
        string primaryKey = "";

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
                if (option == "5")
                    ComputeEmotionsFromFolderInDocumentDb();

            }
        }




        private void ComputeEmotionsFromFolder()
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
            Console.WriteLine("5 - Import emotions to DocumentDb: ");
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


        private async Task CreateDatabaseIfNotExists(string databaseName)
        {
            string endpointUri = "https://happy-meter.documents.azure.com:443/";
            string primaryKey = "CPXWUmc2FzFbSZUeJnRic4GJ0KYigFcQYvtM6EFIsFDrAF76GXm2aFGACF1SUXlACJfommZk5e5kI64wJPZ5jg==";
            DocumentClient client;
            client = new DocumentClient(new Uri(endpointUri), primaryKey);

            // Check to verify a database with the id=FamilyDB does not exist
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
                //this.WriteToConsoleAndPromptToContinue("Found {0}", databaseName);
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = databaseName });
                    //this.WriteToConsoleAndPromptToContinue("Created {0}", databaseName);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        {
            DocumentClient client;
            client = new DocumentClient(new Uri(endpointUri), primaryKey);

            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
                //this.WriteToConsoleAndPromptToContinue("Found {0}", collectionName);
            }
            catch (DocumentClientException de)
            {
                // If the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    DocumentCollection collectionInfo = new DocumentCollection();
                    collectionInfo.Id = collectionName;

                    // Configure collections for maximum query flexibility including string range queries.
                    collectionInfo.IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) { Precision = -1 });

                    // Here we create a collection with 400 RU/s.
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseName),
                        collectionInfo,
                        new RequestOptions { OfferThroughput = 400 });

                    //this.WriteToConsoleAndPromptToContinue("Created {0}", collectionName);
                }
                else
                {
                    throw;
                }
            }
        }
        private void WriteToConsoleAndPromptToContinue(string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        private async Task<Document> AddElementToDocumentDb(string databaseName, string collectionName, InfoDTO emotions)
        {
            DocumentClient client;
            client = new DocumentClient(new Uri(endpointUri), primaryKey);
            var result = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), emotions);
            var document = result.Resource;
            return result;
        }

        

        private void ComputeEmotionsFromFolderInDocumentDb()
        {
            var db = "happy-meter";
            var collection = "faces-happy-meter";
            //CreateDatabaseIfNotExists(db).Wait();
            //CreateDocumentCollectionIfNotExists(db, collection).Wait();

            
            DocumentClient client;
            client = new DocumentClient(new Uri(endpointUri), primaryKey);
            var emotionMapper = new MLMapper();

            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            var files = Directory.GetFiles(sourceFolder);
            int index = 1;
            string username = "username";
            while (true)
            {
                var dto = new InfoDTO()
                {
                    Category = username,
                    Image = index.ToString(),
                    CreatedAt = DateTime.Now,
                    Id = Guid.NewGuid()
                };
                Console.WriteLine(string.Format("Processing {0}", index.ToString()));

                Thread.Sleep(_pauseParameter);
                byte[] imgdata = TakeImageFromLocalCamera();
                FaceEmotionDTO[] emotionDTOs = _mlService.GetEmotionsFromImage(imgdata);

                if (emotionDTOs != null)
                {
                    dto.Emotions = emotionDTOs.ToArray();
                    if (dto.Emotions.Length > 0)
                    {
                        AddElementToDocumentDb(db, collection, dto).Wait();
                    }
                }

                index++;
            }

        }

        private byte[] TakeImageFromLocalCamera()
        {

            ImageViewer viewer = new ImageViewer(); //create an image viewer
            Capture capture = new Capture(); //create a camera captue

            var img = capture.QueryFrame().ToImage<Bgr, Byte>().ToBitmap();

            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
