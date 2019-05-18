using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReverseDnsFunction.Models;
using networkInfo = System.Net.NetworkInformation;
using System.Net;

namespace ReverseDnsFunction.Network
{
    public class NetworkService
    {
        internal struct FetchResult//should be in a models namespace
        {
            public bool Success { get; set; }
            public ReverseDnsData Data { get; set; }
            public string Message { get; set; }
          
        }

        internal static async Task<FetchResult> FetchEndpointData(string endpoint)
        {
            FetchResult fetch = new FetchResult() { Success = false };

            //this is only valid for IP Addresses
            IPAddress addr;
            if (!IPAddress.TryParse(endpoint, out addr))
            {
                fetch.Message = $"Endpoint {endpoint} is not valid for this service.  Must be an IP address";
                fetch.Success = false;
                return fetch;

            }

            IPHostEntry hosts = Dns.GetHostEntry(addr);

            fetch.Data = new ReverseDnsData(hosts);
            fetch.Success = true;
            return fetch;
            
        }
    }
}
