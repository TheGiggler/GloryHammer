using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.WebAPI.Models
{


    /// <summary>
    /// The status of an EndPointDataRequest
    /// </summary>
    public enum Status
    {
        //Everything went as planned
        Success = 0,
        //Some service(s) failed while other(s) succeeded
        Partial = 1,
        //The whole request failed
        Failed = 2       

    }
    /// <summary>
    /// A request for information about an IP address or domain
    /// </summary>
    public class EndPointReportRequest
    {       
        [Required]
        public string EndPointAddress { get; set; }

        public List<ServiceType> ServiceTypes { get; set; }

        //public EndPointDataRequest()
        //{
        //    ServiceTypes = new List<ServiceType>();
        //    EndPoint = String.Empty;
        //}

    }

    /// <summary>
    /// A response with details from the requested services
    /// </summary>
    public class EndPointReport
    {

        /// <summary>
        /// List of all returned data from all requested services
        /// </summary>
        public List<ServiceReport> ServiceResponses { get; set; }

    }

}
