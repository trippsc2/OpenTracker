using Avalonia.Media;
using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class SectionControlVM : ViewModelBase, ISectionControlVM
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly AppSettingsVM _appSettings;
        private readonly Game _game;
        private readonly ISection _section;

        public string Name { get => _section.Name; }
        public bool MarkingVisible { get; }
        public bool NumberBoxVisible { get; }
        public double MarkingPopupHeight { get; }
        public double MarkingPopupWidth { get; }
        public ObservableCollection<MarkingSelectControlVM> ItemSelect { get; }

        private bool _sectionVisible;
        public bool SectionVisible
        {
            get => _sectionVisible;
            private set => this.RaiseAndSetIfChanged(ref _sectionVisible, value);
        }

        private IBrush _fontColor;
        public IBrush FontColor
        {
            get => _fontColor;
            set => this.RaiseAndSetIfChanged(ref _fontColor, value);
        }

        private string _markingSource;
        public string MarkingSource
        {
            get => _markingSource;
            set => this.RaiseAndSetIfChanged(ref _markingSource, value);
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        private string _numberString;
        public string NumberString
        {
            get => _numberString;
            set => this.RaiseAndSetIfChanged(ref _numberString, value);
        }

        private bool _markingPopupOpen;
        public bool MarkingPopupOpen
        {
            get => _markingPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _markingPopupOpen, value);
        }

        private bool _bossImageVisible;
        public bool BossImageVisible
        {
            get => _bossImageVisible;
            set => this.RaiseAndSetIfChanged(ref _bossImageVisible, value);
        }

        private string _bossImageSource;
        public string BossImageSource
        {
            get => _bossImageSource;
            set => this.RaiseAndSetIfChanged(ref _bossImageSource, value);
        }

        public ReactiveCommand<Unit, Unit> ClearVisibleItemCommand { get; }

        public SectionControlVM(UndoRedoManager undoRedoManager, AppSettingsVM appSettings,
            Game game, ISection section)
        {
            _undoRedoManager = undoRedoManager;
            _appSettings = appSettings;
            _game = game;
            _section = section;

            ClearVisibleItemCommand = ReactiveCommand.Create(ClearMarking);

            ItemSelect = new ObservableCollection<MarkingSelectControlVM>();

            if (_section.HasMarking)
                MarkingVisible = true;

            switch (_section)
            {
                case ItemSection _:

                    if (_section.HasMarking)
                    {
                        for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                        {
                            switch ((MarkingType)i)
                            {
                                case MarkingType.Bow:
                                case MarkingType.SilverArrows:
                                case MarkingType.Boomerang:
                                case MarkingType.RedBoomerang:
                                case MarkingType.Hookshot:
                                case MarkingType.Bomb:
                                case MarkingType.Mushroom:
                                case MarkingType.FireRod:
                                case MarkingType.IceRod:
                                case MarkingType.Bombos:
                                case MarkingType.Ether:
                                case MarkingType.Quake:
                                case MarkingType.Shovel:
                                case MarkingType.Powder:
                                case MarkingType.Lamp:
                                case MarkingType.Hammer:
                                case MarkingType.Flute:
                                case MarkingType.Net:
                                case MarkingType.Book:
                                case MarkingType.Bottle:
                                case MarkingType.CaneOfSomaria:
                                case MarkingType.CaneOfByrna:
                                case MarkingType.Cape:
                                case MarkingType.Gloves:
                                case MarkingType.Boots:
                                case MarkingType.Flippers:
                                case MarkingType.HalfMagic:
                                case MarkingType.Sword:
                                case MarkingType.Shield:
                                case MarkingType.Mail:
                                    ItemSelect.Add(new MarkingSelectControlVM(_game, this, (MarkingType)i));
                                    break;
                                case MarkingType.MoonPearl:
                                    ItemSelect.Add(new MarkingSelectControlVM(_game, this, (MarkingType)i));
                                    ItemSelect.Add(new MarkingSelectControlVM(_game, this, null));
                                    break;
                                case MarkingType.Mirror:
                                    ItemSelect.Add(new MarkingSelectControlVM(_game, this, (MarkingType)i));
                                    ItemSelect.Add(new MarkingSelectControlVM(_game, this, MarkingType.SmallKey));
                                    ItemSelect.Add(new MarkingSelectControlVM(_game, this, MarkingType.BigKey));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    MarkingPopupHeight = 200;
                    MarkingPopupWidth = 238;
                    NumberBoxVisible = true;

                    UpdateTextColor();
                    UpdateNumber();

                    break;
                case BossSection _:

                    UpdateBossImage();

                    break;
                case EntranceSection _:

                    if (_section.HasMarking)
                    {
                        for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                            ItemSelect.Add(new MarkingSelectControlVM(_game, this, (MarkingType)i));
                    }

                    MarkingPopupHeight = 280;
                    MarkingPopupWidth = 272;

                    break;
            }

            _appSettings.PropertyChanged += OnAppSettingsChanged;
            _game.Mode.PropertyChanged += OnModeChanged;
            _section.PropertyChanging += OnSectionChanging;
            _section.PropertyChanged += OnSectionChanged;

            UpdateTextColor();
            UpdateImage();
            UpdateBossVisibility();
            UpdateMarkingImage();
            UpdateVisibility();
            SubscribeToMarkingItem();
        }

        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(AppSettingsVM.EmphasisFontColor) &&
                e.PropertyName != nameof(AppSettingsVM.DisplayAllLocations))
                UpdateTextColor();
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
                UpdateBossVisibility();

            UpdateVisibility();
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
                if (_section is ItemSection)
                    UpdateNumber();

                UpdateImage();
            }

            if (e.PropertyName == nameof(ISection.Marking))
            {
                UpdateMarkingImage();
                SubscribeToMarkingItem();
            }

            if (e.PropertyName == nameof(BossSection.Boss))
            {
                UpdateBossImage();
                UpdateBossVisibility();
            }

            if (e.PropertyName == nameof(BossSection.Prize))
                UpdateImage();
        }

        private void OnMarkedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateMarkingImage();
        }

        private void UpdateTextColor()
        {
            if (_section.Accessibility == AccessibilityLevel.Normal)
                FontColor = new SolidColorBrush(new Color(255, 255, 255, 255));
            else
                FontColor = _appSettings.AccessibilityColors[_section.Accessibility];
        }

        private void UpdateNumber()
        {
            NumberString = _section.Available.ToString();
        }

        private void UpdateImage()
        {
            switch (_section)
            {
                case ItemSection _:

                    if (_section.IsAvailable())
                    {
                        switch (_section.Accessibility)
                        {
                            case AccessibilityLevel.None:
                            case AccessibilityLevel.Inspect:
                                ImageSource = "avares://OpenTracker/Assets/Images/chest0.png";
                                break;
                            case AccessibilityLevel.Partial:
                            case AccessibilityLevel.SequenceBreak:
                            case AccessibilityLevel.Normal:
                                ImageSource = "avares://OpenTracker/Assets/Images/chest1.png";
                                break;
                        }
                    }
                    else
                        ImageSource = "avares://OpenTracker/Assets/Images/chest2.png";

                    break;

                case BossSection bossSection:

                    string imageBaseString = "avares://OpenTracker/Assets/Images/Items/";

                    if (bossSection.Prize == null)
                        imageBaseString += "unknown";
                    else if (bossSection.Prize.Type == ItemType.Aga2)
                        imageBaseString += "aga";
                    else
                        imageBaseString += bossSection.Prize.Type.ToString().ToLower();

                    ImageSource = imageBaseString + (bossSection.IsAvailable() ? "0" : "1") + ".png";

                    break;

                case EntranceSection entranceSection:

                    string entranceImageBaseString = "avares://OpenTracker/Assets/Images/door";

                    ImageSource = entranceImageBaseString + (entranceSection.IsAvailable() ? "0" : "1") + ".png";

                    break;
            }
        }

        private void UpdateBossImage()
        {
            if (_section is BossSection bossSection)
            {
                string imageBaseString = "avares://OpenTracker/Assets/Images/";

                if (bossSection.Boss == null)
                    imageBaseString += "Items/unknown1";
                else if (bossSection.Boss.Type == BossType.Aga)
                    imageBaseString += "Items/aga1";
                else
                {
                    imageBaseString += "Bosses/";
                    imageBaseString += bossSection.Boss.Type.ToString().ToLower();
                }

                BossImageSource = imageBaseString + ".png";
            }
        }

        private void UpdateBossVisibility()
        {
            if (_game.Mode.BossShuffle.Value && _section is BossSection bossSection &&
                bossSection.Boss.Type != BossType.Aga)
                BossImageVisible = true;
            else
                BossImageVisible = false;
        }

        private void UpdateMarkingImage()
        {
            if (_section.Marking == null)
                MarkingSource = "avares://OpenTracker/Assets/Images/Items/unknown1.png";
            else
            {
                int itemNumber = 0;
                switch (_section.Marking)
                {
                    case MarkingType.Bow:
                    case MarkingType.SilverArrows:
                    case MarkingType.Boomerang:
                    case MarkingType.RedBoomerang:
                    case MarkingType.SmallKey:
                    case MarkingType.BigKey:
                        MarkingSource = "avares://OpenTracker/Assets/Images/Items/visible-" +
                            _section.Marking.ToString().ToLower() + ".png";
                        break;
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
                        MarkingSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _section.Marking.ToString().ToLower() + "1.png";
                        break;
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        Item item = _game.Items[Enum.Parse<ItemType>(_section.Marking.ToString())];
                        itemNumber = Math.Min(item.Current + 1, item.Maximum);
                        MarkingSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _section.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";
                        break;
                    case MarkingType.Sword:

                        Item sword = _game.Items[ItemType.Sword];

                        if (sword.Current == 0)
                            itemNumber = 0;
                        else
                            itemNumber = Math.Min(sword.Current + 1, sword.Maximum);

                        MarkingSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _section.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";

                        break;
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
                        MarkingSource = "avares://OpenTracker/Assets/Images/" +
                            _section.Marking.ToString().ToLower() + ".png";
                        break;
                    case MarkingType.ToH:
                        MarkingSource = "avares://OpenTracker/Assets/Images/th.png";
                        break;
                    case MarkingType.PoD:
                        MarkingSource = "avares://OpenTracker/Assets/Images/pd.png";
                        break;
                    case MarkingType.Ganon:
                        MarkingSource = "avares://OpenTracker/Assets/Images/ganon.png";
                        break;
                }
            }
        }

        private void UpdateVisibility()
        {
            if (_game.Mode.Validate(_section.RequiredMode))
                SectionVisible = true;
            else
                SectionVisible = false;
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

        public void ChangeAvailable(bool rightClick = false)
        {
            if (rightClick)
            {
                switch (_section)
                {
                    case BossSection bossSectionUncollect:
                        if (!bossSectionUncollect.IsAvailable())
                            _undoRedoManager.Execute(new UncollectSection(_section));
                        break;
                    case EntranceSection entranceSectionUncollect:
                        if (!entranceSectionUncollect.IsAvailable())
                            _undoRedoManager.Execute(new UncollectSection(_section));
                        break;
                    case ItemSection itemSectionUncollect:
                        if (itemSectionUncollect.Available < itemSectionUncollect.Total)
                            _undoRedoManager.Execute(new UncollectSection(_section));
                        break;
                }
            }
            else
            {
                if (_section is EntranceSection ||
                    (_section is BossSection bossSection &&
                    bossSection.Prize != null && bossSection.Prize.Type == ItemType.Aga2) ||
                    _section.Accessibility >= AccessibilityLevel.Partial)
                {
                    if (_section.IsAvailable())
                        _undoRedoManager.Execute(new CollectSection(_game, _section));
                }
            }
        }
    }
}
