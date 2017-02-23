using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveServiceProxy
{
    public class ServiceProxyCognitiveAzure : IServiceProxy
    {
        private HttpClient _client;
        private string _apiLink;
        private string _apiKeyName;
        private string _apiKey;
        private string _contentTypeKey = "Content-Type";
        public ServiceProxyCognitiveAzure()
        {
        }

        public void SetParameters(string apiLink, string apiKeyName, string apiKey)
        {
            _apiLink = apiLink;
            _apiKeyName = apiKeyName;
            _apiKey = apiKey;

            _client = new HttpClient
            {
                BaseAddress = new Uri(apiLink)
            };
        }

        

        public string PostJson(string request)
        {
            string result = string.Empty;
            string contentType = "application/json";
            HttpContent content = new StringContent(request, Encoding.UTF8, contentType);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue(contentType));
            _client.DefaultRequestHeaders.Add(_apiKeyName, _apiKey);
            var serviceTask = _client.PostAsync(_apiLink, content);
            serviceTask.Wait();
            var newTask = ((Task<HttpResponseMessage>)serviceTask).ContinueWith(task =>
            {
                var response = task.Result;
                var jsonTask = response.Content.ReadAsStringAsync();
                jsonTask.Wait();
                result = jsonTask.Result;
            });
            newTask.Wait();
            return result;
        }

        public string PostImageStream(byte[] request)
        {
            string result = string.Empty;
            string contentType = "application/octet-stream";
            HttpContent content = new StreamContent(new MemoryStream(request));
            content.Headers.Add(_contentTypeKey, contentType);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue(contentType));
            _client.DefaultRequestHeaders.Add(_apiKeyName, _apiKey);
            var serviceTask = _client.PostAsync(_apiLink, content);
            serviceTask.Wait();

            var newTask = ((Task<HttpResponseMessage>)serviceTask).ContinueWith(task =>
            {
                var response = task.Result;
                var jsonTask = response.Content.ReadAsStringAsync();
                jsonTask.Wait();
                result = jsonTask.Result;
            });
            newTask.Wait();
            return result;
        }
    }
}
