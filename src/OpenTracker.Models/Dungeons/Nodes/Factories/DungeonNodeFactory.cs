using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Nodes.Connections;

namespace OpenTracker.Models.Dungeons.Nodes.Factories;

/// <summary>
/// This class contains the creation logic for dungeon nodes.
/// </summary>
public class DungeonNodeFactory : IDungeonNodeFactory
{
    private readonly IHCDungeonNodeFactory _hcFactory;
    private readonly IATDungeonNodeFactory _atFactory;
    private readonly IEPDungeonNodeFactory _epFactory;
    private readonly IDPDungeonNodeFactory _dpFactory;
    private readonly IToHDungeonNodeFactory _tohFactory;
    private readonly IPoDDungeonNodeFactory _podFactory;
    private readonly ISPDungeonNodeFactory _spFactory;
    private readonly ISWDungeonNodeFactory _swFactory;
    private readonly ITTDungeonNodeFactory _ttFactory;
    private readonly IIPDungeonNodeFactory _ipFactory;
    private readonly IMMDungeonNodeFactory _mmFactory;
    private readonly ITRDungeonNodeFactory _trFactory;
    private readonly IGTDungeonNodeFactory _gtFactory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hcFactory">
    ///     The <see cref="IHCDungeonNodeFactory"/>.
    /// </param>
    /// <param name="atFactory">
    ///     The <see cref="IATDungeonNodeFactory"/>.
    /// </param>
    /// <param name="epFactory">
    ///     The <see cref="IEPDungeonNodeFactory"/>.
    /// </param>
    /// <param name="dpFactory">
    ///     The <see cref="IDPDungeonNodeFactory"/>.
    /// </param>
    /// <param name="tohFactory">
    ///     The <see cref="IToHDungeonNodeFactory"/>.
    /// </param>
    /// <param name="podFactory">
    ///     The <see cref="IPoDDungeonNodeFactory"/>.
    /// </param>
    /// <param name="spFactory">
    ///     The <see cref="ISPDungeonNodeFactory"/>.
    /// </param>
    /// <param name="swFactory">
    ///     The <see cref="ISWDungeonNodeFactory"/>.
    /// </param>
    /// <param name="ttFactory">
    ///     The <see cref="ITTDungeonNodeFactory"/>.
    /// </param>
    /// <param name="ipFactory">
    ///     The <see cref="IIPDungeonNodeFactory"/>.
    /// </param>
    /// <param name="mmFactory">
    ///     The <see cref="IMMDungeonNodeFactory"/>.
    /// </param>
    /// <param name="trFactory">
    ///     The <see cref="ITRDungeonNodeFactory"/>.
    /// </param>
    /// <param name="gtFactory">
    ///     The <see cref="IGTDungeonNodeFactory"/>.
    /// </param>
    public DungeonNodeFactory(
        IHCDungeonNodeFactory hcFactory, IATDungeonNodeFactory atFactory, IEPDungeonNodeFactory epFactory,
        IDPDungeonNodeFactory dpFactory, IToHDungeonNodeFactory tohFactory, IPoDDungeonNodeFactory podFactory,
        ISPDungeonNodeFactory spFactory, ISWDungeonNodeFactory swFactory, ITTDungeonNodeFactory ttFactory,
        IIPDungeonNodeFactory ipFactory, IMMDungeonNodeFactory mmFactory, ITRDungeonNodeFactory trFactory,
        IGTDungeonNodeFactory gtFactory)
    {
        _hcFactory = hcFactory;
        _atFactory = atFactory;
        _epFactory = epFactory;
        _dpFactory = dpFactory;
        _tohFactory = tohFactory;
        _podFactory = podFactory;
        _spFactory = spFactory;
        _swFactory = swFactory;
        _ttFactory = ttFactory;
        _ipFactory = ipFactory;
        _mmFactory = mmFactory;
        _trFactory = trFactory;
        _gtFactory = gtFactory;
    }

    public void PopulateNodeConnections(
        IMutableDungeon dungeonData, DungeonNodeID id, INode node, IList<INodeConnection> connections)
    {
        switch (dungeonData.ID)
        {
            case DungeonID.HyruleCastle:
                _hcFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.AgahnimTower:
                _atFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.EasternPalace:
                _epFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.DesertPalace:
                _dpFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.TowerOfHera:
                _tohFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.PalaceOfDarkness:
                _podFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.SwampPalace:
                _spFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.SkullWoods:
                _swFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.ThievesTown:
                _ttFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.IcePalace:
                _ipFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.MiseryMire:
                _mmFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.TurtleRock:
                _trFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            case DungeonID.GanonsTower:
                _gtFactory.PopulateNodeConnections(dungeonData, id, node, connections);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}