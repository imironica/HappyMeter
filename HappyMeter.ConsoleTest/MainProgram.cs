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
        private int _pauseParameter = 1;
        string _db = "happy-meter";
        string _collection = "faces-happy-meter";
        string _endpointUri = "";
        string _primaryKey = "";

        public MainProgram(IServiceProxy serviceproxy, IEmotionService emotionService, IMLService mlService)
        {
            _emotionService = emotionService;
            _mlService = mlService;
            _endpointUri = ConfigurationManager.AppSettings["endpointUri"].ToString();
            _primaryKey = ConfigurationManager.AppSettings["primaryKey"].ToString();
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
                    ComputeAllCharacteristicsFromFolder();
                if (option == "6")
                    ComputeEmotionsFromFolderInDocumentDb();
            }
        }

        private void ComputeAllCharacteristicsFromFolder()
        {
            if (!VerifyWebConfigVariables()) return;

            var emotionMapper = new MLMapper();
            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();

            string[] dirs = Directory.GetDirectories(sourceFolder);
            foreach (var dir in dirs)
            {
                string[] files = Directory.GetFiles(dir);
                Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (file) =>
                {
                    var mlService = new MLService(new ServiceProxyCognitiveAzure());
                    string lastFolderName = Path.GetFileName(Path.GetDirectoryName(file));
                    string fileName = Path.GetFileName(file);
                    Console.WriteLine(string.Format("Processing {0}", file));
                    byte[] imgdata = System.IO.File.ReadAllBytes(file);
                    FaceEmotionDTO[] emotionDTOs = mlService.GetEmotionsFromImage(imgdata);

                    if (emotionDTOs != null && emotionDTOs.Count() > 0)
                    {
                        FaceInfoDTO[] faceInfo = mlService.GetFacesFromImage(imgdata);
                        ObjectInfoDTO objectsDTOs = mlService.GetObjectsFromImage(imgdata);

                        var imageinfo = new ImageInfoDTO()
                        {
                            Emotions = emotionDTOs,
                            Faces = faceInfo,
                            Objects = objectsDTOs,
                            Category = lastFolderName,
                            ImageUrl = fileName,
                            Anger = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Anger : 0),
                            Contempt = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Contempt : 0),
                            Disgust = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Disgust : 0),
                            Fear = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Fear : 0),
                            Happiness = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Happiness : 0),
                            Neutral = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Neutral : 0),
                            Sadness = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Sadness : 0),
                            Surprise = emotionDTOs.Average(x => x.Scores != null ? x.Scores.Surprise : 0)
                        };

                        if (imageinfo != null)
                        {
                            _emotionService.AddEmotion(imageinfo);
                        }
                        Thread.Sleep(_pauseParameter);
                    }
                }
                );
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
            Console.WriteLine("5 - Compute all characteristics from folder: ");
            Console.WriteLine("6 - Insert to DocumentDb: ");
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
            DocumentClient client;
            client = new DocumentClient(new Uri(_endpointUri), _primaryKey);

            // Check to verify a database with the id=FamilyDB does not exist
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
            }
            catch (DocumentClientException de)
            {
                // If the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = databaseName });
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
            client = new DocumentClient(new Uri(_endpointUri), _primaryKey);
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
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
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<Document> AddElementToDocumentDb(string databaseName, string collectionName, InfoDTO emotions)
        {
            DocumentClient client;
            client = new DocumentClient(new Uri(_endpointUri), _primaryKey);
            var result = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), emotions);
            var document = result.Resource;
            return result;
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

        private void ComputeEmotionsFromFolderInDocumentDb()
        {
            CreateDatabaseIfNotExists(_db).Wait();
            CreateDocumentCollectionIfNotExists(_db, _collection).Wait();
            var collectionLink = UriFactory.CreateDocumentCollectionUri(_db, _collection);
            DocumentClient client;

            var emotionMapper = new MLMapper();
            var sourceFolder = ConfigurationManager.AppSettings["SourceFolder"].ToString();
            var saveFolder = ConfigurationManager.AppSettings["SaveFolder"].ToString();
            string[] dirs = Directory.GetDirectories(saveFolder);
            string category = Path.GetFileName(Path.GetDirectoryName(saveFolder));

            using (client = new DocumentClient(new Uri(_endpointUri), _primaryKey, ConnectionPolicy))
            {
                foreach (var dir in dirs)
                {
                    string[] files = Directory.GetFiles(dir);
                    foreach (var file in files)
                    {

                        var imageStr = File.ReadAllText(file);
                        ImageInfoDTO objectInfo = JsonConvert.DeserializeObject<ImageInfoDTO>(imageStr);
                        objectInfo.Id = Path.GetFileName(objectInfo.ImageUrl);

                        Console.WriteLine(objectInfo.Id);
                        AddDocument(client, _db, _collection, objectInfo).Wait();
                    }
                }

            }
        }

        private async Task AddDocument(DocumentClient client, string databaseName, string collectionName, ImageInfoDTO dto)
        {
            await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), dto);
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
