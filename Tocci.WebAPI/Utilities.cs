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

            switch (apiType)
            {
                case ServiceType.Geolocation:
                    return serviceModels.ServiceType.Geolocation;
                case ServiceType.IP:
                    return serviceModels.ServiceType.IP;
                default:
                    throw new ArgumentException("Invalid ServiceType requested.");
                       



            }
        }
    }
}
