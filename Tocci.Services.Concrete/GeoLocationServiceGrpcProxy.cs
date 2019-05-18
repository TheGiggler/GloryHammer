using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Grpc.Core;
using Geo;
using Tocci.Services.Proxy.Models;

namespace Tocci.Services.Concrete
{
    public class GeolocationServiceGrpcProxy:EndpointServiceProxyBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "Geolocation Service Proxy"; } }
        public override ServiceType Type { get { return ServiceType.Geolocation; } }
        public override int ServicePort { get; set; }

        string grpcAddress;
        int grpcPort;

        public GeolocationServiceGrpcProxy() { }

        public GeolocationServiceGrpcProxy(GrpcConfig config)
        {
            var setting = config.Settings.Find(s => s.ServiceType == ServiceType.Geolocation);

            ////TODO handle missing settings

            grpcAddress = setting.RemoteHostAddress;
            grpcPort = setting.RemoteHostPort;

            //grpcAddress += ":" + grpcPort;
        }

        /// Call the grpc service
        /// </summary>
        /// <returns></returns>
        public override async Task<ServiceReport> GetEndpointReport(string endPointAddress, string reportID, int? endPointPort = null)
        {
            EndPointDataResponse response = new EndPointDataResponse(); 
            //TODO READ FROM CONFIG
            Channel channel = new Channel(grpcAddress,grpcPort,ChannelCredentials.Insecure);
            var client = new Geo.EndpointReportingService.EndpointReportingServiceClient(channel);
            EndpointDataRequest request = new EndpointDataRequest() { Endpoint = endPointAddress, Id = reportID };
            try
            {
                response = client.GetReport(request);
                return new ServiceReport() { ServiceName = "Remote Geolocation Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.OK, ServiceType = ServiceType.Geolocation, ServiceAddress = ServiceAddress };

            }
            catch (Exception ex)
            {
                return new ServiceReport() { ServiceName = "Remote Geolocation Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.Error, ServiceType = ServiceType.Geolocation, ServiceAddress = ServiceAddress };

            }
           }
    }
}
