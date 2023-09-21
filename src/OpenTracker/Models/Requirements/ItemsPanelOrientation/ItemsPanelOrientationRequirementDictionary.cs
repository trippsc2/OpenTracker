using System.Collections.Generic;
using Avalonia.Layout;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.ItemsPanelOrientation;

/// <summary>
///     This class contains the dictionary container for items panel orientation requirements.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ItemsPanelOrientationRequirementDictionary : LazyDictionary<Orientation, IRequirement>,
    IItemsPanelOrientationRequirementDictionary
{
    private readonly ItemsPanelOrientationRequirement.Factory _factory;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new items panel orientation requirements.
    /// </param>
    public ItemsPanelOrientationRequirementDictionary(ItemsPanelOrientationRequirement.Factory factory)
        : base(new Dictionary<Orientation, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(Orientation key)
    {
        return _factory(key);
    }
}