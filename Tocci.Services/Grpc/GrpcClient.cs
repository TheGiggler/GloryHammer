using System;
using System.Collections.Generic;
using System.Text;

namespace Tocci.Services.Grpc
{
    public class GrpcResult
    {

    }

    public class GrpcRequest
    {
        public string ServiceAddress { get; set; }
        public string ServicePort { get; set; }
    }

   

    public class GrpcClient
    {
        public class ServiceReponses
        {



        }

        public static GrpcResult InvokeRPC(GrpcRequest request)
        {
            return new GrpcResult();
        }
    }
}
