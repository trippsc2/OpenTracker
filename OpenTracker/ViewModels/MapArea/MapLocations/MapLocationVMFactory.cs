using Avalonia;
using Avalonia.Controls;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.UIPanels.LocationsPanel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.MapArea.MapLocations
{
    /// <summary>
    /// This is the class containing creation logic for map location control ViewModel classes.
    /// </summary>
    internal static class MapLocationVMFactory
    {
        /// <summary>
        /// Returns an observable collection of marking select control ViewModel instances for
        /// the supplied section.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// An observable collection of marking select control ViewModel instances.
        /// </returns>
        private static ObservableCollection<MarkingSelectVM> GetMarkingSelectControlVMs(
            IMarkableSection section)
        {
            return section is IEntranceSection ?
                MainWindowVM.EntranceMarkingSelect :
                MainWindowVM.NonEntranceMarkingSelect;
        }

        /// <summary>
        /// Returns a number representing the width of the marking select popup for the specified
        /// section.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// A floating point number representing the width of the marking select popup.
        /// </returns>
        private static double GetMarkingSelectWidth(IMarkableSection section)
        {
            return section is IEntranceSection ? 272.0 : 238.0;
        }

        /// <summary>
        /// Returns a number representing the height of the marking select popup for the specified
        /// section.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// A floating point number representing the height of the marking select popup.
        /// </returns>
        private static double GetMarkingSelectHeight(IMarkableSection section)
        {
            return section is IEntranceSection ? 280.0 : 200.0;
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
        private static MarkingMapLocationVM GetMapLocationMarkingControlVM(IMarkableSection section)
        {
            return new MarkingMapLocationVM(
                section, GetMarkingSelectControlVMs(section), GetMarkingSelectWidth(section),
                GetMarkingSelectHeight(section));
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
                case LocationID.WomanLeftDoor:
                case LocationID.TavernFront:
                case LocationID.ForestChestGameEntrance:
                case LocationID.CastleMainEntrance:
                case LocationID.CastleTowerEntrance:
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
                    {
                        return Dock.Bottom;
                    }
                case LocationID.LibraryEntrance:
                case LocationID.DeathMountainEntryCave:
                case LocationID.DarkIceRodCaveEntrance:
                case LocationID.IceRodCaveEntrance:
                case LocationID.SpiralCaveBottom:
                case LocationID.SpiralCaveTop:
                case LocationID.HookshotCaveTop:
                    {
                        return Dock.Left;
                    }
                case LocationID.MimicCaveEntrance:
                case LocationID.DeathMountainShop:
                case LocationID.TRLedgeRight:
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
        /// <param name="mapArea">
        /// The map area control ViewModel parent class.
        /// </param>
        /// <param name="pinnedLocations">
        /// The observable collection of pinned locations.
        /// </param>
        /// <returns>
        /// A new entrance map location control ViewModel instance.
        /// </returns>
        private static MapLocationVMBase GetEntranceMapLocationControlVM(
            MapLocation mapLocation, MapAreaControlVM mapArea,
            ObservableCollection<PinnedLocationVM> pinnedLocations)
        {
            if (mapLocation == null)
            {
                throw new ArgumentNullException(nameof(mapLocation));
            }

            if (mapArea == null)
            {
                throw new ArgumentNullException(nameof(mapArea));
            }

            if (pinnedLocations == null)
            {
                throw new ArgumentNullException(nameof(pinnedLocations));
            }

            IMarkableSection section = (IMarkableSection)mapLocation.Location.Sections[0];
            Dock markingDock = GetEntranceMarkingDock(mapLocation.Location.ID);

            return new EntranceMapLocationVM(
                mapLocation, mapArea, pinnedLocations, GetMapLocationMarkingControlVM(section),
                markingDock, GetEntrancePoints(markingDock));
        }

        /// <summary>
        /// Returns a new markable map location control ViewModel instance for the specified map
        /// location.
        /// </summary>
        /// <param name="mapLocation">
        /// The map location.
        /// </param>
        /// <param name="mapArea">
        /// The map area control ViewModel parent class.
        /// </param>
        /// <param name="pinnedLocations">
        /// The observable collection of pinned locations.
        /// </param>
        /// <returns>
        /// A new markable map location control ViewModel instance.
        /// </returns>
        private static MapLocationVMBase GetMarkableMapLocationControlVM(
            MapLocation mapLocation, MapAreaControlVM mapArea,
            ObservableCollection<PinnedLocationVM> pinnedLocations)
        {
            if (mapLocation == null)
            {
                throw new ArgumentNullException(nameof(mapLocation));
            }

            if (mapArea == null)
            {
                throw new ArgumentNullException(nameof(mapArea));
            }

            if (pinnedLocations == null)
            {
                throw new ArgumentNullException(nameof(pinnedLocations));
            }

            LocationID id = mapLocation.Location.ID;

            return new MarkableMapLocationVM(
                mapLocation, mapArea, pinnedLocations,
                GetMapLocationMarkingControlVM((IMarkableSection)mapLocation.Location.Sections[0]),
                GetMarkableEntranceMarkingDock(id), GetMarkableNonEntranceMarkingDock(id));
        }

        /// <summary>
        /// Returns a new map location control ViewModel instance for the specified map location.
        /// </summary>
        /// <param name="mapLocation">
        /// The map location.
        /// </param>
        /// <param name="mapArea">
        /// The map area control ViewModel parent class.
        /// </param>
        /// <param name="pinnedLocations">
        /// The observable collection of pinned locations.
        /// </param>
        /// <returns>
        /// A new map location control ViewModel instance.
        /// </returns>
        public static MapLocationVMBase GetMapLocationControlVM(
            MapLocation mapLocation, MapAreaControlVM mapArea,
            ObservableCollection<PinnedLocationVM> pinnedLocations)
        {
            if (mapLocation == null)
            {
                throw new ArgumentNullException(nameof(mapLocation));
            }

            if (mapArea == null)
            {
                throw new ArgumentNullException(nameof(mapArea));
            }

            if (pinnedLocations == null)
            {
                throw new ArgumentNullException(nameof(pinnedLocations));
            }

            switch (mapLocation.Location.Sections[0])
            {
                case IEntranceSection _:
                    {
                        return GetEntranceMapLocationControlVM(mapLocation, mapArea, pinnedLocations);
                    }
                case ITakeAnySection _:
                    {
                        return new TakeAnyMapLocationVM(mapLocation, mapArea, pinnedLocations);
                    }
                case IMarkableSection _:
                    {
                        return GetMarkableMapLocationControlVM(mapLocation, mapArea, pinnedLocations);
                    }
                default:
                    {
                        return new MapLocationVM(mapLocation, mapArea, pinnedLocations);
                    }
            };
        }
    }
}
