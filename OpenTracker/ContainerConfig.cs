using Autofac;
using OpenTracker.Models.AutoTracking.AutotrackValues;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenTracker
{
    /// <summary>
    /// This is the class for creating and configuring the Autofac container.
    /// </summary>
    public static class ContainerConfig
    {
        /// <summary>
        /// Returns a newly configured Autofac container.
        /// </summary>
        /// <returns>
        /// A new Autofac container.
        /// </returns>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            var skip = new List<string>
            {
                nameof(AutoTrackValue)
            };

            var self = new List<string>
            {

            };

            var singleInstance = new List<string>
            {

            };

            RegisterNamespace(
                Assembly.Load(nameof(OpenTracker)), builder, skip, self, singleInstance);
            RegisterNamespace(
                Assembly.Load($"{nameof(OpenTracker)}.{nameof(Models)}"), builder, skip, self,
                singleInstance);
            RegisterNamespace(
                Assembly.Load($"{nameof(OpenTracker)}.{nameof(Utils)}"), builder, skip, self,
                singleInstance);

            return builder.Build();
        }

        /// <summary>
        /// Register all assembly types.
        /// </summary>
        /// <param name="assembly">
        /// The assembly from which types will be registered.
        /// </param>
        /// <param name="builder">
        /// The container builder.
        /// </param>
        /// <param name="self">
        /// A list of strings representing types that should be registered to themselves.
        /// </param>
        /// <param name="singleInstance">
        /// A list of strings representing types that should be registered as a single instance.
        /// </param>
        private static void RegisterNamespace(
            Assembly assembly, ContainerBuilder builder, List<string> skip, List<string> self,
            List<string> singleInstance)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (skip.Contains(type.Name))
                {
                    continue;
                }

                if (self.Contains(type.Name))
                {
                    builder.RegisterType(type).AsSelf();
                    continue;
                }

                var interfaceName = $"I{type.Name}";
                var interfaceType = type.GetInterfaces().FirstOrDefault(
                    i => i.Name == interfaceName);

                if (interfaceType == null)
                {
                    continue;
                }

                if (singleInstance.Contains(type.Name))
                {
                    builder.RegisterType(type).As(interfaceType).SingleInstance();
                    continue;
                }

                builder.RegisterType(type).As(interfaceType);
            }
        }
    }
}
