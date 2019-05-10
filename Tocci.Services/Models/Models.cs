using System;
using System.Collections.Generic;
using System.Text;

namespace Tocci.Services.Models
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
