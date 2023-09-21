using System.Reflection;
using Autofac;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Autofac;

/// <summary>
/// Extends the <see cref="ContainerBuilder"/> to register types from the OpenTracker.Models assembly.
/// </summary>
public static class ContainerBuilderExtensions
{
    public static ContainerBuilder RegisterOpenTrackerModelsTypes(this ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return builder.RegisterTypesWithAttribute(assembly);
    }
}