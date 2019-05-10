using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tocci.WebAPI.Models
{
    /// <summary>
    /// The services available
    /// </summary>
    public enum ServiceType
    {
        Geolocation = 0,
        ReverseDns = 1,
        RDAP = 2,
        Ping = 3
    }

    /// <summary>
    /// The status of a service
    /// </summary>
    public enum ServiceStatus
    {
        OK = 0,
        Error = 1,
        Unavailable = 2
    }

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
    public class EndPointDataRequest
    {       
        public string EndPoint { get; set; }
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
    public class EndPointDataResponse
    {
        /// <summary>
        /// If the service has ab address of some sort, this is it
        /// </summary>
        public string EndPointAddress { get; set; }
        /// <summary>
        /// List of all returned data from all requested services
        /// </summary>
        public List<ServiceResponse> ServiceResponses { get; set; }


    }
    /// <summary>
    /// 
    /// </summary>
    public struct ServiceResponse
    {
        public ServiceType ServiceType { get; set; }
        public string ServiceUrl { get; set; }
        public string ServiceName { get; set; }
        public string Data { get; set; }
        public ServiceStatus ServiceStatus { get; set; }

    }
}
