using System.Collections;
using System.Text;
using TFLRoadStatus.Models;

namespace TFLRoadStatus.Repository
{
    public interface IPrinterClient
    {
        Hashtable StatusSeverityCollection { get; }
        StringBuilder Messages { get; set; }
        void PrintStatusMessage(RoadCorridorStatus roadCorridorStatus);
        void PrepareStatusMessage(RoadCorridorStatus roadCorridorStatus);

        void PrintErrorMessage(string roadID);
        void PrepareErrorMessage(string roadID);
        void AddRoadStatusInfo(string key, string value);
        void PrintMessage(string message);
        void PrintHelp();
    }
}