using System;
using System.Linq;
using System.Reflection;
using Autofac;

namespace OpenTracker.Autofac;

/// <summary>
/// Extends <see cref="ContainerBuilder"/> to register types with the <see cref="DependencyInjectionAttribute"/>
/// attribute.
/// </summary>
public static class ContainerBuilderAutoRegisterExtensions
{
    public static ContainerBuilder RegisterTypesWithAttribute(this ContainerBuilder builder, Assembly assembly)
    {
        var types = assembly.GetExportedTypes();
        var typesWithAttribute = types
            .Where(type => type.GetCustomAttribute<DependencyInjectionAttribute>() is not null);

        foreach (var type in typesWithAttribute)
        {
            RegisterTypeWithAttribute(builder, type);
        }
        
        return builder;
    }

    private static void RegisterTypeWithAttribute(ContainerBuilder builder, Type type)
    {
        var attribute = type.GetCustomAttribute<DependencyInjectionAttribute>();
        
        if (attribute is null)
        {
            return;
        }
        
        var singleInstance = attribute.SingleInstance;

        if (attribute.RegisterAsSelf)
        {
            RegisterAsType(builder, type, type, singleInstance);
        }

        if (attribute.RegisterAsMatchingInterface)
        {
            RegisterAsMatchingInterface(builder, type, singleInstance);
        }
        
        foreach (var registerAsType in attribute.RegisterAsAdditionalTypes)
        {
            RegisterAsType(builder, type, registerAsType, singleInstance);
        }
    }

    private static void RegisterAsType(ContainerBuilder builder, Type type, Type asType, bool singleInstance)
    {
        if (singleInstance)
        {
            builder.RegisterType(type).As(asType).SingleInstance();
            return;
        }

        builder.RegisterType(type).As(asType);
    }
    

    private static void RegisterAsMatchingInterface(ContainerBuilder builder, Type type, bool singleInstance)
    {
        var interfaceType = GetMatchingInterfaceType(type);
        if (interfaceType is null)
        {
            return;
        }
        
        RegisterAsType(builder, type, interfaceType, singleInstance);
    }

    private static Type? GetMatchingInterfaceType(Type type)
    {
        var interfaces = type.GetInterfaces();
        var interfaceName = $"I{type.Name}";
        
        return interfaces.FirstOrDefault(interfaceType => interfaceType.Name == interfaceName);
    }
}