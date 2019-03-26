using System.Net;

namespace TFLRoadStatus.Repository
{
    public interface IApiClient
    {
        HttpStatusCode StatusCode { get; }

        string GetResponse(string roadID);

        string CreateUrl(IConfig config, string roadID);
    }
}