using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gmtl.Components.Web
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Register all types implementing IApiComponent
        /// Except those defined explicitely. Those are registered as 'self'. Useful for 'general' components that use other compoments to build all-in-one statuses
        /// </summary>
        public static void RegisterAvailableImplementationsOfIComponent(this IServiceCollection services, params Type[] registerAsSelf)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var interfaceType = typeof(IComponent);
            var implementations = assembly.GetTypes()
                                          .Where(type => interfaceType.IsAssignableFrom(type) 
                                          && type.IsClass
                                          && !type.IsAbstract
                                          && !registerAsSelf.Contains(type));

            foreach (var implementation in implementations)
            {
                services.AddTransient(interfaceType, implementation);
            }

            foreach (var service in registerAsSelf)
            {
                services.AddTransient(service);
            }
        }

        /// <summary>
        /// Register already registered components as IComponent. 
        /// Useful if you dont want to scan all assemblies and add every class
        /// use as the last command before var app = builder.Build();
        /// </summary>
        public static void RegisterIComponentsInfos(this IServiceCollection services, List<Assembly> assembliesToScan, List<Type> registerAsSelf)
        {
            var interfaceType = typeof(IComponent);
            var components = assembliesToScan.SelectMany(a => a.GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !registerAsSelf.Any(t2 => t == t2))
                .ToList()).ToList();

            foreach (var component in components)
            {
                if (!services.Any(s => s.ServiceType == interfaceType && s.ImplementationType == component))
                {
                    services.AddScoped(interfaceType, component);
                }
            }

            foreach (var serviceType in registerAsSelf)
            {
                services.AddTransient(serviceType);
            }
        }
    }
}