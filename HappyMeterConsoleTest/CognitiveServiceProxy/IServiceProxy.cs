namespace CognitiveServiceProxy
{
    public interface IServiceProxy
    {
        void SetParameters(string apiLink, string apiKeyName, string apiKey);
        string PostImageStream(byte[] request);
        string PostJson(string request);
    }
}