using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Tocci.WebAPI.Extensions
{
    //Got this from a helpful lad at https://medium.com/agilix/asp-net-core-inject-all-instances-of-a-service-interface-64b37b43fdc8
    public static class ServiceCollectionExtensions
    {
        //public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
        //    ServiceLifetime lifetime = ServiceLifetime.Transient)
        //{
        //    var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
        //    foreach (var type in typesFromAssemblies)
        //        services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        //}

        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
    ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {

            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x =>!x.IsAbstract && x.IsSubclassOf(typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }
    }
}
