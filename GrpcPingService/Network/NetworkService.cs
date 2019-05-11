using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GrpcPingService.Models;

namespace GrpcPingService.Network
{
    public class NetworkService
    {
        internal struct FetchResult//should be in a models namespace
        {
            public bool Success { get; set; }
            public PingData Data { get; set; }
        }

        internal static async Task<FetchResult> FetchEndpointData(string endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
