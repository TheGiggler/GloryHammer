
The architecture is fairly simple.

A semi-RESTful API (ASP.NET CORE) that takes in a list of services to retrieve data about a domain or IP address.

The API sends a EndpointReportRequest to the service layer's ServiceManager which will fan out the various requests to configured enpoints where the workers are listening.  This communication is done via gRPC.

The workers are .NET Core Console Apps, each hosting a gRPC server.

The API is running in Azure App Services, while the workers run on an Azure VM.

They handle their individual requests from the proxies and their responses are fanned in back into the ServiceManager and compiled into an EndPointReport, which is sent back to the requestor.

The report can be retrieved again at the URL returned in the Location header.  For the purpose of this exercise, it's only stored in memory rather than a durable store.  In production, MongoDB would be a natural store for it as will never be written to again, only read.

There is rate limiting configured in appSettings.json of the web api.

Swagger documentation is available at hostname/swagger.

Swagger JSON is at hostname/swagger/v1/swagger.json

I typically handle and log exceptions where they occur rather than letting them bubble up and possibly kill the whole process.
My practice is to hand a result class that includes information on success or failure.

I was going to use Polly to implement retry/backoff in the ServiceProxies where they make the gRPC call but decided not to hold up the entire flow due to one possibluy transient fault.  It's easy enough to submit another request.

I haven't done so here, but I find that Serilog and Seq make a nice combination for saving and accessing structured logs.


Known Issues:
Hostname with domain name will annoy RDAP service

Todos:

In the cause of resilience, wrap console apps in Windows Services.



POST
{
"EndpointAddress":"www.mlb.com",
"ServiceTypes":["Ping","Geolocation","ReverseDns","RDAP"]

}
GET (from Location header)
http://localhost:1398/api/EndpointReport?reportid=dc0563e1-6e0b-4547-88d1-be0e16352234



