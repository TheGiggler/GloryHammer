using System;
using System.Collections.Specialized;
using System.Configuration;
using GrpcGeolocationService.Network;
using Nito.AsyncEx.Synchronous;
using GrpcGeolocationService.Models;
using Grpc.Core;
using Geo;
using GrpcGeolocationService.gRPC;
using System.Threading;
using System.Threading.Tasks;
/// <summary>
/// Service to look up
/// </summary>
namespace GrpcGeolocationService
{

    class Program
    {
        static ServiceSettings settings;
        static string endpointToTest;

        static void Main(string[] args)
        {
            //if (args.Length < 1)
            //{
            //    throw new ArgumentNullException("endpoint","Missing endpoint argument");
            //}
            //endpointToTest = args[0];
            //TODO test for missing
            settings = LoadSettingsFromConfig();


            //set up grpc server
            // Build a server
            var server = new Server
            {
                Services = { EndpointReportingService.BindService(new GrpcServerImpl(settings)) },
                Ports = { new ServerPort(settings.ListenUrl, settings.ListenPort, ServerCredentials.Insecure) }
            };

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            var serverTask = RunServiceAsync(server, tokenSource.Token);
            Console.WriteLine("GrpcLocationService listening on port " + settings.ListenPort);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            tokenSource.Cancel();
            Console.WriteLine("Shutting down...");
            serverTask.Wait();
        }

        private static ServiceSettings LoadSettingsFromConfig()
        {
            //Would prefer "Fourth approach" here: https://www.c-sharpcorner.com/article/four-ways-to-read-configuration-setting-in-c-sharp/
            //throw new NotImplementedException();
            var settings = ConfigurationManager.GetSection("ServiceSettings") as NameValueCollection;

            return new ServiceSettings(settings);


        }

        private static Task AwaitCancellation(CancellationToken token)
        {
            var taskSource = new TaskCompletionSource<bool>();
            token.Register(() => taskSource.SetResult(true));
            return taskSource.Task;
        }

        private static async Task RunServiceAsync(Server server, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Start server
            server.Start();

            await AwaitCancellation(cancellationToken);
            await server.ShutdownAsync();
        }
    }
}
