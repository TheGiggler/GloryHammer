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
    [Route("api/EndpointData")]
    public class EndpointDataController : Controller
    {
        List<ServiceType> defaultServices = new List<ServiceType>() { ServiceType.Geolocation, ServiceType.Ping };

        ServicesManager manager;
        public EndpointDataController()
        {
            manager = new ServicesManager();
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<EndPointDataResponse>> GetEndPointData(EndPointDataRequest request)
        {

            if (request.ServiceTypes == null || request.ServiceTypes.Count() == 0)
            {
                //use default
                request.ServiceTypes = defaultServices;
            }
            var result = await manager.SendServiceRequests(null);
            return null;
        }
    }
} 