using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace TFLRoadStatus.Common
{
    public class HttpMessageStub
    {
        private const string ConstHttpStatus400 = "HttpStatus400";
        private const string ConstHttpStatus404 = "HttpStatus404";
        private const string ConstHttpStatus200 = "HttpStatus200";

        public HttpMessageStub(string requestUrl)
        {
            RequestURL = requestUrl;
        }

        public string RequestURL { get; set; }

        public Mock<HttpMessageHandler> CreateMockHttpMessageStub()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(GetStringContent())
                .Verifiable();
            return handlerMock;
        }

        public HttpResponseMessage GetStringContent()
        {
            var response = new HttpResponseMessage();
            if (RequestURL != null && RequestURL.IndexOf(ConstHttpStatus200) >= 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent("[" +
                                                     "{" +
                                                     "  \"$type\": \"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\"," +
                                                     "  \"id\": \"a2\"," +
                                                     "  \"displayName\": \"A2\"," +
                                                     "  \"statusSeverity\": \"Good\"," +
                                                     "  \"statusSeverityDescription\": \"No Exceptional Delays\"," +
                                                     "  \"bounds\": \"[[-0.0857,51.44091],[0.17118,51.49438]]\"," +
                                                     "  \"envelope\": \"[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]\"," +
                                                     "  \"url\": \"/Road/a2\" " +
                                                     "}]");
            }
            else if (RequestURL != null && RequestURL.IndexOf(ConstHttpStatus400) >= 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            else if (RequestURL != null && RequestURL.IndexOf(ConstHttpStatus404) >= 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Content = new StringContent(
                    "{ \"$type\": \"Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities\", " +
                    " \"timestampUtc\": \"2018-08-26T06:16:38.3973386Z\", " +
                    " \"exceptionType\": \"EntityNotFoundException\", " +
                    " \"httpStatusCode\": 404, " +
                    " \"httpStatus\": \"NotFound\", " +
                    " \"relativeUri\": \"/Road/A223\", " +
                    " \"message\": \"The following road id is not recognised: A223\"} ");
            }

            return response;
        }
    }
}