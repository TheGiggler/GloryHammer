using System;
using System.Collections.Generic;
using System.Text;

namespace Tocci.GrpcClient
{
    public class GrpcResult
    {

    }

    public class GrpcRequest
    {

    }

   

    public class GrpcClient
    {
        public class ServiceReponses
        {
            public GrpcResult InvokeRPC(GrpcRequest request)
            {
                return new GrpcResult();
            }


        }
    }
}
