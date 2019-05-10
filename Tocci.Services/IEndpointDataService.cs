using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tocci.Services
{
    public interface IEndpointDataService<T>
    {
        Task<object> GetEndpointData();
    }
}
