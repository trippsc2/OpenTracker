using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Actions;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class SectionControlVM : ViewModelBase, IChangeMarking, IClickHandler, IOpenMarkingSelect
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettings _appSettings;
        private readonly Game _game;
        private readonly ISection _section;

        public ObservableCollection<MarkingSelectControlVM> ItemSelect
        {
            get
            {
                if (_section is EntranceSection)
                    return MainWindowVM.EntranceMarkingSelect;
                else
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
                    return 280.0;
                else
                    return 200.0;
            }
        }

        public double MarkingPopupWidth
        {
            get
            {
                if (_section is EntranceSection)
                    return 272.0;
                else
                    return 238.0;
            }
        }

        public bool SectionVisible
        {
            get
            {
                if (_section is ItemSection itemSection && itemSection.Total <= 0)
                    return false;

                if (_section is DungeonItemSection dungeonItemSection &&
                    dungeonItemSection.Total <= 0)
                    return false;

                if (_section is BossSection bossSection)
                {
                    if ((bossSection.BossPlacement.Boss == null ||
                        bossSection.BossPlacement.Boss.Type != BossType.Aga) &&
                        !_game.Mode.BossShuffle.Value)
                        return false;
                }

                return _game.Mode.Validate(_section.RequiredMode);
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
                    return "avares://OpenTracker/Assets/Images/Items/unknown1.png";
                else
                {
                    int itemNumber;
                    switch (_section.Marking)
                    {
                        case MarkingType.Bow:
                        case MarkingType.SilverArrows:
                        case MarkingType.Boomerang:
                        case MarkingType.RedBoomerang:
                        case MarkingType.SmallKey:
                        case MarkingType.BigKey:
                            return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                _section.Marking.ToString().ToLowerInvariant() + ".png";
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
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _section.Marking.ToString().ToLowerInvariant() + "1.png";
                        case MarkingType.Bottle:
                        case MarkingType.Gloves:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                            Item item = _game.Items[Enum.Parse<ItemType>(_section.Marking.ToString())];
                            itemNumber = Math.Min(item.Current + 1, item.Maximum);
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _section.Marking.ToString().ToLowerInvariant() +
                                itemNumber.ToString(CultureInfo.InvariantCulture) + ".png";
                        case MarkingType.Sword:

                            Item sword = _game.Items[ItemType.Sword];

                            if (sword.Current == 0)
                                itemNumber = 0;
                            else
                                itemNumber = Math.Min(sword.Current + 1, sword.Maximum);

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _section.Marking.ToString().ToLowerInvariant() +
                                itemNumber.ToString(CultureInfo.InvariantCulture) + ".png";

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
                            return "avares://OpenTracker/Assets/Images/" +
                                _section.Marking.ToString().ToLowerInvariant() + ".png";
                        case MarkingType.ToH:
                            return "avares://OpenTracker/Assets/Images/th.png";
                        case MarkingType.PoD:
                            return "avares://OpenTracker/Assets/Images/pd.png";
                        case MarkingType.Ganon:
                            return "avares://OpenTracker/Assets/Images/ganon.png";
                    }

                    return null;
                }

            }
        }

        public bool SectionImageVisible
        {
            get
            {
                if (_section is BossSection bossSection)
                {
                    if (bossSection.PrizeVisible)
                        return true;

                    return false;
                }

                return true;
            }
        }

        public string ImageSource
        {
            get
            {
                switch (_section)
                {
                    case TakeAnySection _:
                    case ItemSection _:
                    case DungeonItemSection _:

                        if (_section.IsAvailable())
                        {
                            switch (_section.Accessibility)
                            {
                                case AccessibilityLevel.None:
                                case AccessibilityLevel.Inspect:
                                    return "avares://OpenTracker/Assets/Images/chest0.png";
                                case AccessibilityLevel.Partial:
                                case AccessibilityLevel.SequenceBreak:
                                case AccessibilityLevel.Normal:
                                    return "avares://OpenTracker/Assets/Images/chest1.png";
                            }
                        }
                        else
                            return "avares://OpenTracker/Assets/Images/chest2.png";

                        break;

                    case BossSection bossSection:

                        string imageBaseString = "avares://OpenTracker/Assets/Images/Items/";

                        if (bossSection.Prize == null)
                            imageBaseString += "unknown";
                        else if (bossSection.Prize.Type == ItemType.Aga2)
                            imageBaseString += "aga";
                        else
                            imageBaseString += bossSection.Prize.Type.ToString().ToLowerInvariant();

                        return imageBaseString + (bossSection.IsAvailable() ? "0" : "1") + ".png";

                    case EntranceSection entranceSection:

                        string entranceImageBaseString = "avares://OpenTracker/Assets/Images/door";

                        return entranceImageBaseString + (entranceSection.IsAvailable() ? "0" : "1") + ".png";
                }

                return null;
            }
        }

        public string NumberString
        {
            get
            {
                if (_section is ItemSection || _section is DungeonItemSection)
                    return _section.Available.ToString(CultureInfo.InvariantCulture);
                else
                    return null;
            }
        }

        public bool BossImageVisible
        {
            get
            {
                if (_game.Mode.BossShuffle.Value && _section is BossSection bossSection &&
                    (bossSection.BossPlacement.Boss == null || bossSection.BossPlacement.Boss.Type != BossType.Aga))
                    return true;
                else
                    return false;
            }
        }

        public string BossImageSource
        {
            get
            {
                if (_section is BossSection bossSection)
                {
                    string imageBaseString = "avares://OpenTracker/Assets/Images/";

                    if (bossSection.BossPlacement.Boss == null)
                        imageBaseString += "Items/unknown1";
                    else if (bossSection.BossPlacement.Boss.Type == BossType.Aga)
                        imageBaseString += "Items/aga1";
                    else
                    {
                        imageBaseString += "Bosses/";
                        imageBaseString += bossSection.BossPlacement.Boss.Type.ToString().ToLowerInvariant();
                    }

                    return imageBaseString + ".png";
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

        public SectionControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            Game game, ISection section)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _section = section ?? throw new ArgumentNullException(nameof(section));

            ChangeMarkingCommand = ReactiveCommand.Create<MarkingType?>(ChangeMarking);
            ClearVisibleItemCommand = ReactiveCommand.Create(ClearMarking);

            _appSettings.AccessibilityColors.PropertyChanged += OnColorChanged;
            _game.Mode.PropertyChanged += OnModeChanged;
            _section.PropertyChanging += OnSectionChanging;
            _section.PropertyChanged += OnSectionChanged;

            if (_section is BossSection bossSection)
                bossSection.BossPlacement.PropertyChanged += OnBossChanged;

            SubscribeToMarkingItem();
        }

        private void OnColorChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateTextColor();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
                UpdateBossVisibility();

            this.RaisePropertyChanged(nameof(SectionVisible));
        }

        private void OnSectionChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Marking))
                UnsubscribeFromMarkingItem();
        }

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
                    this.RaisePropertyChanged(nameof(NumberString));

                UpdateImage();
            }

            if (e.PropertyName == nameof(ISection.Marking))
            {
                UpdateMarkingImage();
                SubscribeToMarkingItem();
            }

            if (e.PropertyName == nameof(BossSection.Prize))
                UpdateImage();
        }

        private void OnMarkedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateMarkingImage();
        }

        private void OnBossChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BossPlacement.Boss))
            {
                UpdateBossVisibility();
                this.RaisePropertyChanged(nameof(BossImageSource));
            }
        }

        private void UpdateTextColor()
        {
            this.RaisePropertyChanged(nameof(FontColor));
            this.RaisePropertyChanged(nameof(NormalAccessibility));
        }

        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        private void UpdateBossVisibility()
        {
            this.RaisePropertyChanged(nameof(SectionVisible));
            this.RaisePropertyChanged(nameof(BossImageVisible));
        }

        private void UpdateMarkingImage()
        {
            this.RaisePropertyChanged(nameof(MarkingSource));
        }

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
                        ItemType itemType = Enum.Parse<ItemType>(_section.Marking.Value.ToString());
                        _game.Items[itemType].PropertyChanged -= OnMarkedItemChanged;
                        break;
                }
            }
        }

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
                        ItemType itemType = Enum.Parse<ItemType>(_section.Marking.Value.ToString());
                        _game.Items[itemType].PropertyChanged += OnMarkedItemChanged;
                        break;
                }
            }
        }

        private void ClearMarking()
        {
            _undoRedoManager.Execute(new MarkSection(_section, null));
            MarkingPopupOpen = false;
        }

        private void CollectSection()
        {
            _undoRedoManager.Execute(new CollectSection(_game, _section));
        }

        private void UncollectSection()
        {
            _undoRedoManager.Execute(new UncollectSection(_section));
        }

        public void ChangeMarking(MarkingType? marking)
        {
            if (marking != null)
            {
                if (_section.Marking != null)
                {
                    if (Enum.TryParse(_section.Marking.ToString(), out ItemType currentItemType))
                    {
                        _game.Items[currentItemType].PropertyChanged -= OnMarkedItemChanged;

                        switch (currentItemType)
                        {
                            case ItemType.Bombos:
                            case ItemType.Ether:
                            case ItemType.Quake:
                                _game.Items[currentItemType + 1].PropertyChanged -= OnMarkedItemChanged;
                                break;
                        }
                    }
                }

                _undoRedoManager.Execute(new MarkSection(_section, marking));

                if (Enum.TryParse(_section.Marking.ToString(), out ItemType newItemType))
                {
                    _game.Items[newItemType].PropertyChanged += OnMarkedItemChanged;

                    switch (newItemType)
                    {
                        case ItemType.Bombos:
                        case ItemType.Ether:
                        case ItemType.Quake:
                            _game.Items[newItemType + 1].PropertyChanged += OnMarkedItemChanged;
                            break;
                    }
                }

                MarkingPopupOpen = false;
            }
        }

        public void OpenMarkingSelect()
        {
            MarkingPopupOpen = true;
        }

        public void OnLeftClick(bool force)
        {
            if ((_section is EntranceSection || force || (_section is BossSection bossSection &&
                bossSection.Prize != null && bossSection.Prize.Type == ItemType.Aga2) ||
                _section.Accessibility >= AccessibilityLevel.Partial) && _section.IsAvailable())
                CollectSection();
        }

        public void OnRightClick(bool force)
        {
            switch (_section)
            {
                case BossSection _:
                case EntranceSection _:
                case TakeAnySection _:
                    if (!_section.IsAvailable())
                        UncollectSection();
                    break;
                case ItemSection itemSection:
                    if (_section.Available < itemSection.Total)
                        UncollectSection();
                    break;
                case DungeonItemSection dungeonItemSection:
                    if (_section.Available < dungeonItemSection.Total)
                        UncollectSection();
                    break;
            }
        }
    }
}
