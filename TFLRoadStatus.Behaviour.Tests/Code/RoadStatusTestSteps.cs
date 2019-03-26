using System.Net.Http;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TFLRoadStatus.Common;
using TFLRoadStatus.Repository;

namespace TFLRoadStatus.Behaviour.Tests.Code
{
    [Binding]
    public class RoadStatusTestSteps
    {
        private const string TestOkUrl = @"http://test/HttpStatus200";
        private const int ExpectedExitCode = 0;
        private readonly IApiClient apiClient;

        private readonly Mock<IConfig> ConfigMock;
        private readonly IPrinterClient printerClient;
        private readonly IRoadStatusProcessor roadStatusProcessor;

        private int ExitCode;
        private string RequestRoadID;

        public RoadStatusTestSteps()
        {
            ConfigMock = new Mock<IConfig>();
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => TestOkUrl);

            apiClient = new ApiClient(ConfigMock.Object,
                new HttpClient(new HttpMessageStub(TestOkUrl).CreateMockHttpMessageStub().Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
        }

        [Given(@"a valid road ID '(.*)' is specified")]
        public void GivenAValidRoadIDIsSpecified(string roadID)
        {
            RequestRoadID = roadID;
            ExitCode = roadStatusProcessor.GetRoadCurrentStatus(RequestRoadID);
        }

        [When(@"the client is run")]
        public void WhenTheClientIsRun()
        {
        }

        [Then(@"the road '(.*)' should be displayed")]
        public void ThenTheRoadShouldBeDisplayed(string p0)
        {
            Assert.AreEqual(ExpectedExitCode, ExitCode);
            Assert.NotNull(roadStatusProcessor.RoadCorridorStatus.displayName);
            Assert.IsNotEmpty(roadStatusProcessor.RoadCorridorStatus.displayName);

            Assert.AreEqual(RequestRoadID, roadStatusProcessor.RoadCorridorStatus.displayName);
        }

        [Then(@"the road '(.*)' should be displayed as '(.*)'")]
        public void ThenTheRoadShouldBeDisplayedAs(string key, string value)
        {
            Assert.AreEqual(roadStatusProcessor.PrinterClient.StatusSeverityCollection[key], value);
            Assert.AreEqual(ExpectedExitCode, ExitCode);
            Assert.NotNull(roadStatusProcessor.RoadCorridorStatus.statusSeverity);
            Assert.IsNotEmpty(roadStatusProcessor.RoadCorridorStatus.statusSeverity);
        }
    }
}