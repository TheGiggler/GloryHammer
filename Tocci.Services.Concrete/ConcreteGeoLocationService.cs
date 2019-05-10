using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Tocci.Services.Grpc;

namespace Tocci.Services.Concrete
{
    public class ConcreteGeoLocationService:EndpointServiceBase
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
        public override async Task<ServiceReport> GetEndpointReport()
        {
            //here we'd call grpc service with as-yet-to-be created GRPC Client;
            GrpcClient.InvokeRPC(new Grpc.GrpcRequest() { ServiceAddress = ServiceAddress, ServicePort = ServicePort });
            return new ServiceReport();
        }
    }
}
