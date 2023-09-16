using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using OpenTracker.Autofac;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Sections.Boolean;
using OpenTracker.Models.Sections.Entrance;
using OpenTracker.ViewModels.ToolTips;

namespace OpenTracker.ViewModels.MapLocations;

[DependencyInjection(SingleInstance = true)]
public sealed class MapLocationVMFactory : IMapLocationVMFactory
{
    private readonly IAlternativeRequirementDictionary _alternativeRequirements;
    private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;
        
    private readonly IMapLocationMarkingVM.Factory _markingFactory;
    private readonly IMapLocationToolTipVM.Factory _toolTipFactory;

    private readonly EntranceMapLocationVM.Factory _entranceFactory;
    private readonly ShopMapLocationVM.Factory _shopFactory;
    private readonly StandardMapLocationVM.Factory _standardFactory;
    private readonly TakeAnyMapLocationVM.Factory _takeAnyFactory;

    private readonly IMapLocationVM.Factory _locationFactory;

    public MapLocationVMFactory(
        IAlternativeRequirementDictionary alternativeRequirements,
        IEntranceShuffleRequirementDictionary entranceShuffleRequirements,
        IMapLocationMarkingVM.Factory markingFactory, IMapLocationToolTipVM.Factory toolTipFactory,
        EntranceMapLocationVM.Factory entranceFactory, ShopMapLocationVM.Factory shopFactory,
        StandardMapLocationVM.Factory standardFactory, TakeAnyMapLocationVM.Factory takeAnyFactory,
        IMapLocationVM.Factory locationFactory)
    {
        _alternativeRequirements = alternativeRequirements;
        _entranceShuffleRequirements = entranceShuffleRequirements;

        _markingFactory = markingFactory;
        _toolTipFactory = toolTipFactory;

        _entranceFactory = entranceFactory;
        _shopFactory = shopFactory;
        _standardFactory = standardFactory;
        _takeAnyFactory = takeAnyFactory;

        _locationFactory = locationFactory;
    }
        
    private IRequirement? GetDockRequirement(LocationID id)
    {
        switch (id)
        {
            case LocationID.BumperCave:
            case LocationID.DesertPalace:
                return _alternativeRequirements[new HashSet<IRequirement>
                {
                    _entranceShuffleRequirements[EntranceShuffle.All],
                    _entranceShuffleRequirements[EntranceShuffle.Insanity]
                }];
            default:
                return null;
        }
    }
        
    private static Dock GetMetDock(LocationID id)
    {
        switch (id)
        {
            case LocationID.BumperCave:
                return Dock.Left;
            case LocationID.DesertPalace:
                return Dock.Bottom;
            default:
                return Dock.Top;
        }
    }

    private static Dock GetUnmetDock(LocationID id)
    {
        switch (id)
        {
            case LocationID.EtherTablet:
            case LocationID.FloatingIsland:
            case LocationID.GanonsTower:
            case LocationID.LumberjackCaveEntrance:
            case LocationID.DeathMountainEntryCave:
            case LocationID.LeftSnitchHouseEntrance:
            case LocationID.LibraryEntrance:
            case LocationID.CastleTowerEntrance:
            case LocationID.DarkIceRodCaveEntrance:
            case LocationID.IceRodCaveEntrance:
            case LocationID.SpiralCaveBottom:
            case LocationID.SpiralCaveTop:
            case LocationID.HookshotCaveTop:
            case LocationID.TRSafetyDoor:
            case LocationID.LumberjackCaveExit:
            case LocationID.MagicBatExit:
                return Dock.Left;
            case LocationID.LumberjackHouseEntrance:
            case LocationID.WomanLeftDoor:
            case LocationID.TavernFront:
            case LocationID.MagicBatEntrance:
            case LocationID.ForestChestGameEntrance:
            case LocationID.CastleMainEntrance:
            case LocationID.EasternPalaceEntrance:
            case LocationID.DesertFrontEntrance:
            case LocationID.SkullWoodsBack:
            case LocationID.ThievesTownEntrance:
            case LocationID.BumperCaveEntrance:
            case LocationID.SwampPalaceEntrance:
            case LocationID.PalaceOfDarknessEntrance:
            case LocationID.DarkIceRodRockEntrance:
            case LocationID.IceFairyCaveEntrance:
            case LocationID.IcePalaceEntrance:
            case LocationID.MiseryMireEntrance:
            case LocationID.TowerOfHeraEntrance:
            case LocationID.ParadoxCaveMiddle:
            case LocationID.ParadoxCaveBottom:
            case LocationID.EDMConnectorBottom:
            case LocationID.TurtleRockEntrance:
            case LocationID.GanonsTowerEntrance:
            case LocationID.ForestHideoutExit:
            case LocationID.CastleSecretExit:
            case LocationID.Sanctuary:
            case LocationID.GanonHoleExit:
            case LocationID.SkullWoodsWestEntrance:
            case LocationID.SkullWoodsSWHole:
            case LocationID.SkullWoodsSEHole:
                return Dock.Bottom;
            case LocationID.GanonHole:
            case LocationID.MimicCaveEntrance:
            case LocationID.DeathMountainShop:
            case LocationID.TRLedgeRight:
            case LocationID.SkullWoodsEastEntrance:
                return Dock.Right;
            default:
                return Dock.Top;
        }
    }

    private IMapLocationMarkingVM? GetMarking(ILocation location)
    {
        var section = location.Sections[0];
            
        return location.Sections[0].Marking is not null ? _markingFactory(section) : null;
    }

    private static double GetOffsetX(Dock markingDock)
    {
        return markingDock == Dock.Right ? 0.0 : -28.0;
    }

    private static double GetOffsetY(Dock markingDock)
    {
        return markingDock == Dock.Bottom ? 0.0 : -28.0;
    }

    private static List<Point> GetPoints(Dock markingDock)
    {
        return markingDock switch
        {
            Dock.Left => new List<Point>()
            {
                new(0, 0),
                new(0, 56),
                new(28, 28)
            },
            Dock.Bottom => new List<Point>()
            {
                new(0, 28),
                new(56, 28),
                new(28, 0)
            },
            Dock.Right => new List<Point>()
            {
                new(28, 0),
                new(28, 56),
                new(0, 28)
            },
            _ => new List<Point>()
            {
                new(0, 0),
                new(56, 0),
                new(28, 28)
            }
        };
    }

    private IShapedMapLocationVMBase GetLocation(IMapLocation mapLocation)
    {
        var location = mapLocation.Location;
        var id = location.ID;
            
        switch (mapLocation.Location.Sections[0])
        {
            case IEntranceSection _:
            {
                var markingDock = GetUnmetDock(id);
                return _entranceFactory(
                    mapLocation, GetOffsetX(markingDock), GetOffsetY(markingDock), GetPoints(markingDock));
            }
            case IShopSection _:
                return _shopFactory(mapLocation);
            case ITakeAnySection _:
                return _takeAnyFactory(mapLocation);
            default:
                return _standardFactory(mapLocation);
        }
    }

    public IMapLocationVM GetMapLocation(IMapLocation mapLocation)
    {
        var location = mapLocation.Location;
        var id = location.ID;

        return _locationFactory(
            mapLocation, GetDockRequirement(id), GetMetDock(id), GetUnmetDock(id), GetMarking(location),
            GetLocation(mapLocation), _toolTipFactory(location));
    }
}