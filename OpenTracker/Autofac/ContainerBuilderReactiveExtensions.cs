using System;
using System.Linq;
using System.Reflection;
using Autofac;
using ReactiveUI;

namespace OpenTracker.Autofac;

/// <summary>
/// Extends <see cref="ContainerBuilder"/> to register ReactiveUI view types.
/// </summary>
public static class ContainerBuilderReactiveExtensions
{
    public static ContainerBuilder RegisterReactiveViewTypes(this ContainerBuilder builder, Assembly assembly)
    {
        var types = assembly.GetExportedTypes();

        foreach (var type in types)
        {
            RegisterReactiveViewType(builder, type);
        }
        
        return builder;
    }

    private static void RegisterReactiveViewType(ContainerBuilder builder, Type type)
    {
        if (!type.IsAssignableTo<IViewFor>() || type.IsAbstract || type.ContainsGenericParameters)
        {
            return;
        }

        var attribute = type.GetCustomAttribute<DependencyInjectionAttribute>();

        var singleInstance = false;

        if (attribute is not null)
        {
            singleInstance = attribute.SingleInstance;
        }
        
        var viewForInterface = GetGenericViewForInterface(type);
        if (viewForInterface is null)
        {
            return;
        }
        
        if (singleInstance)
        {
            builder.RegisterType(type).As(viewForInterface).SingleInstance();
            return;
        }
        
        builder.RegisterType(type).As(viewForInterface);
    }

    private static Type? GetGenericViewForInterface(Type type)
    {
        var interfaces = type.GetInterfaces();

        return interfaces
            .FirstOrDefault(interfaceType =>
                interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IViewFor<>));
    }
}