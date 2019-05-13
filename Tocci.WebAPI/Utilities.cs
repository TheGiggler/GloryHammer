using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tocci.Services;
using serviceModels = Tocci.Services.Models;
using Tocci.WebAPI.Models;

namespace Tocci.WebAPI
{
    public class Utilities
    {
        /// <summary>
        /// Map Services report to WebApi report
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static EndPointReport GenerateEndPointReport(serviceModels.SummaryServiceReport result)
        {
            return SummaryServiceReportToEndPointReport(result);
        }

        private static EndPointReport SummaryServiceReportToEndPointReport(serviceModels.SummaryServiceReport result)
        {
            EndPointReport endPointReport = new EndPointReport();

            endPointReport.EndPointAddress = result.EndPointAddress;
            endPointReport.EndPointPort = result.EndPointPort;
            endPointReport.ServiceResponses = result.ServiceReports;
            endPointReport.GeneratedDateTimeUTC = DateTime.UtcNow;

            var reportCount = result.ServiceReports.Count();
            var failed = result.ServiceReports.Where(r => r.ServiceStatus == serviceModels.ServiceStatus.Error).Count();
            var succeeded = result.ServiceReports.Where(r => r.ServiceStatus == serviceModels.ServiceStatus.OK).Count();
            var unavailable = result.ServiceReports.Where(r => r.ServiceStatus == serviceModels.ServiceStatus.Unavailable).Count();

            if (succeeded == reportCount)
            {
                endPointReport.Status = Status.Success;
            }
            else if (failed + unavailable == reportCount)
            {
                endPointReport.Status = Status.Failed; ;
            }
            else
            {
                endPointReport.Status = Status.Partial;
            }
           
            return endPointReport;

        }

        public static List<serviceModels.ServiceType> MapRequestServicesToServiceType(List<ServiceType> serviceTypes)
        {
            List<serviceModels.ServiceType> result = new List<serviceModels.ServiceType>();

            serviceTypes.ForEach(s=>result.Add(MapApiServiceTypeToServiceType(s)));
           
            return result;
        }


        private static serviceModels.ServiceType MapApiServiceTypeToServiceType(ServiceType apiType)
        {
            //this is admittedly lame ... would have to be updated every time a new service type was added
            //refactoring is a BIG TODO!
            //NOTE: the above later happened to me.  see, so lame!  Twice!

            switch (apiType)
            {
                case ServiceType.Geolocation:
                    return serviceModels.ServiceType.Geolocation;
                case ServiceType.IP:
                    return serviceModels.ServiceType.IP;
                case ServiceType.Ping:
                    return serviceModels.ServiceType.Ping;
                case ServiceType.ReverseDns:
                    return serviceModels.ServiceType.ReverseDns;
                case ServiceType.RDAP:
                    return serviceModels.ServiceType.RDAP;

                default:
                    throw new ArgumentException("Invalid ServiceType requested.");
                       



            }
        }
    }
}
