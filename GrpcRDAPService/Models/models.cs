using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace GrpcRDAPService.Models
{
    internal class ServiceSettings
    {


        public ServiceSettings(NameValueCollection settings)
        {

            this.RdapServiceUrl = settings["RdapServiceUrl"];
            this.ListenUrl = settings["ListenUrl"];
            int port;
            Int32.TryParse(settings["ListenPort"], out port);
            this.ListenPort = port;
        }

        public string RdapServiceUrl { get; set; }
        public string ListenUrl { get; set; }
        public int ListenPort { get; set; }
    }

}
