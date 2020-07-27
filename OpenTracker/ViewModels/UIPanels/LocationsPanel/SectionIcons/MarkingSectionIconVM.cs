using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.Markings;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.UIPanels.LocationsPanel.SectionIcons
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing a section marking.
    /// </summary>
    public class MarkingSectionIconVM : SectionIconVMBase, IClickHandler
    {
        private readonly IMarking _marking;

        public string ImageSource
        {
            get
            {
                if (_marking.Mark == null)
                {
                    return "avares://OpenTracker/Assets/Images/Items/unknown1.png";
                }

                switch (_marking.Mark)
                {
                    case MarkType.Bow:
                    case MarkType.SilverArrows:
                    case MarkType.Boomerang:
                    case MarkType.RedBoomerang:
                    case MarkType.SmallKey:
                    case MarkType.BigKey:
                        {
                            return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                $"{_marking.Mark.ToString().ToLowerInvariant()}.png";
                        }
                    case MarkType.Hookshot:
                    case MarkType.Bomb:
                    case MarkType.Mushroom:
                    case MarkType.Powder:
                    case MarkType.FireRod:
                    case MarkType.IceRod:
                    case MarkType.Bombos:
                    case MarkType.Ether:
                    case MarkType.Quake:
                    case MarkType.Shovel:
                    case MarkType.Lamp:
                    case MarkType.Hammer:
                    case MarkType.Flute:
                    case MarkType.Net:
                    case MarkType.Book:
                    case MarkType.MoonPearl:
                    case MarkType.CaneOfSomaria:
                    case MarkType.CaneOfByrna:
                    case MarkType.Cape:
                    case MarkType.Mirror:
                    case MarkType.Boots:
                    case MarkType.Flippers:
                    case MarkType.HalfMagic:
                    case MarkType.Aga:
                        {
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                $"{_marking.Mark.ToString().ToLowerInvariant()}1.png";
                        }
                    case MarkType.Bottle:
                    case MarkType.Gloves:
                    case MarkType.Shield:
                    case MarkType.Mail:
                        {
                            var item = ItemDictionary.Instance[Enum.Parse<ItemType>(
                                _marking.Mark.ToString())];

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _marking.Mark.ToString().ToLowerInvariant() +
                                Math.Min(item.Current + 1, item.Maximum).ToString(
                                CultureInfo.InvariantCulture) + ".png";
                        }
                    case MarkType.Sword:
                        {
                            var sword = ItemDictionary.Instance[ItemType.Sword];
                            int itemNumber;

                            if (sword.Current == 0)
                            {
                                itemNumber = 0;
                            }
                            else
                            {
                                itemNumber = Math.Min(sword.Current + 1, sword.Maximum);
                            }

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _marking.Mark.ToString().ToLowerInvariant() +
                                $"{itemNumber.ToString(CultureInfo.InvariantCulture)}.png";
                        }
                    case MarkType.HCFront:
                    case MarkType.HCLeft:
                    case MarkType.HCRight:
                    case MarkType.EP:
                    case MarkType.SP:
                    case MarkType.SW:
                    case MarkType.DPFront:
                    case MarkType.DPLeft:
                    case MarkType.DPRight:
                    case MarkType.DPBack:
                    case MarkType.TT:
                    case MarkType.IP:
                    case MarkType.MM:
                    case MarkType.TRFront:
                    case MarkType.TRLeft:
                    case MarkType.TRRight:
                    case MarkType.TRBack:
                    case MarkType.GT:
                        {
                            return "avares://OpenTracker/Assets/Images/" +
                                $"{_marking.Mark.ToString().ToLowerInvariant()}.png";
                        }
                    case MarkType.ToH:
                        {
                            return "avares://OpenTracker/Assets/Images/th.png";
                        }
                    case MarkType.PoD:
                        {
                            return "avares://OpenTracker/Assets/Images/pd.png";
                        }
                    case MarkType.Ganon:
                        {
                            return "avares://OpenTracker/Assets/Images/ganon.png";
                        }
                }

                return null;
            }
        }

        public MarkingSelectVM MarkingSelect { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The marking to be represented.
        /// </param>
        public MarkingSectionIconVM(IMarkableSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            _marking = section.Marking;
            MarkingSelect = MarkingSelectVMFactory.GetMarkingSelectVM(section);

            _marking.PropertyChanging += OnMarkingChanging;
            _marking.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanging event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanging event.
        /// </param>
        private void OnMarkingChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
            {
                UnsubscribeFromMarkingItem();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMarking interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarking.Mark))
            {
                SubscribeToMarkingItem();
                UpdateImage();
            }
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
        private void OnMarkedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateImage();
        }

        /// <summary>
        /// Unsubscribes from the marking item.
        /// </summary>
        private void UnsubscribeFromMarkingItem()
        {
            if (_marking.Mark.HasValue)
            {
                switch (_marking.Mark.Value)
                {
                    case MarkType.Bottle:
                    case MarkType.Gloves:
                    case MarkType.Sword:
                    case MarkType.Shield:
                    case MarkType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(_marking.Mark.Value.ToString());
                            ItemDictionary.Instance[itemType].PropertyChanged -= OnMarkedItemChanged;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Subscribes to the marking item.
        /// </summary>
        private void SubscribeToMarkingItem()
        {
            if (_marking.Mark.HasValue)
            {
                switch (_marking.Mark.Value)
                {
                    case MarkType.Bottle:
                    case MarkType.Gloves:
                    case MarkType.Sword:
                    case MarkType.Shield:
                    case MarkType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(_marking.Mark.Value.ToString());
                            ItemDictionary.Instance[itemType].PropertyChanged += OnMarkedItemChanged;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Handles left clicks and opens the marking select popup.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            MarkingSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
        }
    }
}
