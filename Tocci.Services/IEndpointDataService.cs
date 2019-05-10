using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tocci.Services.Models;

namespace Tocci.Services
{
    public interface IEndpointDataService
    {
        Task<ServiceReport> GetEndpointReport(string endPointAddress, int? endPointPort = null);
    }
}
