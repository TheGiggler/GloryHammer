using System;
using System.Collections.Generic;
using System.Text; 
using Tocci.Services.Models;

namespace Tocci.Services.Proxy.Models
{
    public class GrpcConfig
    {
        public int tocci { get; set; }
        //public GrpcConnectionSettings GrpcConnectionSettings { get; set; } /*= new GrpcConnectionSettings();*/
        public List<GrpcConnectionSetting> Settings { get; set; } = new List<GrpcConnectionSetting>();
    }
    //public class GrpcConnectionSettings
    //{
    //    public List<GrpcConnectionSetting> GrpcConnectionSetting { get; set; } /*= new List<GrpcConnectionSetting>();*/
    //}
    public class GrpcConnectionSetting
    {
        public ServiceType ServiceType { get; set; }
        public string RemoteHostAddress { get; set; }
        public int RemoteHostPort { get; set; }
    }
}

