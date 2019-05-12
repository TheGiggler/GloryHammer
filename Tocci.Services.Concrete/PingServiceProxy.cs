using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Tocci.Services.Grpc;
using Grpc.Core;
using Ping;

namespace Tocci.Services.Proxy
{
    public class PingServiceProxy: EndpointServiceProxyBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "Ping Service Proxy"; } }
        public override ServiceType Type { get { return ServiceType.Ping; } }
        public override int ServicePort { get; set; }

        /// Call the grpc service
        /// </summary>
        /// <returns></returns>
        public override async Task<ServiceReport> GetEndpointReport(string endPointAddress, string reportID, int? endPointPort = null)
        {
            //TODO READ FROM CONFIG
            Channel channel = new Channel("127.0.0.1:8000", ChannelCredentials.Insecure);
            var client = new Ping.PingService.PingServiceClient(channel);
            var request = new EndpointDataRequest() { Endpoint = endPointAddress, Id = reportID };
            var response = client.GetReport(request);

            return new ServiceReport() { ServiceName = "Remote Ping Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.OK, ServiceType = ServiceType.Geolocation, ServiceAddress = ServiceAddress };
        }
    }
}
