using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using ReverseDns;
using GrpcReverseDnsService.Models;
using GrpcReverseDnsService.Network;

using Newtonsoft.Json;

namespace GrpcReverseDns.gRPC
{
    public class GrpcServerImpl:ReverseDnsService.ReverseDnsServiceBase
    {
        ServiceSettings serviceSettings;
        internal GrpcServerImpl(ServiceSettings settings)
        {
            this.serviceSettings = settings;
        }

        public async override Task<EndPointDataResponse> GetReport(EndpointDataRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Ping service ... request received at {DateTime.UtcNow}: {request.Endpoint} ... id={request.Id}...");
            EndPointDataResponse response = new EndPointDataResponse() { Status = ReverseDns.Status.Failed };
            var fetch = await NetworkService.FetchEndpointData(request.Endpoint);

            if (fetch.Success)
            {
                response.Status = ReverseDns.Status.Success;
                response.Endpointdata = JsonConvert.SerializeObject(fetch.Data);
                response.Id = request.Id;
            }
            return response;
        }
    }
}
