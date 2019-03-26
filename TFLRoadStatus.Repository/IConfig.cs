namespace TFLRoadStatus.Repository
{
    public interface IConfig
    {
        string ApiUrl { get; set; }
        string AppID { get; set; }
        string AppKey { get; set; }
    }
}