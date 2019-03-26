using NUnit.Framework;
using TFLRoadStatus.Models;
using TFLRoadStatus.Repository;

namespace TFLRoadStatus.UnitTests
{
   public class PrinterClientTest
    {

            private  IPrinterClient PrinterClient;

            public PrinterClientTest()
            {
                
            }

            [Test]
            public void When_PrepareStatusMessage_Is_Executed_Returns_AddedText()
            {
                string text = "A2";
                string expectedResult = "The status of the A2 is as follows\r\n";
                PrinterClient = new PrinterClient();
            PrinterClient.PrepareStatusMessage(new RoadCorridorStatus(){displayName = text});

                var content = PrinterClient.Messages.ToString();

                Assert.AreEqual(expectedResult, content);
            }

            [Test]
            public void When_StatusSeverityConstants_statusSeverity_Is_Executed_Returns_AddedText()
            {
                string expectedResult = "Road Status";
                PrinterClient = new PrinterClient();
            var statusSeverity = PrinterClient.StatusSeverityCollection["statusSeverity"];

                Assert.AreEqual(expectedResult, statusSeverity);
            }

            [Test]
            public void When_StatusSeverityConstants_statusSeverityDescription_Is_Executed_Returns_AddedText()
            {
                string expectedResult = "Road Status Description";
                PrinterClient = new PrinterClient();
            var statusSeverityDescription = PrinterClient.StatusSeverityCollection["statusSeverityDescription"];

                Assert.AreEqual(expectedResult, statusSeverityDescription);
            }

            [Test]
            public void When_PrepareStatusMessageText_statusSeverity_Is_Executed_Returns_AddedText()
            {
                string key = "statusSeverity";
                string text = "Good";
                string expectedResult = "\tRoad Status is Good\r\n";
                PrinterClient = new PrinterClient();
            PrinterClient.AddRoadStatusInfo(key, text);

                var content = PrinterClient.Messages.ToString();

                Assert.AreEqual(expectedResult, content);
            }

            [Test]
            public void When_PrepareStatusMessageText_statusSeverityDescription_Is_Executed_Returns_AddedText()
            {
                string key = "statusSeverityDescription";
                string text = "No Exceptional Delays";
                string expectedResult = "\tRoad Status Description is No Exceptional Delays\r\n";
                PrinterClient = new PrinterClient();
            PrinterClient.AddRoadStatusInfo(key, text);

                var content = PrinterClient.Messages.ToString();

                Assert.AreEqual(expectedResult, content);
            }
        }
    }

