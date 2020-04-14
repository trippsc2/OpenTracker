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
        public bool VisibleItem { get; }
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

        private string _itemImageSource;
        public string ItemImageSource
        {
            get => _itemImageSource;
            set => this.RaiseAndSetIfChanged(ref _itemImageSource, value);
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
            {
                VisibleItem = true;

                if (_section is ItemSection)
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

                if (_section is EntranceSection)
                {
                    for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
                        ItemSelect.Add(new MarkingSelectControlVM(_game, this, (MarkingType)i));
                }
            }

            if (_section is ItemSection itemSection)
            {
                MarkingPopupHeight = 200;
                MarkingPopupWidth = 238;
                NumberBoxVisible = true;
            }

            if (_section is EntranceSection)
            {
                MarkingPopupHeight = 280;
                MarkingPopupWidth = 272;
            }

            _game.Mode.PropertyChanged += OnModeChanged;
            _section.PropertyChanged += OnSectionChanged;

            Update();
        }

        private void ClearMarking()
        {
            _undoRedoManager.Execute(new MarkSection(_section, null));
            MarkingPopupOpen = false;
        }

        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        private void OnVisibleItemChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            AccessibilityLevel accessibility = _section.Accessibility;

            if (accessibility == AccessibilityLevel.Normal)
                FontColor = new SolidColorBrush(new Color(255, 245, 245, 245));
            else
                FontColor = _appSettings.AccessibilityColors[accessibility];

            if (_section.Marking == null)
                ItemImageSource = "avares://OpenTracker/Assets/Images/Items/visible-empty.png";
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
                        ItemImageSource = "avares://OpenTracker/Assets/Images/Items/visible-" +
                            _section.Marking.ToString().ToLower() + ".png";
                        break;
                    case MarkingType.Hookshot:
                    case MarkingType.Bomb:
                    case MarkingType.Powder:
                    case MarkingType.FireRod:
                    case MarkingType.IceRod:
                    case MarkingType.Shovel:
                    case MarkingType.Lamp:
                    case MarkingType.Hammer:
                    case MarkingType.Flute:
                    case MarkingType.Net:
                    case MarkingType.Book:
                    case MarkingType.MoonPearl:
                    case MarkingType.Bottle:
                    case MarkingType.CaneOfSomaria:
                    case MarkingType.CaneOfByrna:
                    case MarkingType.Cape:
                    case MarkingType.Mirror:
                    case MarkingType.Gloves:
                    case MarkingType.Boots:
                    case MarkingType.Flippers:
                    case MarkingType.HalfMagic:
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                    case MarkingType.Aga:
                        Item item = _game.Items[Enum.Parse<ItemType>(_section.Marking.ToString())];
                        itemNumber = Math.Min(item.Current + 1, item.Maximum);
                        ItemImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _section.Marking.ToString().ToLower() + itemNumber.ToString() + ".png";
                        break;
                    case MarkingType.Mushroom:
                        ItemImageSource = "avares://OpenTracker/Assets/Images/Items/mushroom1.png";
                        break;
                    case MarkingType.Bombos:
                    case MarkingType.Ether:
                    case MarkingType.Quake:
                        Item medallionDungeons = _game.Items[Enum.Parse<ItemType>(_section.Marking.ToString()) + 1];
                        itemNumber = 1 + (medallionDungeons.Current * 2);
                        ItemImageSource = "avares://OpenTracker/Assets/Images/Items/" +
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
                        ItemImageSource = "avares://OpenTracker/Assets/Images/" +
                            _section.Marking.ToString().ToLower() + ".png";
                        break;
                    case MarkingType.ToH:
                        ItemImageSource = "avares://OpenTracker/Assets/Images/th.png";
                        break;
                    case MarkingType.PoD:
                        ItemImageSource = "avares://OpenTracker/Assets/Images/pd.png";
                        break;
                    case MarkingType.Ganon:
                        ItemImageSource = "avares://OpenTracker/Assets/Images/ganon.png";
                        break;
                }
            }

            switch (_section)
            {
                case ItemSection itemSection:

                    if (_game.Mode.Validate(itemSection.RequiredMode))
                        SectionVisible = true;
                    else
                        SectionVisible = false;

                    switch (accessibility)
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

                    if (!_section.IsAvailable())
                        ImageSource = "avares://OpenTracker/Assets/Images/chest2.png";

                    NumberString = itemSection.Available.ToString();

                    break;

                case BossSection bossSection:

                    SectionVisible = true;

                    string imageBaseString = "avares://OpenTracker/Assets/Images/Items/";

                    if (bossSection.Prize == null)
                        imageBaseString += "unknown";
                    else if (bossSection.Prize.Type == ItemType.Aga2)
                        imageBaseString += "aga";
                    else
                        imageBaseString += bossSection.Prize.Type.ToString().ToLower();

                    ImageSource = imageBaseString + (bossSection.IsAvailable() ? "0" : "1") + ".png";

                    imageBaseString = "avares://OpenTracker/Assets/Images/";

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

                    if (_game.Mode.BossShuffle.Value && (bossSection.Boss == null || bossSection.Boss.Type != BossType.Aga))
                        BossImageVisible = true;
                    else
                        BossImageVisible = false;

                    break;

                case EntranceSection entranceSection:

                    SectionVisible = true;

                    string entranceImageBaseString = "avares://OpenTracker/Assets/Images/door";

                    ImageSource = entranceImageBaseString + (entranceSection.IsAvailable() ? "0" : "1") + ".png";

                    break;
            }
        }

        public void ChangeMarking(MarkingType? marking)
        {
            if (marking != null)
            {
                if (_section.Marking != null)
                {
                    if (Enum.TryParse(_section.Marking.ToString(), out ItemType currentItemType))
                    {
                        _game.Items[currentItemType].PropertyChanged -= OnVisibleItemChanged;

                        switch (currentItemType)
                        {
                            case ItemType.Bombos:
                            case ItemType.Ether:
                            case ItemType.Quake:
                                _game.Items[currentItemType + 1].PropertyChanged -= OnVisibleItemChanged;
                                break;
                        }
                    }
                }

                _undoRedoManager.Execute(new MarkSection(_section, marking));

                if (Enum.TryParse(_section.Marking.ToString(), out ItemType newItemType))
                {
                    _game.Items[newItemType].PropertyChanged += OnVisibleItemChanged;

                    switch (newItemType)
                    {
                        case ItemType.Bombos:
                        case ItemType.Ether:
                        case ItemType.Quake:
                            _game.Items[newItemType + 1].PropertyChanged += OnVisibleItemChanged;
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
