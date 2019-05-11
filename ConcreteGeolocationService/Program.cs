using System;
using System.Collections.Specialized;
using System.Configuration;
using ConcreteGeolocationService.Network;
using Nito.AsyncEx.Synchronous;

/// <summary>
/// Service to look up
/// </summary>
namespace ConcreteGeolocationService
{
    internal class ServiceSettings
    {


        public ServiceSettings(NameValueCollection settings)
        {
            this.GeoServiceApiKey = settings["GeoServiceApiKey"];
            this.GeoServiceName = settings["GeoServiceName"];
            this.GeoServiceUrl = settings["GeoServiceUrl"];
        }

        public string GeoServiceUrl { get; set; }
        public string GeoServiceName { get; set; }
        public string GeoServiceApiKey { get; set; }
    }

    class Program
    {
        static ServiceSettings settings;
        static string endpointToTest;

        static void Main(string[] args)
        {
            endpointToTest = args[0];
            //TODO test for missing
            settings = LoadSettingsFromConfig();
            Console.WriteLine("Hello World!");

            //here we'd set up grpc server to listen
            //for now, just this
            var task = NetworkService.FetchEndpointData(settings.GeoServiceUrl, settings.GeoServiceApiKey, endpointToTest);

            var result = task.WaitAndUnwrapException();
         
        }

        private static ServiceSettings LoadSettingsFromConfig()
        {
            //Would prefer "Fourth approach" here: https://www.c-sharpcorner.com/article/four-ways-to-read-configuration-setting-in-c-sharp/
            //throw new NotImplementedException();
            var settings = ConfigurationManager.GetSection("ServiceSettings") as NameValueCollection;

            return new ServiceSettings(settings);


        }
    }
}
