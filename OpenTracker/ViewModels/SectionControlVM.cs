using Avalonia.Media;
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
        private readonly AppSettingsVM _appSettings;
        private readonly Game _game;
        private readonly ISection _section;

        public string Name { get => _section.Name; }
        public bool VisibleItem { get; }
        public bool NumberBoxVisible { get; }
        public ObservableCollection<VisibleItemSelectControlVM> ItemSelect { get; }

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

        private bool _visibleItemPopupOpen;
        public bool VisibleItemPopupOpen
        {
            get => _visibleItemPopupOpen;
            set => this.RaiseAndSetIfChanged(ref _visibleItemPopupOpen, value);
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

        public SectionControlVM(AppSettingsVM appSettings, Game game, ISection section)
        {
            ClearVisibleItemCommand = ReactiveCommand.Create(ClearVisibleItem);
            _appSettings = appSettings;
            _game = game;
            _section = section;

            ItemSelect = new ObservableCollection<VisibleItemSelectControlVM>();

            for (int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
            {
                switch ((ItemType)i)
                {
                    case ItemType.Bow:
                    case ItemType.SilverArrows:
                    case ItemType.Boomerang:
                    case ItemType.RedBoomerang:
                    case ItemType.Hookshot:
                    case ItemType.Powder:
                    case ItemType.Mushroom:
                    case ItemType.FireRod:
                    case ItemType.IceRod:
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                    case ItemType.Lamp:
                    case ItemType.Hammer:
                    case ItemType.Flute:
                    case ItemType.Net:
                    case ItemType.Book:
                    case ItemType.Bottle:
                    case ItemType.CaneOfSomaria:
                    case ItemType.CaneOfByrna:
                    case ItemType.Cape:
                    case ItemType.Gloves:
                    case ItemType.Boots:
                    case ItemType.Flippers:
                    case ItemType.HalfMagic:
                    case ItemType.Sword:
                    case ItemType.Shield:
                    case ItemType.Mail:
                        ItemSelect.Add(new VisibleItemSelectControlVM(_game, this, _game.Items[(ItemType)i]));
                        break;
                    case ItemType.Shovel:
                    case ItemType.MoonPearl:
                        ItemSelect.Add(new VisibleItemSelectControlVM(_game, this, _game.Items[(ItemType)i]));
                        ItemSelect.Add(new VisibleItemSelectControlVM(_game, this, null));
                        break;
                    case ItemType.Mirror:
                        ItemSelect.Add(new VisibleItemSelectControlVM(_game, this, _game.Items[(ItemType)i]));
                        ItemSelect.Add(new VisibleItemSelectControlVM(_game, this, _game.Items[ItemType.SmallKey]));
                        ItemSelect.Add(new VisibleItemSelectControlVM(_game, this, _game.Items[ItemType.BigKey]));
                        break;
                    default:
                        break;
                }
            }

            if (_section is ItemSection itemSection)
            {
                if (itemSection.HasVisibleItem)
                    VisibleItem = true;

                NumberBoxVisible = true;
            }

            _game.Mode.PropertyChanged += OnModeChanged;
            _section.PropertyChanged += OnSectionChanged;

            Update();
        }

        private void ClearVisibleItem()
        {
            ((ItemSection)_section).VisibleItem = null;
            VisibleItemPopupOpen = false;
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
                        case AccessibilityLevel.SequenceBreak:
                        case AccessibilityLevel.Normal:
                            ImageSource = "avares://OpenTracker/Assets/Images/chest1.png";
                            break;
                    }

                    if (!_section.IsAvailable())
                        ImageSource = "avares://OpenTracker/Assets/Images/chest2.png";

                    if (itemSection.HasVisibleItem)
                    {
                        if (itemSection.VisibleItem == null)
                            ItemImageSource = "avares://OpenTracker/Assets/Images/Items/visible-empty.png";
                        else
                        {
                            int itemNumber = 0;
                            switch (itemSection.VisibleItem.Type)
                            {
                                case ItemType.Bow:
                                case ItemType.SilverArrows:
                                case ItemType.Boomerang:
                                case ItemType.RedBoomerang:
                                case ItemType.SmallKey:
                                case ItemType.BigKey:
                                    ItemImageSource = "avares://OpenTracker/Assets/Images/Items/visible-" +
                                        itemSection.VisibleItem.Type.ToString().ToLower() + ".png";
                                    break;
                                case ItemType.Hookshot:
                                case ItemType.Powder:
                                case ItemType.FireRod:
                                case ItemType.IceRod:
                                case ItemType.Shovel:
                                case ItemType.Lamp:
                                case ItemType.Hammer:
                                case ItemType.Flute:
                                case ItemType.Net:
                                case ItemType.Book:
                                case ItemType.MoonPearl:
                                case ItemType.Bottle:
                                case ItemType.CaneOfSomaria:
                                case ItemType.CaneOfByrna:
                                case ItemType.Cape:
                                case ItemType.Mirror:
                                case ItemType.Gloves:
                                case ItemType.Boots:
                                case ItemType.Flippers:
                                case ItemType.HalfMagic:
                                case ItemType.Sword:
                                case ItemType.Shield:
                                case ItemType.Mail:
                                    itemNumber = Math.Min(itemSection.VisibleItem.Current + 1, itemSection.VisibleItem.Maximum);
                                    ItemImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                                        itemSection.VisibleItem.Type.ToString().ToLower() + itemNumber.ToString() + ".png";
                                    break;
                                case ItemType.Mushroom:
                                    ItemImageSource = "avares://OpenTracker/Assets/Images/Items/mushroom1.png";
                                    break;
                                case ItemType.Bombos:
                                case ItemType.Ether:
                                case ItemType.Quake:
                                    itemNumber = 1 + (_game.Items[itemSection.VisibleItem.Type + 1].Current * 2);
                                    ItemImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                                        itemSection.VisibleItem.Type.ToString().ToLower() + itemNumber.ToString() + ".png";
                                    break;
                            }
                        }
                    }

                    NumberString = itemSection.Available.ToString();

                    break;

                case BossSection bossSection:

                    SectionVisible = true;

                    string imageBaseString = "avares://OpenTracker/Assets/Images/Items/";

                    if (bossSection.Prize == null)
                        imageBaseString += "unknown";
                    else
                        imageBaseString += bossSection.Prize.Type.ToString().ToLower();

                    ImageSource = imageBaseString + (bossSection.Available ? "0" : "1") + ".png";

                    imageBaseString = "avares://OpenTracker/Assets/Images/";

                    if (bossSection.Boss == null)
                        imageBaseString += "Items/unknown1";
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

                    ImageSource = entranceImageBaseString + (entranceSection.Available ? "0" : "1") + ".png";

                    break;
            }
        }

        public void ChangeVisibleItem(Item item)
        {
            if (item != null)
            {
                ItemSection itemSection = (ItemSection)_section;

                if (itemSection.VisibleItem != null)
                {
                    itemSection.VisibleItem.PropertyChanged -= OnVisibleItemChanged;

                    switch (itemSection.VisibleItem.Type)
                    {
                        case ItemType.Bombos:
                        case ItemType.Ether:
                        case ItemType.Quake:
                            _game.Items[itemSection.VisibleItem.Type + 1].PropertyChanged -= OnVisibleItemChanged;
                            break;
                    }

                }

                itemSection.VisibleItem = item;

                item.PropertyChanged += OnVisibleItemChanged;

                switch (item.Type)
                {
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                        _game.Items[item.Type + 1].PropertyChanged += OnVisibleItemChanged;
                        break;
                }

                VisibleItemPopupOpen = false;
            }
        }

        public void OpenVisibleItemSelect()
        {
            VisibleItemPopupOpen = true;
        }

        public void ChangeAvailable(bool rightClick = false)
        {
            if (_section.Accessibility >= AccessibilityLevel.SequenceBreak || rightClick ||
                _section is EntranceSection)
            {
                switch (_section)
                {
                    case ItemSection itemSection:

                        itemSection.Available = Math.Max(0, Math.Min(itemSection.Total, itemSection.Available + (rightClick ? 1 : -1)));

                        if (itemSection.VisibleItem != null && itemSection.Available == 0)
                        {
                            itemSection.VisibleItem.Current = Math.Min(itemSection.VisibleItem.Current + 1, itemSection.VisibleItem.Maximum);
                            itemSection.VisibleItem = null;
                        }

                        break;

                    case BossSection bossSection:

                        bossSection.Available = rightClick;

                        break;

                    case EntranceSection entranceSection:

                        entranceSection.Available = rightClick;

                        break;
                }
            }
        }
    }
}
