using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    /// <summary>
    /// This is the class containing take any sections of locations.
    /// </summary>
    public class TakeAnySection : ISection
    {
        private readonly Game _game;
        private readonly List<RequirementNodeConnection> _connections;

        public bool HasMarking => false;
        public string Name => "Take Any";
        public ModeRequirement ModeRequirement { get; }
        public bool UserManipulated { get; set; }

        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        private int _available;
        public int Available
        {
            get => _available;
            set
            {
                if (_available != value)
                {
                    _available = value;
                    OnPropertyChanged(nameof(Available));
                }
            }
        }

        private MarkingType? _marking;
        public MarkingType? Marking
        {
            get => _marking;
            set
            {
                if (_marking != value)
                {
                    OnPropertyChanging(nameof(Marking));
                    _marking = value;
                    OnPropertyChanged(nameof(Marking));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="iD">
        /// The location identity.
        /// </param>
        public TakeAnySection(Game game, LocationID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _connections = new List<RequirementNodeConnection>();
            ModeRequirement = new ModeRequirement();
            Available = 1;

            switch (iD)
            {
                case LocationID.TreesFairyCaveTakeAny:
                case LocationID.KakarikoFortuneTellerTakeAny:
                case LocationID.PegsFairyCaveTakeAny:
                case LocationID.ForestChestGameTakeAny:
                case LocationID.LumberjackHouseTakeAny:
                case LocationID.LeftSnitchHouseTakeAny:
                case LocationID.RightSnitchHouseTakeAny:
                case LocationID.ThiefCaveTakeAny:
                case LocationID.IceBeeCaveTakeAny:
                case LocationID.FortuneTellerTakeAny:
                case LocationID.ChestGameTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.LightWorld,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.GrassHouseTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GrassHouse,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.BombHutTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombHut,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.IceFairyCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IceFairyCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.RupeeCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.RupeeCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.CentralBonkRocksTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CentralBonkRocks,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.HypeFairyCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HypeFairyCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.EDMFairyCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.ArrowGameTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkCentralBonkRocksTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWCentralBonkRocks,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkIceRodCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodCave,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthEast,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkIceRodRockTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodRock,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.DarkMountainFairyTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new ModeRequirement()));
                    }
                    break;
            }

            List<RequirementNodeID> nodeSubscriptions = new List<RequirementNodeID>();
            List<RequirementType> requirementSubscriptions = new List<RequirementType>();

            foreach (RequirementNodeConnection connection in _connections)
            {
                if (!nodeSubscriptions.Contains(connection.FromNode))
                {
                    _game.RequirementNodes[connection.FromNode].PropertyChanged += OnRequirementChanged;
                    nodeSubscriptions.Add(connection.FromNode);
                }

                if (!requirementSubscriptions.Contains(connection.Requirement))
                {
                    _game.Requirements[connection.Requirement].PropertyChanged += OnRequirementChanged;
                    requirementSubscriptions.Add(connection.Requirement);
                }
            }

            UpdateAccessibility();
        }

        /// <summary>
        /// Raises the PropertyChanging event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changing property.
        /// </param>
        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Requirement and RequirementNode
        /// classes that are requirements for dungeon items.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items.
        /// </summary>
        private void UpdateAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)_game.RequirementNodes[connection.FromNode].Accessibility);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                {
                    continue;
                }

                AccessibilityLevel requirementAccessibility =
                    _game.Requirements[connection.Requirement].Accessibility;

                AccessibilityLevel finalConnectionAccessibility =
                    (AccessibilityLevel)Math.Min(Math.Min((byte)nodeAccessibility,
                    (byte)requirementAccessibility), (byte)connection.MaximumAccessibility);

                if (finalConnectionAccessibility == AccessibilityLevel.Normal)
                {
                    finalAccessibility = AccessibilityLevel.Normal;
                    break;
                }

                if (finalConnectionAccessibility > finalAccessibility)
                {
                    finalAccessibility = finalConnectionAccessibility;
                }
            }

            Accessibility = finalAccessibility;
        }

        /// <summary>
        /// Clears the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether to override the location logic.
        /// </param>
        public void Clear(bool force)
        {
            Available = 0;
        }

        /// <summary>
        /// Returns whether the location has not been fully collected.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the section has been fully collected.
        /// </returns>
        public bool IsAvailable()
        {
            return Available > 0;
        }

        /// <summary>
        /// Resets the section to its starting values.
        /// </summary>
        public void Reset()
        {
            Marking = null;
            Available = 1;
        }
    }
}
