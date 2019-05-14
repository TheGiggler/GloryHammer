using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.WebAPI.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ServiceType
    {
        Geolocation = 0,
        ReverseDns = 1,
        RDAP = 2,
        Ping = 3,
        IP = 4
    }

    /// <summary>
    /// The status of an EndPointDataRequest
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
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
    public class EndpointReportRequest
    {       
        [Required]
        public string EndpointAddress { get; set; }
        int? port;
        public int EndpointPort { get { return port ?? 80; } set { port = value; } }
        //TODO
        //ADD PROTOCOL
        //[JsonConverter(typeof(StringEnumConverter))]
        public List<ServiceType> ServiceTypes { get; set; }//Was going to use the Tocci.Services.Models.ServiceType type but didn't want to expose implementation details (caller would have 
                                                      //had to use $type with type and assembly names in body of json POST in order for model to bind
                                                      //I find that annoying!

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
        public string ReportID { get; set; }
        public string EndPointAddress { get; set; }
        /// <summary>
        /// For future use
        /// </summary>
        public int EndPointPort { get; set; }
        public DateTime GeneratedDateTimeUTC { get; set; }
        public Status Status { get; set; }
        /// <summary>
        /// List of all returned data from all requested services
        /// </summary>
        public List<ServiceReport> ServiceResponses { get; set; }//really should not be using a type from Services

        public EndPointReport()
        {
            this.ReportID = Guid.NewGuid().ToString();  //lame way to generate ...would use id from db if actually persisting
        }

    }

}
