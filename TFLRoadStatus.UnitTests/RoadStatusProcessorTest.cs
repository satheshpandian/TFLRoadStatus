using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using TFLRoadStatus.Common;
using TFLRoadStatus.Repository;

namespace TFLRoadStatus.UnitTests
{
    
    public class RoadStatusProcessorTest
    {
        private string RequestRoadID;


        private const string TestOkUrl = @"http://test/HttpStatus200";
        private  IApiClient apiClient;
        private  IPrinterClient printerClient;
        private  IRoadStatusProcessor roadStatusProcessor;

        private int ExitCode;
        private int ExpectedExitCode = 0;

        private readonly Mock<IConfig> ConfigMock;

        public RoadStatusProcessorTest()
        {
            ConfigMock = new Mock<IConfig>();
        }
        [Test]
        public void When_RoadStatus_Valid_Request_Is_Executed_Returns_RoadStatus()
        {
            RequestRoadID = "A2";
            ExpectedExitCode = 0;
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => "http://testurl/HttpStatus200");
            apiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(TestOkUrl).CreateMockHttpMessageStub().Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);

            var returnCode = this.ExecuteMethod();

            Assert.AreEqual(ExpectedExitCode, returnCode);
        }

        [Test]
        public void When_RoadStatus_Invalid_Request_Is_Executed_Returns_NotFound()
        {
            RequestRoadID = "A223";
            ExpectedExitCode = 1;
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => "http://testurl/HttpStatus404");
            apiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub().Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
            var returnCode = this.ExecuteMethod();

            Assert.AreEqual(ExpectedExitCode, returnCode);
        }
        [Test]
        public void When_RoadStatus_Invalid_Request_Is_Executed_And_Api_Down_Returns_NotFound()
        {
            RequestRoadID = "A223";
            ExpectedExitCode = -1;
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => "http://testurl/HttpStatus400");
            apiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub().Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
            HttpRequestException exception = Assert.Throws<HttpRequestException>(() => roadStatusProcessor.GetRoadCurrentStatus(this.RequestRoadID));

            Assert.AreEqual("Error request http status code BadRequest", exception.Message);
        }

        [Test]
        public void When_RoadStatus_RoadID_Empty()
        {
            RequestRoadID = "";
            ExpectedExitCode = -1;
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => "http://testurl/HttpStatus200");
            apiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub().Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
            ArgumentNullException exception= Assert.Throws<ArgumentNullException>(() => roadStatusProcessor.GetRoadCurrentStatus(this.RequestRoadID)); 

            Assert.AreEqual( "Value cannot be null.", exception.Message);
        }

        [Test]
        public void When_RoadStatus_Api_Returns_Empty()
        {
            RequestRoadID = "A2";
            ExpectedExitCode = -1;
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => "http://testurl/HttpStatus200");
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>("SendAsync",ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content=new StringContent("")
                }).Verifiable(); 

            apiClient = new ApiClient(ConfigMock.Object, new HttpClient(handlerMock.Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
            InvalidDataException exception = Assert.Throws<InvalidDataException>(() => roadStatusProcessor.GetRoadCurrentStatus(this.RequestRoadID));

            Assert.AreEqual("No Data", exception.Message);
        }

        [Test]
        public void When_RoadStatus_Api_BadRequest_Returns_Empty()
        {
            RequestRoadID = "A2";
            ExpectedExitCode = -1;
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => "http://testurl/HttpStatus200");
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("")
                }).Verifiable();

            apiClient = new ApiClient(ConfigMock.Object, new HttpClient(handlerMock.Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
            HttpRequestException exception = Assert.Throws<HttpRequestException>(() => roadStatusProcessor.GetRoadCurrentStatus(this.RequestRoadID));

            Assert.AreEqual("Error request http status code BadRequest", exception.Message);
        }


        [Test]
        public void When_RoadStatus_Api_BadRequest_Returns_Content()
        {
            RequestRoadID = "A2";
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => "http://testurl/HttpStatus200");
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("[]")
                }).Verifiable();

            apiClient = new ApiClient(ConfigMock.Object, new HttpClient(handlerMock.Object));

            printerClient = new PrinterClient();
            roadStatusProcessor = new RoadStatusProcessor(apiClient, printerClient);
            HttpRequestException exception = Assert.Throws<HttpRequestException>(() => roadStatusProcessor.GetRoadCurrentStatus(this.RequestRoadID));

            Assert.AreEqual("Error request http status code BadRequest", exception.Message);
        }
        private int ExecuteMethod()
        {
            return roadStatusProcessor.GetRoadCurrentStatus(this.RequestRoadID);
        }
    }
}
