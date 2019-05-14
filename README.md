
The architecture is fairly simple.

A semi-RESTful API (ASP.NET CORE) that takes in a list of services to retrieve data about a domain or IP address.

The API sends a EndpointReportRequest to the service layer's ServiceManager which will fan out the various requests to configured enpoints where the workers are listening.  This communication is done via gRPC.

The workers are .NET Core Console Apps, each hosting a gRPC server.

The API is running in Azure App Services, while the workers run on an Azure VM.

They handle their individual requests from the proxies and their responses are fanned in back into the ServiceManager and compiled into an EndPointReport, which is sent back to the requestor.

Because the return data from the various services was not heterogenous, the data returned for the various services will have different shapes.

The report can be retrieved again at the URL returned in the Location header.  For the purpose of this exercise, it's only stored in memory rather than a durable store.  In production, MongoDB would be a natural store for it as will never be written to again, only read.

There is rate limiting configured in appSettings.json of the web api.  You'll see some 429's if you POST fast enough.

Swagger documentation is available at https://tocciwebapi.azurewebsites.net/swagger/index.html.

Swagger JSON is at https://tocciwebapi.azurewebsites.net/swagger/v1/swagger.json

A sample POST request, with the valid ServiceType enums:
{
"EndpointAddress":"mlb.com",
"ServiceTypes":["ReverseDns","RDAP","PING","Geolocation"]
}

A successful request will return a 201 with the EndPointReport that was generated..  Some don't like to send the created resource in the reponse to a POST, but I've got no problem with it!

There's also a Location header with the Uri of the report, which can be used to GET it (as long as it stays in memory).

I typically handle and log exceptions where they occur rather than letting them bubble up and possibly kill the whole process.
My practice is to return a typed result object that includes information on success or failure.

I was going to use Polly to implement retry/backoff in the ServiceProxies where they make the gRPC call but decided not to hold up the entire flow due to one possibluy transient fault.  It's easy enough to submit another request.

I haven't done so here, but I find that Serilog and Seq make a nice combination for saving and accessing structured logs in applications of this size.



Known Issues:
Hostname with domain name will annoy RDAP service

Todos:
In the cause of resilience, wrap console apps in Windows Services.
The models for the various service outputs could use a base class.
Add structured logging with Serilog and Seq.







