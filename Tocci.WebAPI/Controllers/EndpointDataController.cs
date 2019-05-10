using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tocci.Services;
using Tocci.WebAPI.Models;
using Tocci.Services.Models;

namespace Tocci.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/EndpointReport")]
    [ApiController]
    public class EndpointReportController : Controller
    {
        List<ServiceType> defaultServices = new List<ServiceType>() { ServiceType.Geolocation };

        ServicesManager manager;
        public EndpointReportController(List<EndpointServiceBase>endpointServices)
        {
            manager = new ServicesManager(endpointServices);
        }

        [Route("")]
        [HttpPost]        
        public async Task<ActionResult<EndPointReport>> CreateEndPointReport([FromBody] EndPointReportRequest request)
        {

            if (request.ServiceTypes == null || request.ServiceTypes.Count() == 0)
            {
                //use default
                request.ServiceTypes = defaultServices;
            }
            var result = await manager.SendServiceRequests(request.ServiceTypes);

            return GenerateEndPointReport(result) ;
        }

        private EndPointReport GenerateEndPointReport(SummaryServiceReport result)
        {
            throw new NotImplementedException();
        }
    }
} 