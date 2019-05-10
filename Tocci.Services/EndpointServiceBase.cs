using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.Services
{
    public abstract class EndpointServiceBase : IEndpointDataService
    {
        public abstract string InfoUrl { get; set; }
        public abstract string Name { get; }
        public abstract Services.Models.ServiceType Type { get;  }
        public abstract Task<ServiceReport> GetEndpointReport();

    }
}
