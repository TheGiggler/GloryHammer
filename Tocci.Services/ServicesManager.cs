using System.Collections.Generic;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.Services
{



    //class to manage the various configured and requested services, fan out requests, fan in results
    public class ServicesManager
    {
        List<EndpointServiceBase> _endpointServices; //services injected in constructor
        SummaryServiceReport _report;

        public ServicesManager(IEnumerable<EndpointServiceBase>endpointServices)
        {
            _endpointServices = new List<EndpointServiceBase>(endpointServices);
           
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

                //TODO: Allow multiple services of same time to be invoked
                var service = _endpointServices.Find(svc => svc.Type == currentServiceType);
                if (service != null)
                {
                    //var report = await service.GetEndpointReport().Result();
                    //should we await the call below?
                    var report = await service.GetEndpointReport(serviceRequest.EndpointAddress, serviceRequest.EndpointPort);
                    summary.ServiceReports.Add(report);
                    //System.Threading.Thread.Sleep(5000);
                }
            });

            return summary;

        }

    
    }


}
