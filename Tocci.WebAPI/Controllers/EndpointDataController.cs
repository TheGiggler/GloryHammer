using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tocci.Services;
using Tocci.WebAPI.Models;

namespace Tocci.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/EndpointReport")]
    [ApiController]
    public class EndpointReportController : Controller
    {
        List<ServiceType> defaultServices = new List<ServiceType>() { ServiceType.Geolocation };

        ServicesManager manager;
        public EndpointReportController()
        {
            manager = new ServicesManager();
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
            var result = await manager.SendServiceRequests(defaultServices);
            return null;
        }
    }
} 