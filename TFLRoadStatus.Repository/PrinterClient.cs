using System;
using System.Collections;
using System.Text;
using TFLRoadStatus.Models;

namespace TFLRoadStatus.Repository
{
    public class PrinterClient : IPrinterClient
    {
        private const string constStatusSeverity = "statusSeverity";
        private const string constStatusSeverityDescription = "statusSeverityDescription";

        public PrinterClient()
        {
            StatusSeverityCollection = new Hashtable();
            Messages = new StringBuilder();
            UpdateRoadStatusConfig(constStatusSeverity, "Road Status");
            UpdateRoadStatusConfig(constStatusSeverityDescription, "Road Status Description");
        }

        public StringBuilder Messages { get; set; }

        public Hashtable StatusSeverityCollection { get; }

        public void PrintErrorMessage(string roadID)
        {
            PrepareErrorMessage(roadID);
            PrintMessage(Messages.ToString());
        }

        public void PrepareErrorMessage(string roadID)
        {
            Messages.AppendLine($"{roadID} is not a valid road");
        }


        public void PrintStatusMessage(RoadCorridorStatus roadCorridorStatus)
        {
            PrepareStatusMessage(roadCorridorStatus);
            AddRoadStatusInfo(constStatusSeverity, roadCorridorStatus.statusSeverity);
            AddRoadStatusInfo(constStatusSeverityDescription, roadCorridorStatus.statusSeverityDescription);
            PrintMessage(Messages.ToString());
        }

        public void PrepareStatusMessage(RoadCorridorStatus roadCorridorStatus)
        {
            Messages.AppendLine($"The status of the {roadCorridorStatus.displayName} is as follows");
        }

        public void AddRoadStatusInfo(string key, string value)
        {
            Messages.AppendLine($"\t{StatusSeverityCollection[key]} is {value}");
        }

        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintHelp()
        {
            Messages.Clear();
            Messages.AppendLine("Please enter:");
            Messages.AppendLine("\tRoadStatus <RoadName>");
        }

        public void UpdateRoadStatusConfig(string key, string value)
        {
            StatusSeverityCollection.Add(key, value);
        }
    }
}