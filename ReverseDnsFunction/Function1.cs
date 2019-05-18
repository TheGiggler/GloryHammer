
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using ReverseDnsFunction.Network;
using static ReverseDnsFunction.Network.NetworkService;
using ReverseDnsFunction.Models;

namespace ReverseDnsFunction
{
    public static class Function1
    {
        [FunctionName("GetReverseDns")]
        public async static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string ip = req.Query["ip"];

            var result = FetchEndpointData(ip);

            return new CreatedResult("myLocationUrl",result);

           
        }

        internal static async Task<NetworkService.FetchResult> FetchEndpointData(string endpoint)
        {
            FetchResult fetch = new FetchResult() { Success = false };

            //this is only valid for IP Addresses
            IPAddress addr;
            if (!IPAddress.TryParse(endpoint, out addr))
            {
                fetch.Message = $"Endpoint {endpoint} is not valid for this service.  Must be an IP address";
                fetch.Success = false;
                return fetch;

            }

            IPHostEntry hosts = Dns.GetHostEntry(addr);

            fetch.Data = new ReverseDnsData(hosts);
            fetch.Success = true;
            return fetch;

        }
    }
}
