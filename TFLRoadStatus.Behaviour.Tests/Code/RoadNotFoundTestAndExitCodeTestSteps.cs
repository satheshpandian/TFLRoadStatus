using System.Net.Http;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TFLRoadStatus.Common;
using TFLRoadStatus.Repository;

namespace TFLRoadStatus.Behaviour.Tests.Code
{
    [Binding]
    public class RoadNotFoundTestAndExitCodeTestSteps
    {
        private const string InValidUrl = @"http://test/HttpStatus404";
        private const int ExpectedExitCode = 0;
        private readonly IApiClient apiClient;

        private readonly Mock<IConfig> ConfigMock;
        private readonly IPrinterClient printerClient;
        private readonly IRoadStatusProcessor roadStatusProcessor;

        public int ExitCode;

        public RoadNotFoundTestAndExitCodeTestSteps()
        {
            ConfigMock = new Mock<IConfig>();
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => InValidUrl);

            apiClient = new ApiClient(ConfigMock.Object,
                new HttpClient(new HttpMessageStub(InValidUrl).CreateMockHttpMessageStub().Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
        }

        [Given(@"an invalid road ID '(.*)' is passed")]
        public void GivenAnInvalidRoadIDIsPassed(string roadID)
        {
            ExitCode = roadStatusProcessor.GetRoadCurrentStatus(roadID);
        }

        [Then(@"the application should return an informative error")]
        public void ThenTheApplicationShouldReturnAnInformativeError()
        {
            var expectedHttpStatusCode = "404";
            var expectedHttpStatus = "NotFound";

            Assert.AreEqual(expectedHttpStatusCode, roadStatusProcessor.HttpNotFoundError.httpStatusCode);
            Assert.AreEqual(expectedHttpStatus, roadStatusProcessor.HttpNotFoundError.httpStatus);
        }

        [Then(@"the application should exit with a non-zero system Error code")]
        public void ThenTheApplicationShouldExitWithANon_ZeroSystemErrorCode()
        {
            var expectedExitCode = 1;
            Assert.AreEqual(expectedExitCode, ExitCode);
        }
    }
}