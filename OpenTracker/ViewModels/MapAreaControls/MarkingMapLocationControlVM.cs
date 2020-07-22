using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;

namespace OpenTracker.ViewModels.MapAreaControls
{
    /// <summary>
    /// This is the ViewModel for the marking map location control.
    /// </summary>
    public class MarkingMapLocationControlVM : ViewModelBase, IClickHandler
    {
        private readonly IMarkableSection _section;

        public string ImageSource
        {
            get
            {
                if (_section.Marking == null)
                {
                    return "avares://OpenTracker/Assets/Images/Items/unknown1.png";
                }

                switch (_section.Marking)
                {
                    case MarkingType.Bow:
                    case MarkingType.SilverArrows:
                    case MarkingType.Boomerang:
                    case MarkingType.RedBoomerang:
                    case MarkingType.SmallKey:
                    case MarkingType.BigKey:
                        {
                            return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                $"{_section.Marking.ToString().ToLowerInvariant()}.png";
                        }
                    case MarkingType.Hookshot:
                    case MarkingType.Bomb:
                    case MarkingType.Mushroom:
                    case MarkingType.Powder:
                    case MarkingType.FireRod:
                    case MarkingType.IceRod:
                    case MarkingType.Bombos:
                    case MarkingType.Ether:
                    case MarkingType.Quake:
                    case MarkingType.Shovel:
                    case MarkingType.Lamp:
                    case MarkingType.Hammer:
                    case MarkingType.Flute:
                    case MarkingType.Net:
                    case MarkingType.Book:
                    case MarkingType.MoonPearl:
                    case MarkingType.CaneOfSomaria:
                    case MarkingType.CaneOfByrna:
                    case MarkingType.Cape:
                    case MarkingType.Mirror:
                    case MarkingType.Boots:
                    case MarkingType.Flippers:
                    case MarkingType.HalfMagic:
                    case MarkingType.Aga:
                        {
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                $"{_section.Marking.ToString().ToLowerInvariant()}1.png";
                        }
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            var item = ItemDictionary.Instance[Enum.Parse<ItemType>(
                                _section.Marking.ToString())];

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _section.Marking.ToString().ToLowerInvariant() +
                                Math.Min(item.Current + 1, item.Maximum).ToString(
                                CultureInfo.InvariantCulture) + ".png";
                        }
                    case MarkingType.Sword:
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
                                _section.Marking.ToString().ToLowerInvariant() +
                                $"{itemNumber.ToString(CultureInfo.InvariantCulture)}.png";
                        }
                    case MarkingType.HCFront:
                    case MarkingType.HCLeft:
                    case MarkingType.HCRight:
                    case MarkingType.EP:
                    case MarkingType.SP:
                    case MarkingType.SW:
                    case MarkingType.DPFront:
                    case MarkingType.DPLeft:
                    case MarkingType.DPRight:
                    case MarkingType.DPBack:
                    case MarkingType.TT:
                    case MarkingType.IP:
                    case MarkingType.MM:
                    case MarkingType.TRFront:
                    case MarkingType.TRLeft:
                    case MarkingType.TRRight:
                    case MarkingType.TRBack:
                    case MarkingType.GT:
                        {
                            return "avares://OpenTracker/Assets/Images/" +
                                $"{_section.Marking.ToString().ToLowerInvariant()}.png";
                        }
                    case MarkingType.ToH:
                        {
                            return "avares://OpenTracker/Assets/Images/th.png";
                        }
                    case MarkingType.PoD:
                        {
                            return "avares://OpenTracker/Assets/Images/pd.png";
                        }
                    case MarkingType.Ganon:
                        {
                            return "avares://OpenTracker/Assets/Images/ganon.png";
                        }
                }

                return null;
            }
        }

        private bool _markingPopupOpen;
        public bool MarkingPopupOpen
        {
            get => _markingPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _markingPopupOpen, value);
        }

        public ObservableCollection<MarkingSelectControlVM> MarkingSelect { get; }
        public double MarkingSelectWidth { get; }
        public double MarkingSelectHeight { get; }

        public ReactiveCommand<MarkingType?, Unit> ChangeMarkingCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearVisibleItemCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The section marking to be represented.
        /// </param>
        /// <param name="markingSelect">
        /// The observable collection of marking select control ViewModel instances.
        /// </param>
        /// <param name="markingSelectWidth">
        /// A floating point number representing the width of the marking select popup.
        /// </param>
        /// <param name="markingSelectHeight">
        /// A floating point number representing the height of the marking select popup.
        /// </param>
        public MarkingMapLocationControlVM(
            IMarkableSection section, ObservableCollection<MarkingSelectControlVM> markingSelect,
            double markingSelectWidth, double markingSelectHeight)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));

            MarkingSelect = markingSelect ?? throw new ArgumentNullException(nameof(markingSelect));
            MarkingSelectWidth = markingSelectWidth;
            MarkingSelectHeight = markingSelectHeight;

            ChangeMarkingCommand = ReactiveCommand.Create<MarkingType?>(ChangeMarking);
            ClearVisibleItemCommand = ReactiveCommand.Create(ClearMarking);

            _section.PropertyChanging += OnSectionChanging;
            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanging event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanging event.
        /// </param>
        private void OnSectionChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(IMarkableSection.Marking))
            {
                UnsubscribeFromMarkingItem();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IBossSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMarkableSection.Marking))
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
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateImage();
            }
        }

        /// <summary>
        /// Unsubscribes from the marking item.
        /// </summary>
        private void UnsubscribeFromMarkingItem()
        {
            if (_section.Marking.HasValue)
            {
                switch (_section.Marking.Value)
                {
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(_section.Marking.Value.ToString());
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
            if (_section.Marking.HasValue)
            {
                switch (_section.Marking.Value)
                {
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            ItemType itemType = Enum.Parse<ItemType>(_section.Marking.Value.ToString());
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
        /// Clears the marking of the section.
        /// </summary>
        private void ClearMarking()
        {
            UndoRedoManager.Instance.Execute(new MarkSection(_section, null));
            MarkingPopupOpen = false;
        }

        /// <summary>
        /// Changes the marking of the section to the specified marking.
        /// </summary>
        /// <param name="marking">
        /// The marking to be set.
        /// </param>
        public void ChangeMarking(MarkingType? marking)
        {
            if (marking == null)
            {
                return;
            }

            UndoRedoManager.Instance.Execute(new MarkSection(_section, marking));
            MarkingPopupOpen = false;
        }

        /// <summary>
        /// Handles left clicks and opens the marking select popup.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            MarkingPopupOpen = true;
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
