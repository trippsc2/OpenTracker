using System;

namespace OpenTracker.Utils.Autofac;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class DependencyInjectionAttribute : Attribute
{
    /// <summary>
    /// A <see cref="bool"/> representing whether the type should be registered as a singleton.
    /// </summary>
    public bool SingleInstance { get; init; }
    /// <summary>
    /// A <see cref="bool"/> representing whether the type should be registered as itself.
    /// </summary>
    public bool RegisterAsSelf { get; init; } = true;
    /// <summary>
    /// A <see cref="bool"/> representing whether the type should be registered as its matching interface.
    /// </summary>
    public bool RegisterAsMatchingInterface { get; init; } = true;
    public Type[] RegisterAsAdditionalTypes { get; }
    
    public DependencyInjectionAttribute(params Type[] registerAsAdditionalTypes)
    {
        RegisterAsAdditionalTypes = registerAsAdditionalTypes;
    }
}