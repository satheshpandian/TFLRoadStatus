using System;
using System.Collections.Specialized;
using System.Configuration;

namespace TFLRoadStatus.Repository
{
  public  class Config:IConfig
    {
        public Config()
        {
            var settings = (NameValueCollection)ConfigurationManager.AppSettings;

            this.ApiUrl = settings["url"] ?? String.Empty;
            this.AppID = settings["app_id"] ?? String.Empty;
            this.AppKey = settings["app_key"] ?? String.Empty;
        }
        public string ApiUrl { get; set; }
        public string AppID { get; set; }
        public string AppKey { get; set; }
    }
}
