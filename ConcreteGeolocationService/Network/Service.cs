using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace ConcreteGeolocationService.Network
{
    internal class NetworkService
    {
        internal struct FetchResult//should be in a models namespace
        {
            public bool Success {get;set;}
            public IPData Data { get; set; }
        }
        internal static async Task<FetchResult> FetchEndpointData(string geoApiUrl,string apiKey, string endpoint)
        {
            string endpointToTest;
            var result = new FetchResult();

            //determine if this is ip address or domain
            //if domain, need to get ip first
            IPAddress output;

            if (!IPAddress.TryParse(endpoint, out output))
            {
                endpointToTest = GetIpAddressFromDomain(endpoint);
            }
            else
            {
                endpointToTest = endpoint;
            }

            var target = string.Format(geoApiUrl, endpointToTest, apiKey);
            HttpClient client = new HttpClient();

            //add Polly retries with back-off here
            var response = await client.GetAsync(target);

            if (response.IsSuccessStatusCode)
            {
                result.Success = true;
                //deserialize content into IPData
                JsonSerializerSettings jSettings = new JsonSerializerSettings();
                jSettings.NullValueHandling = NullValueHandling.Ignore;
                result.Data= JsonConvert.DeserializeObject<IPData>(response.Content.ReadAsStringAsync().Result,jSettings);
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
