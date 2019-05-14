
The architecture is fairly simple.

A semi-RESTful API (ASP.NET CORE) that takes in a list of services to retrieve data about a domain or IP address.

The API sends a request to the service layer which will fan out the various requests to configured enpoints where the workers are listening.  This communication is done via gRPC.

The workers are .NET Core Console Apps, each hosting a gRPC server.



Directory names do not necessarily echo the names of the types withing ... lotta refactoring!

I typically handle and log exceptions where they occur rather than letting them bubble up and possibly kill the whole process.
My practice is to hand a result class that includes information on success or failure.

I was going to use Polly to implement retry/backoff in the ServiceProxies where the make the gRPC call but decided not to hold up the entire flow due to one possibluy transient fault.  It's easy enough to submit another request.
