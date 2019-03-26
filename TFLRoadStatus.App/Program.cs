using System;
using System.Linq;
using System.Net.Http;
using StructureMap;
using TFLRoadStatus.Repository;

namespace TFLRoadStatus.App
{
    internal class Program
    {
        private static Container container = null;
        static int Main(string[] args)
        {
            container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                });
                _.For<IRoadStatusProcessor>().Use<RoadStatusProcessor>();
                _.For<IApiClient>().Use<ApiClient>();
                _.For<IPrinterClient>().Use<PrinterClient>();
                _.For<IConfig>().Use<Config>();
                _.For<HttpClient>().Use(new HttpClient());
            });
            IRoadStatusProcessor app = container.GetInstance<IRoadStatusProcessor>(); 

            string parameter;

            if ((parameter = ParseArgs(args)) != null)
            {
                return app.GetRoadCurrentStatus(parameter);
            }

            Console.ReadLine();
            return -1;
        }

        private static string ParseArgs(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                return args.First();
            }

            PrintHelp();

            return null;
        }

        private static void PrintHelp()
        {
            var printerClient = container.GetInstance<IPrinterClient>();
            printerClient.PrintHelp();
        }
    }
}