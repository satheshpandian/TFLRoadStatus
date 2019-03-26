using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using TFLRoadStatus.Models;

namespace TFLRoadStatus.Repository
{
    public class RoadStatusProcessor : IRoadStatusProcessor
    {
        private readonly IApiClient _apiClient;

        public RoadStatusProcessor(IApiClient apiClient, IPrinterClient printerClient)
        {
            _apiClient = apiClient;
            PrinterClient = printerClient;
        }

        public RoadCorridorStatus RoadCorridorStatus { get; set; }
        public NotFoundError HttpNotFoundError { get; set; }
        public IPrinterClient PrinterClient { get; set; }

        public int GetRoadCurrentStatus(string roadID)
        {
            if (string.IsNullOrEmpty(roadID)) throw new ArgumentNullException();
            try
            {
                var roadStatus = _apiClient.GetResponse(roadID);

                if (!string.IsNullOrEmpty(roadStatus?.Trim()))
                {
                    if (_apiClient.StatusCode == HttpStatusCode.OK)
                    {
                        RoadCorridorStatus = JsonConvert.DeserializeObject<RoadCorridorStatus[]>(roadStatus)
                            ?.FirstOrDefault();
                        if (RoadCorridorStatus != null) PrinterClient.PrintStatusMessage(RoadCorridorStatus);

                        return 0;
                    }

                    if (_apiClient.StatusCode == HttpStatusCode.NotFound)
                    {
                        HttpNotFoundError = JsonConvert.DeserializeObject<NotFoundError>(roadStatus);
                        if (HttpNotFoundError != null) PrinterClient.PrintErrorMessage(roadID);
                        return 1;
                    }
                }
                else
                {
                    throw new InvalidDataException("No Data");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return -1;
        }
    }
}