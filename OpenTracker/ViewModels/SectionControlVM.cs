using Avalonia.Media;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using ReactiveUI;
using System;
using System.ComponentModel;

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

        public SectionControlVM(AppSettingsVM appSettings, Game game, ISection section)
        {
            _appSettings = appSettings;
            _game = game;
            _section = section;

            if (_section is ItemSection)
                VisibleItem = ((ItemSection)_section).HasVisibleItem;

            _section.ItemRequirementChanged += OnItemRequirementChanged;
            _section.PropertyChanged += OnSectionChanged;

            

            Update();
        }

        private void OnItemRequirementChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
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
                            switch (itemSection.VisibleItem.Type)
                            {
                                case ItemType.Bow:
                                    break;
                                case ItemType.SilverArrows:
                                    break;
                                case ItemType.Boomerang:
                                    break;
                                case ItemType.RedBoomerang:
                                    break;
                                case ItemType.Hookshot:
                                    break;
                                case ItemType.Bomb:
                                    break;
                                case ItemType.BigBomb:
                                    break;
                                case ItemType.Powder:
                                    break;
                                case ItemType.MagicBat:
                                    break;
                                case ItemType.Mushroom:
                                    break;
                                case ItemType.TowerCrystals:
                                    break;
                                case ItemType.FireRod:
                                    break;
                                case ItemType.IceRod:
                                    break;
                                case ItemType.Bombos:
                                    break;
                                case ItemType.BombosDungeons:
                                    break;
                                case ItemType.Ether:
                                    break;
                                case ItemType.EtherDungeons:
                                    break;
                                case ItemType.Quake:
                                    break;
                                case ItemType.QuakeDungeons:
                                    break;
                                case ItemType.Shovel:
                                    break;
                                case ItemType.GanonCrystals:
                                    break;
                                case ItemType.Lamp:
                                    break;
                                case ItemType.Hammer:
                                    break;
                                case ItemType.Flute:
                                    break;
                                case ItemType.FluteActivated:
                                    break;
                                case ItemType.Net:
                                    break;
                                case ItemType.Book:
                                    break;
                                case ItemType.MoonPearl:
                                    break;
                                case ItemType.Bottle:
                                    break;
                                case ItemType.CaneOfSomaria:
                                    break;
                                case ItemType.CaneOfByrna:
                                    break;
                                case ItemType.Cape:
                                    break;
                                case ItemType.Mirror:
                                    break;
                                case ItemType.GoMode:
                                    break;
                                case ItemType.Aga:
                                    break;
                                case ItemType.Gloves:
                                    break;
                                case ItemType.Boots:
                                    break;
                                case ItemType.Flippers:
                                    break;
                                case ItemType.HalfMagic:
                                    break;
                                case ItemType.Sword:
                                    break;
                                case ItemType.Shield:
                                    break;
                                case ItemType.Mail:
                                    break;
                                case ItemType.GreenPendant:
                                    break;
                                case ItemType.Pendant:
                                    break;
                                case ItemType.Crystal:
                                    break;
                                case ItemType.RedCrystal:
                                    break;
                                case ItemType.HCSmallKey:
                                    break;
                                case ItemType.DPSmallKey:
                                    break;
                                case ItemType.ToHSmallKey:
                                    break;
                                case ItemType.ATSmallKey:
                                    break;
                                case ItemType.PoDSmallKey:
                                    break;
                                case ItemType.SPSmallKey:
                                    break;
                                case ItemType.SWSmallKey:
                                    break;
                                case ItemType.TTSmallKey:
                                    break;
                                case ItemType.IPSmallKey:
                                    break;
                                case ItemType.MMSmallKey:
                                    break;
                                case ItemType.TRSmallKey:
                                    break;
                                case ItemType.GTSmallKey:
                                    break;
                                case ItemType.EPBigKey:
                                    break;
                                case ItemType.DPBigKey:
                                    break;
                                case ItemType.ToHBigKey:
                                    break;
                                case ItemType.PoDBigKey:
                                    break;
                                case ItemType.SPBigKey:
                                    break;
                                case ItemType.SWBigKey:
                                    break;
                                case ItemType.TTBigKey:
                                    break;
                                case ItemType.IPBigKey:
                                    break;
                                case ItemType.MMBigKey:
                                    break;
                                case ItemType.TRBigKey:
                                    break;
                                case ItemType.GTBigKey:
                                    break;
                            }
                        }
                    }

                    NumberString = itemSection.Available.ToString();

                    break;
            }
        }

        public void ChangeAvailable(bool rightClick = false)
        {
            if (_section.GetAccessibility(_game.Mode, _game.Items) >= Accessibility.SequenceBreak)
            {
                switch (_section)
                {
                    case ItemSection itemSection:
                        itemSection.Available = Math.Max(0, Math.Min(itemSection.Total, itemSection.Available + (rightClick ? 1 : -1)));
                        break;
                }
            }
        }
    }
}
