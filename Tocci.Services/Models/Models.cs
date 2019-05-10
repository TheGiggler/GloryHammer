using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// I prefer that the API and whatever services it uses only share types when passing data between each other
/// For instance, the request to the api should not include a type from the Services library, rather it own
/// The response to the caller should return a type from the api rather than from the Services
/// This stems from my REST experiences (Resources, Representations), though I hesitate to call this a RESTful API!
/// This requires mappings back and forth, which should be fairly fast
/// </summary>
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
        Ping = 3,
        IP = 4
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
    public struct ServiceReport
    {
        public ServiceType ServiceType { get; set; }
        public string ServiceAddress { get; set; }
        public string ServiceName { get; set; }
        public string Data { get; set; }
        public ServiceStatus ServiceStatus { get; set; }

    }

    public class GrpcResult
    {

    }

    public class GrpcRequest
    {

    }


}
