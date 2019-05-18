using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.Services.Proxy
{
    public class ReverseDnsAzureFunctionProxy : EndpointServiceProxyBase
    {
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "RDAP Service Proxy"; } }
        public override ServiceType Type { get { return ServiceType.RDAP; } }
        public override int ServicePort { get; set; }

        public ReverseDnsAzureFunctionProxy()
        {
        }

        public override Task<ServiceReport> GetEndpointReport(string endPointAddress, string reportID, int? endPointPort = null)
        {
            throw new NotImplementedException();
        }


    }
}
