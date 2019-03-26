using System.Linq;
using System.Net.Http;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using TFLRoadStatus.Common;
using TFLRoadStatus.Models;
using TFLRoadStatus.Repository;

namespace TFLRoadStatus.UnitTests
{
 public   class ApiClientTest
    {
        private readonly Mock<IConfig> ConfigMock;
        private  IApiClient ApiClient;
        private string RoadID;

        public ApiClientTest()
        {
            ConfigMock = new Mock<IConfig>();
        }

        [Test]
        public void When_Execute_Request_With_Correct_Url()
        {
            this.RoadID = "A2";
            var expectedRoad = "A2";
            var expectedSeverity = "Good";

            SetupConfigUri("http://testurl/HttpStatus200");
            ApiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub().Object));
            var roadStatus = GetValidRoadStatus<RoadCorridorStatus>().FirstOrDefault();

            Assert.NotNull(roadStatus);

            Assert.AreEqual(expectedRoad, roadStatus.displayName);
            Assert.AreEqual(expectedSeverity, roadStatus.statusSeverity);
        }

        [Test]
        public void When_Execute_Request_With_Wrong_Url()
        {
            this.RoadID = "A2";
            var expectedHttpStatus = "NotFound";
            var expectedHttpStatusCode = "404";

            SetupConfigUri("http://testurl/HttpStatus400");

            ApiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub().Object));
            HttpRequestException exception = Assert.Throws<HttpRequestException>(() => ApiClient.GetResponse(this.RoadID));
        }

        [Test]
        public void When_Execute_Request_With_Invalid_Road_Then_Returns_Json()
        {
            this.RoadID = "A223";
            var expectedHttpStatus = "NotFound";
            var expectedHttpStatusCode = "404";

            SetupConfigUri("http://testurl/HttpStatus404");

            ApiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub().Object));
            var roadStatus = GetInvalidRoadStatus<NotFoundError>();

            Assert.NotNull(roadStatus);

            Assert.AreEqual(expectedHttpStatus, roadStatus.httpStatus);
            Assert.AreEqual(expectedHttpStatusCode, roadStatus.httpStatusCode);
        }

        private NotFoundError GetInvalidRoadStatus<NotFoundError>()
        {
            var result = ApiClient.GetResponse(this.RoadID);
            return JsonConvert.DeserializeObject<NotFoundError>(result);
        }

        private RoadCorridorStatus[] GetValidRoadStatus<RoadCorridorStatus>()
        {
            var result = ApiClient.GetResponse(this.RoadID);
            return JsonConvert.DeserializeObject<RoadCorridorStatus[]>(result);
        }

        private void SetupConfigUri(string uri)
        {
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => uri);
        }
    }


}
