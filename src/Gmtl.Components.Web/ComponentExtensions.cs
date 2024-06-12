using Microsoft.Extensions.DependencyInjection;
using System;
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
        /// <param name="services"></param>
        /// <param name="registerAsSelf"></param>
        public static void RegisterRegiestredImplementationsOfIComponent(this IServiceCollection services, params Type[] registerAsSelf)
        {
            var interfaceType = typeof(IComponent);

            var implementations = services.Where(type =>
                type.ImplementationType != null
                && interfaceType.IsAssignableFrom(type.ImplementationType)
                && type.ImplementationType.IsClass
                && !type.ImplementationType.IsAbstract
                && !registerAsSelf.Contains(type.ImplementationType)).ToList();

            foreach (var implementation in implementations)
            {
                services.AddTransient(interfaceType, implementation.ImplementationType);
            }

            foreach (var service in registerAsSelf)
            {
                services.AddTransient(service);
            }
        }
    }
}