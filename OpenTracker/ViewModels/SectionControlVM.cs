using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;
using System.Text;
using OpenTracker.ViewModels.Bases;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.ViewModels
{
    public class SectionControlVM : ViewModelBase, IChangeMarking, IClickHandler,
        IOpenMarkingSelect
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly ISection _section;

        public ObservableCollection<MarkingSelectControlVM> ItemSelect
        {
            get
            {
                if (_section is EntranceSection)
                {
                    return MainWindowVM.EntranceMarkingSelect;
                }
                
                return MainWindowVM.NonEntranceMarkingSelect;
            }
        }

        public string Name =>
            _section.Name;

        public bool MarkingVisible =>
            _section.HasMarking;
        public bool NumberBoxVisible =>
            _section is ItemSection || _section is DungeonItemSection;

        public double MarkingPopupHeight
        {
            get
            {
                if (_section is EntranceSection)
                {
                    return 280.0;
                }
                
                return 200.0;
            }
        }

        public double MarkingPopupWidth
        {
            get
            {
                if (_section is EntranceSection)
                {
                    return 272.0;
                }
                
                return 238.0;
            }
        }

        public bool SectionVisible
        {
            get
            {
                if (_section is ItemSection itemSection && itemSection.Total <= 0)
                {
                    return false;
                }

                if (_section is DungeonItemSection dungeonItemSection &&
                    dungeonItemSection.Total <= 0)
                {
                    return false;
                }

                if (_section is BossSection bossSection && !bossSection.PrizeVisible &&
                    !Mode.Instance.BossShuffle)
                {
                    return false;
                }

                return Mode.Instance.Validate(_section.ModeRequirement);
            }
        }

        public Color FontColor =>
            Color.Parse(_appSettings.AccessibilityColors[_section.Accessibility]);

        public bool NormalAccessibility =>
            _section.Accessibility == AccessibilityLevel.Normal;

        public string MarkingSource
        {
            get
            {
                if (_section.Marking == null)
                {
                    return "avares://OpenTracker/Assets/Images/Items/unknown1.png";
                }
                
                int itemNumber;

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
                                $"{ _section.Marking.ToString().ToLowerInvariant() }.png";
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
                                $"{ _section.Marking.ToString().ToLowerInvariant() }1.png";
                        }
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        {
                            var item = ItemDictionary.Instance[Enum.Parse<ItemType>(_section.Marking.ToString())];
                            itemNumber = Math.Min(item.Current + 1, item.Maximum);

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _section.Marking.ToString().ToLowerInvariant() +
                                $"{ itemNumber.ToString(CultureInfo.InvariantCulture) }.png";
                        }
                    case MarkingType.Sword:
                        {
                            var sword = ItemDictionary.Instance[ItemType.Sword];

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
                                $"{ itemNumber.ToString(CultureInfo.InvariantCulture) }.png";
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
                                $"{ _section.Marking.ToString().ToLowerInvariant() }.png";
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

        public bool SectionImageVisible
        {
            get
            {
                if (_section is BossSection bossSection)
                {
                    if (bossSection.PrizeVisible)
                    {
                        return true;
                    }

                    return false;
                }

                return true;
            }
        }

        public string ImageSource
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                switch (_section)
                {
                    case TakeAnySection _:
                    case ItemSection _:
                    case DungeonItemSection _:
                        {
                            if (_section.IsAvailable())
                            {
                                switch (_section.Accessibility)
                                {
                                    case AccessibilityLevel.None:
                                    case AccessibilityLevel.Inspect:
                                        {
                                            return "avares://OpenTracker/Assets/Images/chest0.png";
                                        }
                                    case AccessibilityLevel.Partial:
                                    case AccessibilityLevel.SequenceBreak:
                                    case AccessibilityLevel.Normal:
                                        {
                                            return "avares://OpenTracker/Assets/Images/chest1.png";
                                        }
                                }
                            }
                            
                            return "avares://OpenTracker/Assets/Images/chest2.png";
                        }
                    case BossSection bossSection:
                        {
                            sb.Append("avares://OpenTracker/Assets/Images/Items/");

                            if (bossSection.Prize == null)
                            {
                                sb.Append("unknown");
                            }
                            else if (bossSection.Prize.Type == ItemType.Aga2)
                            {
                                sb.Append("aga");
                            }
                            else
                            {
                                sb.Append(bossSection.Prize.Type.ToString().ToLowerInvariant());
                            }

                            sb.Append(bossSection.IsAvailable() ? "0" : "1");
                        }
                        break;
                    case EntranceSection entranceSection:
                        {
                            sb.Append("avares://OpenTracker/Assets/Images/door");
                            sb.Append(entranceSection.IsAvailable() ? "0" : "1");
                        }
                        break;
                }

                sb.Append(".png");

                return sb.ToString();
            }
        }

        public string NumberString
        {
            get
            {
                if (_section is ItemSection || _section is DungeonItemSection)
                {
                    return _section.Available.ToString(CultureInfo.InvariantCulture);
                }
                
                return null;
            }
        }

        public bool BossImageVisible
        {
            get
            {
                return Mode.Instance.BossShuffle && _section is BossSection bossSection &&
                    (bossSection.BossPlacement.Boss == null ||
                    bossSection.BossPlacement.Boss != BossType.Aga);
            }
        }

        public string BossImageSource
        {
            get
            {
                if (_section is BossSection bossSection)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("avares://OpenTracker/Assets/Images/");

                    if (!bossSection.BossPlacement.Boss.HasValue)
                    {
                        sb.Append("Items/unknown1");
                    }
                    else if (bossSection.BossPlacement.Boss == BossType.Aga)
                    {
                        sb.Append("Items/aga1");
                    }
                    else
                    {
                        sb.Append($"Bosses/{bossSection.BossPlacement.Boss.ToString().ToLowerInvariant()}");
                    }

                    sb.Append(".png");

                    return sb.ToString();
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

        public ReactiveCommand<MarkingType?, Unit> ChangeMarkingCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearVisibleItemCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="appSettings">
        /// The app settings.
        /// </param>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="section">
        /// The section to be represented.
        /// </param>
        public SectionControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            ISection section)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _section = section ?? throw new ArgumentNullException(nameof(section));

            ChangeMarkingCommand = ReactiveCommand.Create<MarkingType?>(ChangeMarking);
            ClearVisibleItemCommand = ReactiveCommand.Create(ClearMarking);

            _appSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            Mode.Instance.PropertyChanged += OnModeChanged;
            _section.PropertyChanging += OnSectionChanging;
            _section.PropertyChanged += OnSectionChanged;

            if (_section is BossSection bossSection)
            {
                bossSection.BossPlacement.PropertyChanged += OnBossChanged;
            }

            SubscribeToMarkingItem();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ObservableCollection for the
        /// accessibility colors.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
            {
                UpdateBossVisibility();
            }

            this.RaisePropertyChanged(nameof(SectionVisible));
        }

        /// <summary>
        /// Subscribes to the PropertyChanging event on the ISection-implementing classes.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanging event.
        /// </param>
        private void OnSectionChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Marking))
            {
                UnsubscribeFromMarkingItem();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection-implementing classes.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Accessibility))
            {
                UpdateTextColor();
                UpdateImage();
            }

            if (e.PropertyName == nameof(ISection.Available))
            {
                if (_section is ItemSection || _section is DungeonItemSection)
                {
                    this.RaisePropertyChanged(nameof(NumberString));
                }

                UpdateImage();
            }

            if (e.PropertyName == nameof(ISection.Marking))
            {
                UpdateMarkingImage();
                SubscribeToMarkingItem();
            }

            if (e.PropertyName == nameof(BossSection.Prize))
            {
                UpdateImage();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Item class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnMarkedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateMarkingImage();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the BossSection class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnBossChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossPlacement.Boss))
            {
                UpdateBossVisibility();
                this.RaisePropertyChanged(nameof(BossImageSource));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the FontColor and NormalAccessibility properties.
        /// </summary>
        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(FontColor));
            this.RaisePropertyChanged(nameof(NormalAccessibility));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the SectionVisible and BossImageVisible properties.
        /// </summary>
        private void UpdateBossVisibility()
        {
            this.RaisePropertyChanged(nameof(SectionVisible));
            this.RaisePropertyChanged(nameof(BossImageVisible));
        }

        /// <summary>
        /// Raises the PropertyChanged event for the MarkingSource property.
        /// </summary>
        private void UpdateMarkingImage()
        {
            this.RaisePropertyChanged(nameof(MarkingSource));
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
        /// Clears the marking of the first section of the location.
        /// </summary>
        private void ClearMarking()
        {
            _undoRedoManager.Execute(new MarkSection(_section, null));
            MarkingPopupOpen = false;
        }

        /// <summary>
        /// Collects a single item from the section represented.
        /// </summary>
        private void CollectSection()
        {
            _undoRedoManager.Execute(new CollectSection(_section));
        }

        /// <summary>
        /// Uncollects a single item from the section represented.
        /// </summary>
        private void UncollectSection()
        {
            _undoRedoManager.Execute(new UncollectSection(_section));
        }

        /// <summary>
        /// Changes the marking of the first section of the location to the specified
        /// marking.
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

            if (_section.Marking != null)
            {
                if (Enum.TryParse(_section.Marking.ToString(), out ItemType currentItemType))
                {
                    ItemDictionary.Instance[currentItemType].PropertyChanged -= OnMarkedItemChanged;

                    switch (currentItemType)
                    {
                        case ItemType.Bombos:
                        case ItemType.Ether:
                        case ItemType.Quake:
                            {
                                ItemDictionary.Instance[currentItemType + 1].PropertyChanged -= OnMarkedItemChanged;
                            }
                            break;
                    }
                }
            }
            
            _undoRedoManager.Execute(new MarkSection(_section, marking));

            if (Enum.TryParse(_section.Marking.ToString(), out ItemType newItemType))
            {
                ItemDictionary.Instance[newItemType].PropertyChanged += OnMarkedItemChanged;

                switch (newItemType)
                {
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                        {
                            ItemDictionary.Instance[newItemType + 1].PropertyChanged += OnMarkedItemChanged;
                        }
                        break;
                }
            }
            
            MarkingPopupOpen = false;
        }

        /// <summary>
        /// Opens the Marking Select popup.
        /// </summary>
        public void OpenMarkingSelect()
        {
            MarkingPopupOpen = true;
        }

        /// <summary>
        /// Click handler for left click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            if ((_section is EntranceSection || force || (_section is BossSection bossSection &&
                bossSection.Prize != null && bossSection.Prize.Type == ItemType.Aga2) ||
                _section.Accessibility >= AccessibilityLevel.Partial) && _section.IsAvailable())
            {
                CollectSection();
            }
        }

        /// <summary>
        /// Click handler for right click.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            switch (_section)
            {
                case BossSection _:
                case EntranceSection _:
                case TakeAnySection _:
                    {
                        if (!_section.IsAvailable())
                        {
                            UncollectSection();
                        }
                    }
                    break;
                case ItemSection itemSection:
                    {
                        if (_section.Available < itemSection.Total)
                        {
                            UncollectSection();
                        }
                    }
                    break;
                case DungeonItemSection dungeonItemSection:
                    {
                        if (_section.Available < dungeonItemSection.Total)
                        {
                            UncollectSection();
                        }
                    }
                    break;
            }
        }
    }
}
