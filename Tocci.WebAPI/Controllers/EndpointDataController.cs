using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tocci.Services;
using Tocci.WebAPI.Models;
using serviceModels = Tocci.Services.Models;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Tocci.WebAPI.Controllers
{


    [Produces("application/json")]
    [Route("api/EndpointReport")]
    [ApiController]
    public class EndpointReportController : Controller
    {
        //fake persistence
        static List<EndPointReport> Reports = new List<EndPointReport>();
        List<serviceModels.ServiceType> defaultServices = new List<serviceModels.ServiceType>() { serviceModels.ServiceType.Geolocation, serviceModels.ServiceType.IP };
        ILogger _logger;
        ServicesManager manager;
        string _hostNameAndPort;          
        public EndpointReportController(IEnumerable<EndpointServiceProxyBase> endpointServices, IConfiguration config, ILogger logger)
        {
            manager = new ServicesManager(endpointServices);
            _hostNameAndPort = (string)config.GetValue(typeof(string), "HostNameAndPort");
            _logger = logger;
        }

        /// <summary>
        /// A method to retrieve a stored report by its id
        /// </summary>
        /// <param name="endpointAddress"></param>
        /// <param name="endPointPort"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(EndPointReport))]
        [ProducesResponseType(400, Type = typeof(void))]
        [ProducesResponseType(404, Type = typeof(void))]
        //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]//set cache much longer as this would be in db and would never change
        public async Task<ActionResult<EndPointReport>> GetEndPointReport(string reportID)
        {
            var report = Reports.FirstOrDefault(r => r.ReportID == reportID);

            if (report == null)
            {
                NotFound(typeof(void));
            }

            return Ok(report);
        }

        /// <summary>
        /// This is a POST because we are creating a resource, an EndpointReport
        /// This could presumably be stored in a db and retrieved by a reportid and or a list by id and/or time range with a GET
        /// I might do this with mongo if time allows
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(EndPointReport))]
        [ProducesResponseType(400, Type = typeof(void))]
        public async Task<ActionResult<EndPointReport>> CreateEndPointReport([FromBody] EndpointReportRequest request)
        {
            List<serviceModels.ServiceType> services = new List<serviceModels.ServiceType>();
            var servicesRequest = new serviceModels.ServiceRequest();

            if (request.ServiceTypes == null || request.ServiceTypes.Count() == 0)
            {
                //use default
                services = defaultServices;
            }
            else
            {
                try
                {
                    services = Utilities.MapRequestServicesToServiceType(request.ServiceTypes);
                }
                catch (ArgumentException argEx)//will occur if bad service is passed.  probably could just skip it and keep going
                {
                    return new BadRequestResult();

                }
                //services = defaultServices;
            }

            servicesRequest.ServiceTypes = services;
            servicesRequest.EndpointAddress = request.EndpointAddress;
            servicesRequest.EndpointPort = request.EndpointPort;
            //var serviceRequest = new Models.EndPointReportRequest() { EndPointAddress = request.EndPointAddress, ServiceTypes = endpointServicesRequested };
            var result = await manager.SendServiceRequests(servicesRequest,_logger);

            var report=  Utilities.GenerateEndPointReport(result);
            //save to fake db
            Reports.Add(report);
            return Created($"{_hostNameAndPort}/api/EndpointReport?{report.ReportID}",report);

        }




    }
} 