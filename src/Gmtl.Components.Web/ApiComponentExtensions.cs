using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Gmtl.Components.Web
{
    public static class ApiComponentExtensions
    {
        /// <summary>
        /// Register all types implementing IApiComponent
        /// Except those defined explicitely. Those are registered as 'self'. Useful for 'general' components that use other compoments to build all-in-one statuses
        /// </summary>
        public static void RegisterAllImplementationsOfIApiComponent(this IServiceCollection services, params Type[] registerAsSelf)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var interfaceType = typeof(IApiComponent);
            var implementations = assembly.GetTypes()
                                          .Where(type => interfaceType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract && !registerAsSelf.Contains(type));

            foreach (var implementation in implementations)
            {
                services.AddTransient(interfaceType, implementation);
            }
            foreach (var service in registerAsSelf)
            {
                services.AddTransient(service);
            }
        }
    }
}