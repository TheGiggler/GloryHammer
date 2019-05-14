using Microsoft.Extensions.Logging;
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
        ILogger<ServicesManager> _logger;
        public ServicesManager(IEnumerable<EndpointServiceProxyBase>endpointServices)
        {
            _endpointServices = new List<EndpointServiceProxyBase>(endpointServices);
           // _logger = logger;
            _report = new SummaryServiceReport();
        }

        public async Task<SummaryServiceReport> SendServiceRequests(ServiceRequest serviceRequest, ILogger logger)
        {
            //create id for correlationid/reportid
            var id = Guid.NewGuid().ToString();
            SummaryServiceReport summary = new SummaryServiceReport() { EndPointAddress = serviceRequest.EndpointAddress, EndPointPort = serviceRequest.EndpointPort };

            Parallel.ForEach(serviceRequest.ServiceTypes, async(currentServiceType) =>
            {
                logger.LogInformation($"Preparing service requests for reportid{id}");
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

            logger.LogInformation($"Completed service requests for reportid {id}");
            return summary;

        }

    
    }


}
