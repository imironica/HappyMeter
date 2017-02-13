namespace CognitiveServiceProxy
{
    public interface IServiceProxy
    {
        string PostImageStream(byte[] request);
        string PostJson(string request);
    }
}