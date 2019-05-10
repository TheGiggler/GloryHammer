using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tocci.Services;
using Tocci.WebAPI.Models;
using serviceModels = Tocci.Services.Models;

namespace Tocci.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/EndpointReport")]
    [ApiController]
    public class EndpointReportController : Controller
    {
        List<serviceModels.ServiceType> defaultServices = new List<serviceModels.ServiceType>() { serviceModels.ServiceType.Geolocation, serviceModels.ServiceType.IP };

        ServicesManager manager;
        public EndpointReportController(IEnumerable<EndpointServiceBase>endpointServices)
        {
            manager = new ServicesManager(endpointServices);
        }

        [Route("")]
        [HttpPost]        
        public async Task<ActionResult<EndPointReport>> CreateEndPointReport([FromBody] EndPointReportRequest request)
        {
            List<serviceModels.ServiceType> services = new List<serviceModels.ServiceType>();

            if (request.ServiceTypes == null || request.ServiceTypes.Count() == 0)
            {
                //use default
                services = defaultServices;
            }
            else
            {
                //services = MapRequestServicestoServiceType(request.ServiceTypes);
                services = defaultServices;
            }
            var result = await manager.SendServiceRequests(services);

            return Utilities.GenerateEndPointReport(result) ;
        }




    }
} 