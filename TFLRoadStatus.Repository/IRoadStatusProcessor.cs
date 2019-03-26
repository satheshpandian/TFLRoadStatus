using TFLRoadStatus.Models;

namespace TFLRoadStatus.Repository
{
    public interface IRoadStatusProcessor
    {
        NotFoundError HttpNotFoundError { get; set; }
        RoadCorridorStatus RoadCorridorStatus { get; set; }
        IPrinterClient PrinterClient { get; }
        int GetRoadCurrentStatus(string road);
    }
}