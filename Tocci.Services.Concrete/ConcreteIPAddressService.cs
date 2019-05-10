using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Tocci.Services.Grpc;
namespace Tocci.Services.Concrete
{
    public class ConcreteIPAddressService: EndpointServiceBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "Concrete Geolocation Service"; } }
        public override ServiceType Type { get { return ServiceType.IP; } }
        public override int ServicePort { get; set; }

        public ConcreteIPAddressService() { }

        /// Call the grpc service
        /// </summary>
        /// <returns></returns>
        public override async Task<ServiceReport> GetEndpointReport(string endPointAddress, int? endPointPort = null)
        {
            //fake latency
            //System.Threading.Thread.Sleep(5000);
            //todo: read grpc params from config file
            //here we'd call grpc service with as-yet-to-be created GRPC Client;
            // GrpcClient.InvokeRPC(new Grpc.GrpcRequest() { ServiceAddress = ServiceAddress, ServicePort = ServicePort });
            return new ServiceReport() {ServiceName = "Some Web IP service", Data = "some IP data", ServiceStatus = ServiceStatus.OK, ServiceType = ServiceType.IP, ServiceAddress = ServiceAddress };
        }
    }
}
