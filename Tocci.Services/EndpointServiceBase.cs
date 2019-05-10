using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tocci.Services
{
    public abstract class EndpointServiceBase<T> : IEndpointDataService<T>
    {
        public abstract string InfoUrl { get; set; }
        public abstract string Name { get; set; }
        public abstract Task<object> GetEndpointData();
        //{
        //    throw new NotImplementedException();
        //}
    }
}
