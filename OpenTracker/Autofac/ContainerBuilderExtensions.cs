using System.Reflection;
using Autofac;

namespace OpenTracker.Autofac;

/// <summary>
/// Extends <see cref="ContainerBuilder"/> to register types from the OpenTracker assembly.
/// </summary>
public static class ContainerBuilderExtensions
{
    public static ContainerBuilder RegisterOpenTrackerTypes(this ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        builder
            .ConfigureAvalonia()
            .RegisterTypesWithAttribute(assembly)
            .RegisterReactiveViewTypes(assembly);
        
        return builder;
    }
}