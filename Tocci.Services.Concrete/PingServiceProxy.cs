using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Grpc.Core;
using Ping;
using System.Configuration;
using Tocci.Services.Proxy.Models;

namespace Tocci.Services.Proxy
{
    public class PingServiceProxy: EndpointServiceProxyBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "Ping Service Proxy"; } }
        public override ServiceType Type { get { return ServiceType.Ping; } }
        public override int ServicePort { get; set; }

        string grpcAddress;
        int grpcPort;


        public PingServiceProxy(GrpcConfig config)
        {
            var setting = config.Settings.Find(s => s.ServiceType == ServiceType.Ping);

            ////TODO handle missing settings

            grpcAddress = setting.RemoteHostAddress;
            grpcPort = setting.RemoteHostPort;

            grpcAddress += ":" + grpcPort;
        }
        /// Call the grpc service
        /// </summary>
        /// <returns></returns>
        public override async Task<ServiceReport> GetEndpointReport(string endPointAddress, string reportID, int? endPointPort = null)
        {
            EndPointDataResponse response = new EndPointDataResponse(); 
            Channel channel = new Channel(grpcAddress, ChannelCredentials.Insecure);
            var client = new Ping.PingService.PingServiceClient(channel);
            var request = new EndpointDataRequest() { Endpoint = endPointAddress, Id = reportID };
            try
            {
                response = client.GetReport(request);
                return new ServiceReport() { ServiceName = "Remote Ping Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.OK, ServiceType = ServiceType.Ping, ServiceAddress = ServiceAddress };

            }
            catch (Exception ex)
            {
                return new ServiceReport() { ServiceName = "Remote Ping Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.Error, ServiceType = ServiceType.Ping, ServiceAddress = ServiceAddress };

            }

           }
    }
}
