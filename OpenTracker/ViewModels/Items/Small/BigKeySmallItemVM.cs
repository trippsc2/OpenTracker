using Avalonia.Layout;
using OpenTracker.Interfaces;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    public class BigKeySmallItemVM : SmallItemVMBase, IClickHandler
    {
        private readonly IRequirement _spacerRequirement;
        private readonly IRequirement _requirement;
        private readonly IItem _item;

        public bool SpacerVisible =>
            _spacerRequirement.Met;
        public bool Visible =>
            _requirement.Met;
        public string ImageSource =>
            "avares://OpenTracker/Assets/Images/Items/bigkey" +
            (_item.Current > 0 ? "1" : "0") + ".png";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon whose big keys are to be represented.
        /// </param>
        public BigKeySmallItemVM(IDungeon dungeon)
        {
            if (dungeon == null)
            {
                throw new ArgumentNullException(nameof(dungeon));
            }

            if (dungeon.BigKeyItem == null)
            {
                throw new ArgumentOutOfRangeException(nameof(dungeon));
            }

            _item = dungeon.BigKeyItem;

            if (dungeon.ID == LocationID.HyruleCastle)
            {
                _spacerRequirement = new AlternativeRequirement(new List<IRequirement>
                {
                    new AggregateRequirement(new List<IRequirement>
                    {
                        new ItemsPanelOrientationRequirement(Orientation.Vertical),
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AlwaysDisplayDungeonItemsRequirement(true),
                            RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                        })
                    }),
                    new AggregateRequirement(new List<IRequirement>
                    {
                        RequirementDictionary.Instance[RequirementType.KeyDropShuffleOn],
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AlwaysDisplayDungeonItemsRequirement(true),
                            RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                        })
                    })
                });
                _requirement = new AggregateRequirement(new List<IRequirement>
                {
                    RequirementDictionary.Instance[RequirementType.KeyDropShuffleOn],
                    new AlternativeRequirement(new List<IRequirement>
                    {
                        new AlwaysDisplayDungeonItemsRequirement(true),
                        RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                    })
                });
            }
            else
            {
                _spacerRequirement = new AlternativeRequirement(new List<IRequirement>
                {
                    new AlwaysDisplayDungeonItemsRequirement(true),
                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                });
                _requirement = new AlternativeRequirement(new List<IRequirement>
                {
                    new AlwaysDisplayDungeonItemsRequirement(true),
                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                });
            }

            _item.PropertyChanged += OnItemChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
            _spacerRequirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(SpacerVisible));
            this.RaisePropertyChanged(nameof(Visible));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Handles left clicks and adds an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            UndoRedoManager.Instance.Execute(new AddItem(_item));
        }

        /// <summary>
        /// Handles right clicks and removes an item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            UndoRedoManager.Instance.Execute(new RemoveItem(_item));
        }
    }
}
