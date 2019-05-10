using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Data.Models;

namespace Tocci.Services.Concrete
{
    public class ConcreteGeoLocationService: EndpointServiceBase<IGeolocationService>
    {
        private string uri, name;
        public override string InfoUrl { get => uri; set => uri = value; }
        public override string Name { get => name; set => name=value; }

        public override async Task<object> GetEndpointData()
        {

            return null;
        }
    }
}
