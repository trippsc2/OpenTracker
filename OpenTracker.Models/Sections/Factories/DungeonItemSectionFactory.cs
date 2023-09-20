using OpenTracker.Models.AutoTracking.Values;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.AccessibilityProvider;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.Sections.Item;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Sections.Factories;

/// <summary>
/// This class contains the creation logic for <see cref="IDungeonItemSection"/> objects.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class DungeonItemSectionFactory : IDungeonItemSectionFactory
{
    private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;
        
    private readonly IMarking.Factory _markingFactory;
        
    private readonly IDungeonItemSection.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="smallKeyShuffleRequirements">
    ///     The <see cref="ISmallKeyShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="markingFactory">
    ///     An Autofac factory for creating new <see cref="IMarking"/> objects.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IDungeonItemSection"/> objects.
    /// </param>
    public DungeonItemSectionFactory(
        ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IMarking.Factory markingFactory,
        IDungeonItemSection.Factory factory)
    {
        _smallKeyShuffleRequirements = smallKeyShuffleRequirements;
        _markingFactory = markingFactory;
        _factory = factory;
    }

    public IDungeonItemSection GetDungeonItemSection(
        IDungeon dungeon, IDungeonAccessibilityProvider accessibilityProvider, IAutoTrackValue? autoTrackValue,
        LocationID id)
    {
        return _factory(dungeon, accessibilityProvider, autoTrackValue, GetMarking(id), GetRequirement(id));
    }

    /// <summary>
    /// Returns the nullable <see cref="IMarking"/> for the specified <see cref="LocationID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <returns>
    ///     The nullable <see cref="IMarking"/>.
    /// </returns>
    private IMarking? GetMarking(LocationID id)
    {
        return id is LocationID.DesertPalace or LocationID.GanonsTower ? _markingFactory() : null;
    }

    /// <summary>
    /// Returns the <see cref="IRequirement"/> for the section to be active for the specified
    /// <see cref="LocationID"/>.
    /// </summary>
    /// <param name="id">
    ///     The <see cref="LocationID"/>.
    /// </param>
    /// <returns>
    ///     The <see cref="IRequirement"/> for the section to be active.
    /// </returns>
    private IRequirement? GetRequirement(LocationID id)
    {
        return id == LocationID.AgahnimTower ? _smallKeyShuffleRequirements[true] : null;
    }
}