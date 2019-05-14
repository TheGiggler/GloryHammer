using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using GrpcRDAPService.Network.Models;

namespace GrpcRDAPService.Network
{
    internal class NetworkService
    {
        internal struct FetchResult//should be in a models namespace
        {
            public bool Success {get;set;}
            public string Data { get; set; }
            public string Message { get; set; }
        }
        internal static async Task<FetchResult> FetchEndpointData(string rdapApiUrl,string endpoint)
        {
            //string endpointToTest;
            var result = new FetchResult();
            string subDirectory;

            //determine if this is ip address or domain
            //if domain, need to get ip first
            IPAddress output;

            if (IPAddress.TryParse(endpoint, out output))
            {
                subDirectory = "ip";
            }
            else
            {
                subDirectory = "domain";
            }

            var target = string.Format(rdapApiUrl,subDirectory, endpoint);
            HttpClient client = new HttpClient();

            //add Polly retries with back-off here
            //try
            //{

             var response = await client.GetAsync(target);

            //}
            //catch (Exception ex)
            //{

            //}

            if (response.IsSuccessStatusCode)
            {
                result.Success = true;
                //deserialize content into IPData
                //JsonSerializerSettings jSettings = new JsonSerializerSettings();
                //jSettings.NullValueHandling = NullValueHandling.Ignore;

                //response is in application/rdap+json doesn't seem to be parseable 
                result.Data = await response.Content.ReadAsStringAsync();
  
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                result.Success = false;
                result.Message = "Data not found. Only include domain name in request.";

            }
            else
            {
                result.Success = false;
            }

            return result;
        }

        private static string GetIpAddressFromDomain(string endpoint)
        {
            IPAddress[] ips =  Dns.GetHostAddresses(endpoint);
            //this can return multiples... just take the first for our purposes
            if (ips.Length > 0)
            {
                return ips[0].ToString();
            }
            else
            {
                throw new ArgumentException("Valid domain or IP address was not submitted.");
            }
        }
    }
}
