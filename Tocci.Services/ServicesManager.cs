using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Tocci.Services.Grpc;

namespace Tocci.Services
{
    public class SummaryServiceReport
    {

        public List<ServiceReport> ServiceReports = new List<ServiceReport>();

    }



    //class to manage the various configured and requested services, fan out requests, fan in results
    public class ServicesManager
    {
        List<EndpointServiceBase> _endpointServices; //services injected in constructor
        SummaryServiceReport _report;

        public ServicesManager(List<EndpointServiceBase>endpointServices)
        {
            _endpointServices = endpointServices;
            _report = new SummaryServiceReport();
        }

        public async Task<SummaryServiceReport> SendServiceRequests(List<ServiceType>serviceTypes)
        {
            Parallel.ForEach(serviceTypes, (currentServiceType) =>
            {
                //TODO TODO TODO
                //check if service type is in requested list.  if not, skip it
                //>do that here


                var service = _endpointServices.Find(svc => svc.Type == currentServiceType);
                if (service != null)
                {
                    //var report = await service.GetEndpointReport().Result();
                    //should we await the call below?
                    _report.ServiceReports.Add(service.GetEndpointReport().Result);
                }
            });

            return _report;

        }

    
    }


}
