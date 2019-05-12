using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcPingService.Models
{
    public class PingData
    {
        public string Address { get; set; }
        public string Status { get; set; }
        public long RoundTripTime { get; set; }


    }
}
