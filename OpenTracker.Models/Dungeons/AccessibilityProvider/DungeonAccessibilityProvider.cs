using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.Items;
using OpenTracker.Models.Items.Keys;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.KeyDoor;
using ReactiveUI;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    ///     This class contains the logic for updating the dungeon accessibility.
    /// </summary>
    public class DungeonAccessibilityProvider : ReactiveObject, IDungeonAccessibilityProvider
    {
        private readonly IDungeon _dungeon;
        private readonly IMutableDungeonQueue _mutableDungeonQueue;
        
        private readonly IKeyDoorIterator _keyDoorIterator;
        private readonly IResultAggregator _resultAggregator;

        private bool _visible;
        public bool Visible
        {
            get => _visible;
            private set => this.RaiseAndSetIfChanged(ref _visible, value);
        }

        private bool _sequenceBreak;
        public bool SequenceBreak
        {
            get => _sequenceBreak;
            private set => this.RaiseAndSetIfChanged(ref _sequenceBreak, value);
        }

        private int _accessible;
        public int Accessible
        {
            get => _accessible;
            private set => this.RaiseAndSetIfChanged(ref _accessible, value);
        }

        public IList<IBossAccessibilityProvider> BossAccessibilityProviders { get; } =
            new List<IBossAccessibilityProvider>();

        private ISmallKeyItem SmallKey => _dungeon.SmallKey;
        private IBigKeyItem? BigKey => _dungeon.BigKey;
        private IEnumerable<DungeonItemID> Bosses => _dungeon.Bosses;
        private IEnumerable<DungeonNodeID> Nodes => _dungeon.Nodes;
        private IEnumerable<INode> EntryNodes => _dungeon.EntryNodes;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="mode">
        ///     The mode settings data.
        /// </param>
        /// <param name="bossProviderFactory">
        ///     An Autofac factory for creating boss accessibility providers.
        /// </param>
        /// <param name="mutableDungeonQueue">
        ///     An Autofac factory for creating the mutable dungeon queue.
        /// </param>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="keyDoorIterator">
        ///     The key door iterator.
        /// </param>
        /// <param name="resultAggregator">
        ///     The result aggregator.
        /// </param>
        public DungeonAccessibilityProvider(
            IMode mode, IBossAccessibilityProvider.Factory bossProviderFactory,
            IMutableDungeonQueue.Factory mutableDungeonQueue, IDungeon dungeon,
            IKeyDoorIterator.Factory keyDoorIterator, IResultAggregator.Factory resultAggregator)
        {
            _dungeon = dungeon;
            _mutableDungeonQueue = mutableDungeonQueue(_dungeon);

            _keyDoorIterator = keyDoorIterator(_dungeon, _mutableDungeonQueue);
            _resultAggregator = resultAggregator(_dungeon, _mutableDungeonQueue);

            foreach (var _ in Bosses)
            {
                BossAccessibilityProviders.Add(bossProviderFactory());
            }

            mode.PropertyChanged += OnModeChanged;

            if (BigKey is not null)
            {
                BigKey.PropertyChanged += OnBigKeyChanged;
            }

            SmallKey.PropertyChanged += OnSmallKeyChanged;

            foreach (var node in EntryNodes)
            {
                node.PropertyChanged += OnNodeChanged;
            }
            
            SubscribeToConnectionRequirements();
            UpdateValues();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.MapShuffle) || e.PropertyName == nameof(IMode.CompassShuffle) ||
                e.PropertyName == nameof(IMode.SmallKeyShuffle) || e.PropertyName == nameof(IMode.BigKeyShuffle) ||
                e.PropertyName == nameof(IMode.GenericKeys) || e.PropertyName == nameof(IMode.GuaranteedBossItems) ||
                e.PropertyName == nameof(IMode.KeyDropShuffle))
            {
                UpdateValues();
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnBigKeyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateValues();
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IKeyItem interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnSmallKeyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISmallKeyItem.EffectiveCurrent))
            {
                UpdateValues();
            }
        }

        /// <summary>
        ///     Subscribes to the ChangePropagated event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the ChangePropagated event.
        /// </param>
        private void OnRequirementChangePropagated(object? sender, EventArgs e)
        {
            UpdateValues();
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IRequirementNode interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IOverworldNode.Accessibility))
            {
                UpdateValues();
            }
        }

        /// <summary>
        ///     Subscribes to PropertyChanged event on each requirement.
        /// </summary>
        private void SubscribeToConnectionRequirements()
        {
            var requirementSubscriptions = new List<IRequirement>();
            var dungeonData = _mutableDungeonQueue.GetNext();

            foreach (var node in Nodes)
            {
                foreach (var connection in dungeonData.Nodes[node].Connections)
                {
                    var requirement = connection.Requirement;

                    if (requirement is KeyDoorRequirement || requirementSubscriptions.Contains(requirement))
                    {
                        continue;
                    }
                    
                    requirement.ChangePropagated += OnRequirementChangePropagated;
                    requirementSubscriptions.Add(requirement);
                }
            }
            
            _mutableDungeonQueue.Requeue(dungeonData);
        }

        /// <summary>
        ///     Updates all values in the accessibility provider.
        /// </summary>
        private void UpdateValues()
        {
            var finalQueue = new BlockingCollection<IDungeonState>();

            _keyDoorIterator.ProcessKeyDoorPermutations(finalQueue);
            var result = _resultAggregator.AggregateResults(finalQueue);

            for (var i = 0; i < result.BossAccessibility.Count; i++)
            {
                BossAccessibilityProviders[i].Accessibility = result.BossAccessibility[i];
            }

            Visible = result.Visible;
            SequenceBreak = result.SequenceBreak;
            Accessible = result.Accessible;
        }
    }
}