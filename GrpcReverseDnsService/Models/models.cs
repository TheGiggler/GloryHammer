using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcReverseDnsService.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;


    internal class ServiceSettings
    {


        public ServiceSettings(NameValueCollection settings)
        {
            this.GeoServiceApiKey = settings["GeoServiceApiKey"];
            this.GeoServiceName = settings["GeoServiceName"];
            this.GeoServiceUrl = settings["GeoServiceUrl"];
            this.ListenUrl = settings["ListenUrl"];
            int port;
            Int32.TryParse(settings["ListenPort"], out port);
            this.ListenPort = port;
        }

        public string GeoServiceUrl { get; set; }
        public string GeoServiceName { get; set; }
        public string GeoServiceApiKey { get; set; }
        public string ListenUrl { get; set; }
        public int ListenPort { get; set; }
    }



}



