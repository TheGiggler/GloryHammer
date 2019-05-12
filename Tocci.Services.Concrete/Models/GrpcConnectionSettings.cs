using System;
using System.Collections.Generic;
using System.Text; 
using Tocci.Services.Models;

namespace Tocci.Services.Proxy.Models
{
    public class GrpcConnectionSettings
    {
        public List<GrpcConnectionSetting> Settings { get; set; }
    }
    public class GrpcConnectionSetting
    {
        public ServiceType ServiceType { get; set; }
        public string RemoteHostAddress { get; set; }
        public int RemoteHostPort { get; set; }
    }
}

