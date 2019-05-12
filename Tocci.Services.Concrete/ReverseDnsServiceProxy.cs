﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;
using Grpc.Core;
using ReverseDns;

namespace Tocci.Services.Proxy
{
    public class ReverseDnsServiceProxy : EndpointServiceProxyBase
    {
        private string uri, name;
        public override string ServiceAddress { get; set; }
        public override string Name { get { return "Reverse DNS Service Proxy"; } }
        public override ServiceType Type { get { return ServiceType.ReverseDns; } }
        public override int ServicePort { get; set; }

        public override async Task<ServiceReport> GetEndpointReport(string endPointAddress, string reportID, int? endPointPort = null)
        {
            EndPointDataResponse response = new EndPointDataResponse(); ;
            //TODO READ FROM CONFIG
            Channel channel = new Channel("127.0.0.1:9000", ChannelCredentials.Insecure);
            var client = new ReverseDnsService.ReverseDnsServiceClient(channel);
            EndpointDataRequest request = new EndpointDataRequest() { Endpoint = endPointAddress, Id = reportID };
            try
            {
                response = client.GetReport(request);

                return new ServiceReport() { ServiceName = "Remote ReverseDns Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.OK, ServiceType = ServiceType.ReverseDns, ServiceAddress = ServiceAddress };
            }
            catch (Exception ex)
            {
                return new ServiceReport() { ServiceName = "Remote ReverseDns Service", Data = response.Endpointdata, ServiceStatus = ServiceStatus.Error, ServiceType = ServiceType.ReverseDns, ServiceAddress = ServiceAddress };

            }
        }
    }
}