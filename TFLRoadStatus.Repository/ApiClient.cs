using System.Net;
using System.Net.Http;

namespace TFLRoadStatus.Repository
{
    public class ApiClient : IApiClient
    {
        private readonly IConfig _config;
        private readonly HttpClient _httpClient;

        public ApiClient(IConfig config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string GetResponse(string roadID)
        {
            var content = string.Empty;

            using (_httpClient)
            {
                var requestUrl = CreateUrl(_config, roadID);

                var httpContent = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var response = _httpClient.SendAsync(httpContent).Result;

                StatusCode = response.StatusCode;

                if (response.StatusCode != HttpStatusCode.OK &&
                    response.StatusCode != HttpStatusCode.NotFound)
                    throw new HttpRequestException($"Error request http status code {response.StatusCode}");

                content = response.Content.ReadAsStringAsync().Result;
            }

            return content;
        }

        public string CreateUrl(IConfig config, string roadID)
        {
            return string.Format(config.ApiUrl + "{0}?app_id={1}&app_key={2}", roadID, config.AppID, config.AppKey);
        }
    }
}