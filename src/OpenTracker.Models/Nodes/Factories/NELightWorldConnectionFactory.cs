using System;
using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes.Connections;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Crystal;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Mode;

namespace OpenTracker.Models.Nodes.Factories;

/// <summary>
/// This class contains the creation logic for northeast light world <see cref="INodeConnection"/> objects.
/// </summary>
public class NELightWorldConnectionFactory : INELightWorldConnectionFactory
{
    private readonly ICrystalRequirement _crystalRequirement;
    private readonly IComplexRequirementDictionary _complexRequirements;
    private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
    private readonly IItemRequirementDictionary _itemRequirements;
    private readonly IItemExactRequirementDictionary _itemExactRequirements;
    private readonly IPrizeRequirementDictionary _prizeRequirements;
    private readonly IWorldStateRequirementDictionary _worldStateRequirements;
        
    private readonly IOverworldNodeDictionary _overworldNodes;

    private readonly INodeConnection.Factory _connectionFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="crystalRequirement">
    ///     The <see cref="ICrystalRequirement"/>.
    /// </param>
    /// <param name="complexRequirements">
    ///     The <see cref="IComplexRequirementDictionary"/>.
    /// </param>
    /// <param name="entranceShuffleRequirements">
    ///     The <see cref="IEntranceShuffleRequirementDictionary"/>.
    /// </param>
    /// <param name="itemRequirements">
    ///     The <see cref="IItemRequirementDictionary"/>.
    /// </param>
    /// <param name="itemExactRequirements">
    ///     The <see cref="IItemExactRequirementDictionary"/>.
    /// </param>
    /// <param name="prizeRequirements">
    ///     The <see cref="IPrizeRequirementDictionary"/>.
    /// </param>
    /// <param name="worldStateRequirements">
    ///     The <see cref="IWorldStateRequirementDictionary"/>.
    /// </param>
    /// <param name="overworldNodes">
    ///     The <see cref="IOverworldNodeDictionary"/>.
    /// </param>
    /// <param name="connectionFactory">
    ///     An Autofac factory for creating new <see cref="INodeConnection"/> objects.
    /// </param>
    public NELightWorldConnectionFactory(
        ICrystalRequirement crystalRequirement, IComplexRequirementDictionary complexRequirements,
        IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
        IItemRequirementDictionary itemRequirements, IItemExactRequirementDictionary itemExactRequirements,
        IPrizeRequirementDictionary prizeRequirements, IWorldStateRequirementDictionary worldStateRequirements,
        IOverworldNodeDictionary overworldNodes, INodeConnection.Factory connectionFactory)
    {
        _crystalRequirement = crystalRequirement;
        _complexRequirements = complexRequirements;
        _entranceShuffleRequirements = entranceShuffleRequirements;
        _itemRequirements = itemRequirements;
        _itemExactRequirements = itemExactRequirements;
        _prizeRequirements = prizeRequirements;
        _worldStateRequirements = worldStateRequirements;
            
        _overworldNodes = overworldNodes;
            
        _connectionFactory = connectionFactory;
    }

    public IEnumerable<INodeConnection> GetNodeConnections(OverworldNodeID id, INode node)
    {
        return id switch
        {
            OverworldNodeID.HyruleCastleTop => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorld], node,
                    _entranceShuffleRequirements[EntranceShuffle.None]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DarkWorldEast], node,
                    _complexRequirements[ComplexRequirementType.DWMirror])
            },
            OverworldNodeID.HyruleCastleTopInverted => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.HyruleCastleTop], node,
                    _worldStateRequirements[WorldState.Inverted])
            },
            OverworldNodeID.HyruleCastleTopStandardOpen => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.HyruleCastleTop], node,
                    _worldStateRequirements[WorldState.StandardOpen])
            },
            OverworldNodeID.AgahnimTowerEntrance => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.HyruleCastleTopInverted], node,
                    _crystalRequirement),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.HyruleCastleTopStandardOpen], node,
                    _complexRequirements[ComplexRequirementType.ATBarrier])
            },
            OverworldNodeID.CastleSecretExitArea => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunny], node)
            },
            OverworldNodeID.ZoraArea => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWWitchAreaNotBunny], node,
                    _itemRequirements[(ItemType.Gloves, 1)]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.CatfishArea], node,
                    _complexRequirements[ComplexRequirementType.DWMirror]),
                _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node),
                _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
            },
            OverworldNodeID.ZoraLedge => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.ZoraArea], node,
                    _itemRequirements[(ItemType.Flippers, 1)]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.ZoraArea], node,
                    _complexRequirements[ComplexRequirementType.FakeFlippersSplashDeletion]),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.ZoraArea], node,
                    _complexRequirements[ComplexRequirementType.WaterWalk])
            },
            OverworldNodeID.WaterfallFairy => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaFlippers], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWLakeHyliaFakeFlippers], node,
                    _itemRequirements[(ItemType.MoonPearl, 1)]),
                _connectionFactory(_overworldNodes[OverworldNodeID.LWLakeHyliaWaterWalk], node)
            },
            OverworldNodeID.WaterfallFairyNotBunny => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.WaterfallFairy], node,
                    _complexRequirements[ComplexRequirementType.NotBunnyLW])
            },
            OverworldNodeID.LWWitchArea => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldNotBunny], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.ZoraArea], node,
                    _itemRequirements[(ItemType.Gloves, 1)])
            },
            OverworldNodeID.LWWitchAreaNotBunny => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWWitchArea], node,
                    _complexRequirements[ComplexRequirementType.NotBunnyLW])
            },
            OverworldNodeID.WitchsHut => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWWitchArea], node,
                    _itemExactRequirements[(ItemType.Mushroom, 1)])
            },
            OverworldNodeID.Sahasrahla => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LightWorld], node,
                    _prizeRequirements[(PrizeType.GreenPendant, 1)])
            },
            OverworldNodeID.LWEastPortal => new List<INodeConnection>
            {
                _connectionFactory(_overworldNodes[OverworldNodeID.LightWorldHammer], node),
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.DWEastPortalInverted], node,
                    _itemRequirements[(ItemType.Gloves, 1)])
            },
            OverworldNodeID.LWEastPortalStandardOpen => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWEastPortal], node,
                    _worldStateRequirements[WorldState.StandardOpen])
            },
            OverworldNodeID.LWEastPortalNotBunny => new List<INodeConnection>
            {
                _connectionFactory(
                    _overworldNodes[OverworldNodeID.LWEastPortal], node,
                    _complexRequirements[ComplexRequirementType.NotBunnyLW])
            },
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }
}