using Avalonia;
using Avalonia.Controls;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Maps.Locations
{
    /// <summary>
    /// This is the class containing creation logic for map location control ViewModel classes.
    /// </summary>
    public class MapLocationVMFactory : IMapLocationVMFactory
    {
        private readonly IMarkingMapLocationVM.Factory _markingFactory;
        private readonly EntranceMapLocationVM.Factory _entranceFactory;
        private readonly TakeAnyMapLocationVM.Factory _takeAnyFactory;
        private readonly ShopMapLocationVM.Factory _shopFactory;
        private readonly MarkableDungeonMapLocationVM.Factory _markableDungeonFactory;
        private readonly MarkableMapLocationVM.Factory _markableFactory;
        private readonly DungeonMapLocationVM.Factory _dungeonFactory;
        private readonly MapLocationVM.Factory _locationFactory;

        public MapLocationVMFactory(
            IMarkingMapLocationVM.Factory markingFactory,
            EntranceMapLocationVM.Factory entranceFactory,
            TakeAnyMapLocationVM.Factory takeAnyFactory, ShopMapLocationVM.Factory shopFactory,
            MarkableDungeonMapLocationVM.Factory markableDungeonFactory,
            MarkableMapLocationVM.Factory markableFactory,
            DungeonMapLocationVM.Factory dungeonFactory, MapLocationVM.Factory locationFactory)
        {
            _markingFactory = markingFactory;
            _entranceFactory = entranceFactory;
            _takeAnyFactory = takeAnyFactory;
            _shopFactory = shopFactory;
            _markableDungeonFactory = markableDungeonFactory;
            _markableFactory = markableFactory;
            _dungeonFactory = dungeonFactory;
            _locationFactory = locationFactory;
        }

        /// <summary>
        /// Returns a new marking map location control ViewModel instance for the speicifed
        /// section.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// A new marking map location control ViewModel instance.
        /// </returns>
        private IMarkingMapLocationVM GetMapLocationMarkingControlVM(IMarkableSection section)
        {
            return _markingFactory(section);
        }

        /// <summary>
        /// Returns the marking dock direction of the specified location identity.
        /// </summary>
        /// <param name="id">
        /// The location identity.
        /// </param>
        /// <returns>
        /// The marking dock direction.
        /// </returns>
        private static Dock GetEntranceMarkingDock(LocationID id)
        {
            switch (id)
            {
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
                    {
                        return Dock.Bottom;
                    }
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
                    {
                        return Dock.Left;
                    }
                case LocationID.GanonHole:
                case LocationID.MimicCaveEntrance:
                case LocationID.DeathMountainShop:
                case LocationID.TRLedgeRight:
                case LocationID.SkullWoodsEastEntrance:
                    {
                        return Dock.Right;
                    }
                default:
                    {
                        return Dock.Top;
                    }
            }
        }

        /// <summary>
        /// Returns a list of points representing the shape of the entrance triangle polygon.
        /// control.
        /// </summary>
        /// <param name="markingDock">
        /// The marking dock direction.
        /// </param>
        /// <returns>
        /// A list of points representing the shape of the entrance triangle polygon.
        /// </returns>
        private static List<Point> GetEntrancePoints(Dock markingDock)
        {
            return markingDock switch
            {
                Dock.Left => new List<Point>()
                        {
                            new Point(0, 0),
                            new Point(0, 56),
                            new Point(28, 28)
                        },
                Dock.Bottom => new List<Point>()
                        {
                            new Point(0, 28),
                            new Point(56, 28),
                            new Point(28, 0)
                        },
                Dock.Right => new List<Point>()
                        {
                            new Point(28, 0),
                            new Point(28, 56),
                            new Point(0, 28)
                        },
                _ => new List<Point>()
                        {
                            new Point(0, 0),
                            new Point(56, 0),
                            new Point(28, 28)
                        }
            };
        }

        /// <summary>
        /// Returns the marking dock direction of the specified location identity when entrance
        /// shuffle is enabled.
        /// </summary>
        /// <param name="id">
        /// The specified location identity.
        /// </param>
        /// <returns>
        /// The marking dock direction.
        /// </returns>
        private static Dock GetMarkableEntranceMarkingDock(LocationID id)
        {
            switch (id)
            {
                case LocationID.EtherTablet:
                case LocationID.FloatingIsland:
                case LocationID.GanonsTower:
                case LocationID.BumperCave:
                    {
                        return Dock.Left;
                    }
                case LocationID.DesertPalace:
                    {
                        return Dock.Bottom;
                    }
                default:
                    {
                        return Dock.Top;
                    }
            }
        }

        /// <summary>
        /// Returns the marking dock direction of the specified location identity when entrance
        /// shuffle is disabled.
        /// </summary>
        /// <param name="id">
        /// The specified location identity.
        /// </param>
        /// <returns>
        /// The marking dock direction.
        /// </returns>
        private static Dock GetMarkableNonEntranceMarkingDock(LocationID id)
        {
            switch (id)
            {
                case LocationID.EtherTablet:
                case LocationID.FloatingIsland:
                case LocationID.GanonsTower:
                    {
                        return Dock.Left;
                    }
                default:
                    {
                        return Dock.Top;
                    }
            }
        }

        /// <summary>
        /// Returns a new entrance map location control ViewModel instance for the specified map
        /// location.
        /// </summary>
        /// <param name="mapLocation">
        /// The map location.
        /// </param>
        /// A new entrance map location control ViewModel instance.
        /// </returns>
        private IMapLocationVMBase GetEntranceMapLocationControlVM(IMapLocation mapLocation)
        {
            IMarkableSection section = (IMarkableSection)mapLocation.Location!.Sections[0];
            Dock markingDock = GetEntranceMarkingDock(mapLocation.Location.ID);

            return _entranceFactory(
                mapLocation, GetMapLocationMarkingControlVM(section),
                markingDock, GetEntrancePoints(markingDock));
        }

        /// <summary>
        /// Returns a new markable map location control ViewModel instance for the specified map
        /// location.
        /// </summary>
        /// <param name="mapLocation">
        /// The map location.
        /// </param>
        /// <returns>
        /// A new markable map location control ViewModel instance.
        /// </returns>
        private IMapLocationVMBase GetMarkableMapLocationControlVM(IMapLocation mapLocation)
        {
            LocationID id = mapLocation.Location!.ID;

            if (mapLocation.Location is IDungeon)
            {
                return _markableDungeonFactory(
                    mapLocation,
                    GetMapLocationMarkingControlVM((IMarkableSection)mapLocation.Location.Sections[0]),
                    GetMarkableEntranceMarkingDock(id), GetMarkableNonEntranceMarkingDock(id));
            }

            return _markableFactory(
                mapLocation,
                GetMapLocationMarkingControlVM((IMarkableSection)mapLocation.Location.Sections[0]),
                GetMarkableEntranceMarkingDock(id), GetMarkableNonEntranceMarkingDock(id));
        }

        /// <summary>
        /// Returns a new map location control ViewModel instance for the specified map location.
        /// </summary>
        /// <param name="mapLocation">
        /// The map location.
        /// </param>
        /// <returns>
        /// A new map location control ViewModel instance.
        /// </returns>
        public IMapLocationVMBase GetMapLocationControlVM(IMapLocation mapLocation)
        {
            switch (mapLocation.Location!.Sections[0])
            {
                case IEntranceSection _:
                    {
                        return GetEntranceMapLocationControlVM(mapLocation);
                    }
                case ITakeAnySection _:
                    {
                        return _takeAnyFactory(mapLocation);
                    }
                case IShopSection _:
                    {
                        return _shopFactory(mapLocation);
                    }
                case IMarkableSection _:
                    {
                        return GetMarkableMapLocationControlVM(mapLocation);
                    }
                default:
                    {
                        if (mapLocation.Location is IDungeon)
                        {
                            return _dungeonFactory(mapLocation);
                        }

                        return _locationFactory(mapLocation);
                    }
            };
        }
    }
}
