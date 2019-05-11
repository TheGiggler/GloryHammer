using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Geo;
using Grpc.Core;
using static Geo.EndpointReportingService;
using GrpcGeolocationService.Network;
using GrpcGeolocationService.Network.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using GrpcGeolocationService.Models;



namespace GrpcGeolocationService.gRPC
{
    public class GrpcServerImpl: EndpointReportingServiceBase
    {
        ServiceSettings serviceSettings;
        internal GrpcServerImpl(ServiceSettings settings)
        {
            this.serviceSettings = settings;
        }
        public override async Task<EndPointDataResponse> GetReport(EndpointDataRequest request, ServerCallContext context)
        {

            Console.WriteLine($"Request received at {DateTime.UtcNow}: {request.Endpoint} ... id={request.Id}...");
            EndPointDataResponse response = new EndPointDataResponse() { Status = Geo.Status.Failed };
            var fetch = await NetworkService.FetchEndpointData(serviceSettings.GeoServiceUrl, serviceSettings.GeoServiceApiKey, request.Endpoint);

            if (fetch.Success)
            {
                response.Status = Geo.Status.Success;
                response.Endpointdata = JsonConvert.SerializeObject(fetch.Data);
                response.Id = request.Id;
            }
            return response;

        }

        //private static string GetIpAddressFromDomain(string endpoint)
        //{
        //    IPAddress[] ips = Dns.GetHostAddresses(endpoint);
        //    //this can return multiples... just take the first for our purposes
        //    if (ips.Length > 0)
        //    {
        //        return ips[0].ToString();
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Valid domain or IP address was not submitted.");
        //    }


        //}
    }
}
