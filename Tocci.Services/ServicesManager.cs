using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.Services
{



    //class to manage the various configured and requested services, fan out requests, fan in results
    public class ServicesManager
    {
        List<EndpointServiceProxyBase> _endpointServices; //services injected in constructor
        SummaryServiceReport _report;

        public ServicesManager(IEnumerable<EndpointServiceProxyBase>endpointServices)
        {
            _endpointServices = new List<EndpointServiceProxyBase>(endpointServices);
           
            _report = new SummaryServiceReport();
        }

        public async Task<SummaryServiceReport> SendServiceRequests(ServiceRequest serviceRequest)
        {
            SummaryServiceReport summary = new SummaryServiceReport() { EndPointAddress = serviceRequest.EndpointAddress, EndPointPort = serviceRequest.EndpointPort };

            Parallel.ForEach(serviceRequest.ServiceTypes, async(currentServiceType) =>
            {
                //TODO TODO TODO
                //check if service type is in requested list.  if not, skip it
                //>do that here

                //create id for correlation
                var id = Guid.NewGuid().ToString();
               
                //TODO: Allow multiple services of same time to be invoked
                var service = _endpointServices.Find(svc => svc.Type == currentServiceType);
                if (service != null)
                {
                    //var report = await service.GetEndpointReport().Result();
                    //should we await the call below?
                    var report = await service.GetEndpointReport(serviceRequest.EndpointAddress,id,serviceRequest.EndpointPort);
                    summary.ServiceReports.Add(report);
                    //System.Threading.Thread.Sleep(5000);
                }
            });

            return summary;

        }

    
    }


}
