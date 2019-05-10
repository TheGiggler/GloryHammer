using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.Services.Concrete
{
    public class ConcreteGeoLocationService:EndpointServiceBase
    {
        private string uri, name;
        public override string InfoUrl { get; set; }
        public override string Name { get { return "Concrete Geolocation Service"; } }
        public override ServiceType Type { get { return ServiceType.Geolocation; } }

        /// <summary>
        /// Call the grpc service
        /// </summary>
        /// <returns></returns>
        public override async Task<ServiceReport> GetEndpointReport()
        {

            return new ServiceReport();
        }
    }
}
