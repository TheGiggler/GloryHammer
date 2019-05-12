using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GrpcReverseDnsService.Models;
using networkInfo = System.Net.NetworkInformation;
using System.Net;

namespace GrpcReverseDnsService.Network
{
    public class NetworkService
    {
        internal struct FetchResult//should be in a models namespace
        {
            public bool Success { get; set; }
            public ReverseDnsData Data { get; set; }
          
        }

        internal static async Task<FetchResult> FetchEndpointData(string endpoint)
        {
            FetchResult fetch = new FetchResult() { Success = false };

            IPHostEntry hosts = Dns.GetHostEntry(endpoint);

            fetch.Data = new ReverseDnsData(hosts);
            fetch.Success = true;
            return fetch;
            
        }
    }
}
