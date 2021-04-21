using System;
using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Sections.Entrance;

namespace OpenTracker.Models.Sections.Factories
{
    /// <summary>
    ///     This class contains the creation logic for dropdown sections.
    /// </summary>
    public class DropdownSectionFactory : IDropdownSectionFactory
    {
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IEntranceShuffleRequirementDictionary _entranceShuffleRequirements;

        private readonly IOverworldNodeDictionary _overworldNodes;
            
        private readonly IDropdownSection.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="alternativeRequirements">
        ///     The alternative requirement dictionary.
        /// </param>
        /// <param name="entranceShuffleRequirements">
        ///     The entrance shuffle requirement dictionary.
        /// </param>
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new dropdown sections.
        /// </param>
        public DropdownSectionFactory(
            IAlternativeRequirementDictionary alternativeRequirements,
            IEntranceShuffleRequirementDictionary entranceShuffleRequirements, IOverworldNodeDictionary overworldNodes,
            IDropdownSection.Factory factory)
        {
            _alternativeRequirements = alternativeRequirements;
            _entranceShuffleRequirements = entranceShuffleRequirements;
            
            _overworldNodes = overworldNodes;
            
            _factory = factory;
        }

        public IDropdownSection GetDropdownSection(LocationID id)
        {
            return _factory(
                GetExitNode(id), GetHoleNode(id), _alternativeRequirements[new HashSet<IRequirement>
                {
                    _entranceShuffleRequirements[EntranceShuffle.All],
                    _entranceShuffleRequirements[EntranceShuffle.Insanity]
                }]);
        }

        /// <summary>
        ///     Returns the exit node of the section.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     The exit node.
        /// </returns>
        private INode GetExitNode(LocationID id)
        {
            switch (id)
            {
                case LocationID.CastleSecretEntrance:
                    return _overworldNodes[OverworldNodeID.CastleSecretExitArea];
                case LocationID.GanonHole:
                    return _overworldNodes[OverworldNodeID.LightWorldInverted];
                default:
                    return _overworldNodes[OverworldNodeID.LightWorld];
            }
        }

        /// <summary>
        ///     Returns the hole node of the section.
        /// </summary>
        /// <param name="id">
        ///     The location ID.
        /// </param>
        /// <returns>
        ///     The hole node.
        /// </returns>
        private INode GetHoleNode(LocationID id)
        {
            switch (id)
            {
                case LocationID.LumberjackCaveEntrance:
                    return _overworldNodes[OverworldNodeID.LumberjackCaveHole];
                case LocationID.TheWellEntrance:
                    return _overworldNodes[OverworldNodeID.LightWorld];
                case LocationID.MagicBatEntrance:
                    return _overworldNodes[OverworldNodeID.MagicBatLedge];
                case LocationID.ForestHideoutEntrance:
                case LocationID.CastleSecretEntrance:
                case LocationID.HoulihanHole:
                    return _overworldNodes[OverworldNodeID.LightWorldNotBunny];
                case LocationID.SanctuaryGrave:
                    return _overworldNodes[OverworldNodeID.EscapeGrave];
                case LocationID.GanonHole:
                    return _overworldNodes[OverworldNodeID.GanonHole];
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}