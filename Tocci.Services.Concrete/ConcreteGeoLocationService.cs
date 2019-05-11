using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Tocci.Services.Grpc;
using Grpc.Core;
using Geo;

namespace Tocci.Services.Concrete
{
    public class ConcreteGeoLocationService:EndpointServiceProxyBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "Concrete Geolocation Service"; } }
        public override ServiceType Type { get { return ServiceType.Geolocation; } }
        public override int ServicePort { get; set; }

        public ConcreteGeoLocationService() { }

        /// Call the grpc service
        /// </summary>
        /// <returns></returns>
        public override async Task<ServiceReport> GetEndpointReport(string endPointAddress, string reportID, int? endPointPort = null)
        {
            //TODO READ FROM CONFIG
            Channel channel = new Channel("127.0.0.1:7000", ChannelCredentials.Insecure);
            var client = new Geo.EndpointReportingService.EndpointReportingServiceClient(channel);
            EndpointDataRequest request = new EndpointDataRequest() { Endpoint = endPointAddress, Id = reportID };
            var response = client.GetReport(request);
            ////fake latency
            //System.Threading.Thread.Sleep(5000);
            //here we'd call grpc service with as-yet-to-be created GRPC Client;
            // GrpcClient.InvokeRPC(new Grpc.GrpcRequest() { ServiceAddress = ServiceAddress, ServicePort = ServicePort });
            return new ServiceReport() { ServiceName = "Some Web geolocation service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.OK, ServiceType = ServiceType.Geolocation, ServiceAddress = ServiceAddress };
        }
    }
}
