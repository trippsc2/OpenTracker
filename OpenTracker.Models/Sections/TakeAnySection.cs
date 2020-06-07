﻿using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.Sections
{
    public class TakeAnySection : ISection
    {
        private readonly Game _game;
        private readonly List<RequirementNodeConnection> _connections;

        public bool HasMarking => false;
        public string Name => "Take Any";
        public Mode RequiredMode { get; }
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

        public TakeAnySection(Game game, LocationID iD)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _connections = new List<RequirementNodeConnection>();
            RequiredMode = new Mode();
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
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.GrassHouseTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.GrassHouse,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.BombHutTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.BombHut,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.IceFairyCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.IceFairyCave,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.RupeeCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.RupeeCave,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.CentralBonkRocksTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.CentralBonkRocks,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.HypeFairyCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.HypeFairyCave,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.EDMFairyCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainEastBottom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.DarkChapelTakeAny:
                case LocationID.DarkVillageFortuneTellerTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldWest,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.DarkTreesFairyCaveTakeAny:
                case LocationID.DarkSahasrahlaTakeAny:
                case LocationID.DarkFluteSpotFiveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldEast,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.ArrowGameTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouth,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.DarkCentralBonkRocksTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWCentralBonkRocks,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.DarkIceRodCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodCave,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.DarkFakeIceRodCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DarkWorldSouthEast,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.DarkIceRodRockTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DWIceRodRock,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.DarkMountainFairyTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.DeathMountainWestBottom,
                            RequirementType.None, new Mode()));
                    }
                    break;
                case LocationID.MireRightShackTakeAny:
                case LocationID.MireCaveTakeAny:
                    {
                        _connections.Add(new RequirementNodeConnection(RequirementNodeID.MireArea,
                            RequirementType.None, new Mode()));
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

        private void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateAccessibility();
        }

        private void UpdateAccessibility()
        {
            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (RequirementNodeConnection connection in _connections)
            {
                AccessibilityLevel nodeAccessibility = AccessibilityLevel.Normal;
                
                nodeAccessibility = (AccessibilityLevel)Math.Min((byte)nodeAccessibility,
                    (byte)_game.RequirementNodes[connection.FromNode].Accessibility);

                if (nodeAccessibility < AccessibilityLevel.SequenceBreak)
                    continue;

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
                    finalAccessibility = finalConnectionAccessibility;
            }

            Accessibility = finalAccessibility;
        }

        public void Clear(bool force)
        {
            Available = 0;
        }

        public bool IsAvailable()
        {
            return Available > 0;
        }

        public void Reset()
        {
            Marking = null;
            Available = 1;
        }
    }
}