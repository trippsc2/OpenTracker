﻿using Autofac;
using OpenTracker.Autofac;
using OpenTracker.Models.Autofac;
using OpenTracker.Utils.Autofac;
using Splat.Autofac;

namespace OpenTracker;

/// <summary>
/// This class contains the logic to create and configure the Autofac container.
/// </summary>
public static class ContainerConfig
{
    public static ContainerBuilder GetContainerBuilder()
    {
        var builder = new ContainerBuilder();

        return builder
            .RegisterOpenTrackerTypes()
            .RegisterOpenTrackerUtilsTypes()
            .RegisterOpenTrackerModelsTypes();
    }

    /// <summary>
    /// Returns a newly configured Autofac container.
    /// </summary>
    /// <returns>
    /// A new Autofac container.
    /// </returns>
    public static IContainer Configure()
    {
        var builder = GetContainerBuilder();
        var container = builder.Build();
        var autofacResolver = container.Resolve<AutofacDependencyResolver>();
        autofacResolver.SetLifetimeScope(container);
            
        return container;
    }
}