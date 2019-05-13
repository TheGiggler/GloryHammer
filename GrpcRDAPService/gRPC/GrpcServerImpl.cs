using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcRDAPService.Models;
using GrpcRDAPService.Network;
using RDAP;
namespace GrpcRDAPService.gRPC
{
    public class GrpcServerImpl:RDAP.RDAPService.RDAPServiceBase
    {
        ServiceSettings serviceSettings;
        internal GrpcServerImpl(ServiceSettings settings)
        {
            this.serviceSettings = settings;
        }


        public async override Task<EndPointDataResponse> GetReport(EndpointDataRequest request, ServerCallContext context)
        {
            Console.WriteLine($"RDAP service ... request received at {DateTime.UtcNow}: {request.Endpoint} ... id={request.Id}...");
            EndPointDataResponse response = new EndPointDataResponse() { Status = RDAP.Status.Failed };
            var fetch = await NetworkService.FetchEndpointData(serviceSettings.RdapServiceUrl,request.Endpoint);

            if (fetch.Success)
            {
                response.Status = RDAP.Status.Success;
                response.Endpointdata = fetch.Data;
                response.Id = request.Id;
            }
            return response;
        }
    }
}
