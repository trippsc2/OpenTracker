using System.Reflection;
using Autofac;

namespace OpenTracker.Utils.Autofac;

/// <summary>
/// Extends <see cref="ContainerBuilder"/> to register all types in the OpenTracker.Utils namespace.
/// </summary>
public static class ContainerBuilderExtensions
{
    public static ContainerBuilder RegisterOpenTrackerUtilsTypes(this ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return builder.RegisterTypesWithAttribute(assembly);
    }
}