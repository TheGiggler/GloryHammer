using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GrpcPingService.Models;
using networkInfo = System.Net.NetworkInformation;

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
            FetchResult fetch = new FetchResult() { Success = false };
            PingData data = new PingData();

            networkInfo.Ping ping = new networkInfo.Ping();
         
            byte[] inBuffer = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());

            var reply = await ping.SendPingAsync(endpoint, 20, inBuffer);

            if (reply.Status == networkInfo.IPStatus.Success)
            {
                var buffer = System.Text.Encoding.UTF8.GetString(reply.Buffer);
                fetch.Success = true;
                data.Address = reply.Address.ToString();
                data.RoundTripTime = reply.RoundtripTime;
                data.Status = reply.Status.ToString();
                fetch.Data = data;
            }
            else
            {

                data.Status = reply.Status.ToString();
            }
            return fetch;
            
        }
    }
}
