using Grpc.Core;
using RDAP;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Tocci.Services.Proxy.Models;

namespace Tocci.Services.Proxy
{
    public class RdapServiceProxy : EndpointServiceProxyBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "RDAP Service Proxy"; } }
        public override ServiceType Type { get { return ServiceType.RDAP; } }
        public override int ServicePort { get; set; }

        string grpcAddress;
        int grpcPort;
        public RdapServiceProxy(GrpcConfig config)
        {
            var setting = config.Settings.Find(s => s.ServiceType == ServiceType.RDAP);

            //TODO handle missing settings

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
            var client = new RDAP.RDAPService.RDAPServiceClient(channel);
            var request = new EndpointDataRequest() { Endpoint = endPointAddress, Id = reportID };
            try
            {
                response = client.GetReport(request);

                return new ServiceReport() { ServiceName = "Remote RDAP Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.OK, ServiceType = ServiceType.RDAP, ServiceAddress = ServiceAddress };

            }
            catch (Exception ex)
            {
                return new ServiceReport() {Message=response.Message, ServiceName = "Remote RDAP Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.Error, ServiceType = ServiceType.RDAP, ServiceAddress = ServiceAddress };

            }
        }

    }
}
