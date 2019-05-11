using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Ping;
using GrpcPingService.Models;
using GrpcPingService.Network;

using Newtonsoft.Json;

namespace GrpcPingService.gRPC
{
    public class GrpcServerImpl:Ping.PingService.PingServiceBase
    {
        ServiceSettings serviceSettings;
        internal GrpcServerImpl(ServiceSettings settings)
        {
            this.serviceSettings = settings;
        }

        public async override Task<EndPointDataResponse> GetReport(EndpointDataRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Ping service ... request received at {DateTime.UtcNow}: {request.Endpoint} ... id={request.Id}...");
            EndPointDataResponse response = new EndPointDataResponse() { Status = Ping.Status.Failed };
            var fetch = await NetworkService.FetchEndpointData(request.Endpoint);

            if (fetch.Success)
            {
                response.Status = Ping.Status.Success;
                response.Endpointdata = JsonConvert.SerializeObject(fetch.Data);
                response.Id = request.Id;
            }
            return response;
        }
    }
}
