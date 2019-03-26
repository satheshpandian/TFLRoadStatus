namespace TFLRoadStatus.Models
{
    public class NotFoundError
    {
        public string timestampUtc { get; set; }
        public string exceptionType { get; set; }
        public string httpStatusCode { get; set; }
        public string httpStatus { get; set; }
        public string relativeUri { get; set; }
        public string message { get; set; }
    }
}