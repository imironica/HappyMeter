using HappyMeter.Models;
using HappyMeter.Models.Mapper;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;

namespace CognitiveServiceProxy
{
    public class MLService : IMLService
    {
        #region Private
        private IServiceProxy _serviceProxy;
        private string _apiCognitiveEmotionLink;
        private string _apiCognitiveFaceLink;
        private string _apiCognitiveCVLink;
        
        private string _apiContentType;
        private string _apiKeyName;
        private string _apiKeyEmotions;
        private string _apiKeyFace;
        private string _apiKeyCV;
        private MLMapper _emotionMapper;

        public MLService(IServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
            _apiCognitiveEmotionLink = ConfigurationManager.AppSettings["ApiCognitiveEmotionLink"].ToString();
            _apiCognitiveFaceLink = ConfigurationManager.AppSettings["ApiCognitiveFaceLink"].ToString();
            _apiCognitiveCVLink = ConfigurationManager.AppSettings["ApiCognitiveCVLink"].ToString();
            _apiContentType = ConfigurationManager.AppSettings["ApiContentType"].ToString();
            _apiKeyName = ConfigurationManager.AppSettings["ApiKeyName"].ToString();
            _apiKeyEmotions = ConfigurationManager.AppSettings["ApiKeyEmotions"].ToString();
            _apiKeyFace = ConfigurationManager.AppSettings["ApiKeyFace"].ToString();
            _apiKeyCV = ConfigurationManager.AppSettings["ApiKeyCV"].ToString();
            _emotionMapper = new MLMapper();
        }

        public FaceEmotionDTO[] GetEmotionsFromImage(byte[] imgdata)
        {
            _serviceProxy.SetParameters(_apiCognitiveEmotionLink, _apiKeyName, _apiKeyEmotions);
            var response = _serviceProxy.PostImageStream(imgdata);
            FaceEmotion[] emotions = JsonConvert.DeserializeObject<FaceEmotion[]>(response);
            var emotionDTOs = _emotionMapper.GetEmotionDTOListFromEmotionList(emotions).ToArray();
            return emotionDTOs;
        }

        public FaceEmotionDTO[] GetEmotionsFromLink(string imgLink)
        {
            var jsonPost = "{ \"url\": \"" + imgLink + "\" }";
            _serviceProxy.SetParameters(_apiCognitiveEmotionLink, _apiKeyName, _apiKeyEmotions);
            var response = _serviceProxy.PostJson(jsonPost);
            FaceEmotion[] emotions = JsonConvert.DeserializeObject<FaceEmotion[]>(response);
            var emotionDTOs = _emotionMapper.GetEmotionDTOListFromEmotionList(emotions).ToArray();
            return emotionDTOs;
        }

        public FaceInfoDTO[] GetFacesFromImage(byte[] imgdata)
        {
            _serviceProxy.SetParameters(_apiCognitiveFaceLink, _apiKeyName, _apiKeyFace);
            var response = _serviceProxy.PostImageStream(imgdata);
            FaceInfo[] faceInfo = JsonConvert.DeserializeObject<FaceInfo[]>(response);
            var facesDTOs = _emotionMapper.GetFaceDTOListFromFaceInfoList(faceInfo).ToArray();
            return facesDTOs;
        }

        public ObjectInfoDTO GetObjectsFromImage(byte[] imgdata)
        {
            _serviceProxy.SetParameters(_apiCognitiveCVLink, _apiKeyName, _apiKeyCV);
            var response = _serviceProxy.PostImageStream(imgdata);
            ObjectInfo objectInfo = JsonConvert.DeserializeObject<ObjectInfo>(response);
            var objDTO = _emotionMapper.GetObjectInfoDTOFromObjectInfo(objectInfo);
            return objDTO;
        }
        
    }
}
