using System;
using System.Collections.Generic;
using System.Text; 
using Tocci.Services.Models;

namespace Tocci.Services.Proxy.Models
{
    public class GrpcConfig
    {
        public GrpcConnectionSettings grpcConnectionSettings { get; set; }
    }
    public class GrpcConnectionSettings
    {
        public GrpcConnectionSetting[] Settings { get; set; }
    }
    public class GrpcConnectionSetting
    {
        public int ServiceType { get; set; }
        public string RemoteHostAddress { get; set; }
        public int RemoteHostPort { get; set; }
    }
}

