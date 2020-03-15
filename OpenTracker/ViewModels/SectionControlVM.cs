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

        public ReactiveCommand<Unit, Unit> ClearVisibleItemCommand { get; }

        public ObservableCollection<VisibleItemSelectControlVM> ItemSelect { get; }

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

            if (_section is ItemSection)
            {
                ItemSection itemSection = (ItemSection)_section;

                if (itemSection.HasVisibleItem)
                {
                    VisibleItem = true;
                }
            }

            _section.ItemRequirementChanged += OnItemRequirementChanged;
            _section.PropertyChanged += OnSectionChanged;

            Update();
        }

        private void ClearVisibleItem()
        {
            ((ItemSection)_section).VisibleItem = null;
            VisibleItemPopupOpen = false;
        }

        private void OnItemRequirementChanged(object sender, EventArgs e)
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
            Accessibility accessibility = _section.GetAccessibility(_game.Mode, _game.Items);

            if (accessibility == Accessibility.Normal)
                FontColor = new SolidColorBrush(new Color(255, 245, 245, 245));
            else
                FontColor = _appSettings.AccessibilityColors[accessibility];


            switch (_section)
            {
                case ItemSection itemSection:

                    switch (accessibility)
                    {
                        case Accessibility.None:
                        case Accessibility.Inspect:
                            ImageSource = "avares://OpenTracker/Assets/Images/chest0.png";
                            break;
                        case Accessibility.SequenceBreak:
                        case Accessibility.Normal:
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
            if (_section.GetAccessibility(_game.Mode, _game.Items) >= Accessibility.SequenceBreak)
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
                }
            }
        }
    }
}
