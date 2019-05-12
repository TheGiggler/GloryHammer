using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Grpc.Core;
using Geo;

namespace Tocci.Services.Concrete
{
    public class GeolocationServiceProxy:EndpointServiceProxyBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "Geolocation Service Proxy"; } }
        public override ServiceType Type { get { return ServiceType.Geolocation; } }
        public override int ServicePort { get; set; }

        public GeolocationServiceProxy() { }

        /// Call the grpc service
        /// </summary>
        /// <returns></returns>
        public override async Task<ServiceReport> GetEndpointReport(string endPointAddress, string reportID, int? endPointPort = null)
        {
            EndPointDataResponse response = new EndPointDataResponse(); 
            //TODO READ FROM CONFIG
            Channel channel = new Channel("127.0.0.1:7000", ChannelCredentials.Insecure);
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
