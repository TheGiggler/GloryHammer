using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tocci.Services
{
    public class ServiceReponses
    {
        public List<object> Responses = new List<object>();

    }



    //class to manage the various configured and requested services, fan out requests, fan in results
    public class ServicesManager
    {
        ServiceReponses response;

        public ServicesManager()
        {
            response = new ServiceReponses();
        }

        public async Task<object> SendServiceRequests(List<IEndpointDataService<object>> services)
        {
            Parallel.ForEach(services, (currentService) =>
            {
                response.Responses.Add(currentService.GetEndpointData());
            });

            return response;

        }

    
    }


}
