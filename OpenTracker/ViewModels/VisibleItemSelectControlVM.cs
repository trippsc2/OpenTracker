using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class VisibleItemSelectControlVM : ViewModelBase, IVisibleItemSelectControlVM
    {
        private readonly Game _game;
        private readonly SectionControlVM _section;
        private readonly Item _item;

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public VisibleItemSelectControlVM(Game game, SectionControlVM section, Item item)
        {
            _game = game;
            _section = section;
            _item = item;

            if (_item != null)
            {
                _item.PropertyChanged += OnItemChanged;

                switch (_item.Type)
                {
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                        _game.Items[_item.Type + 1].PropertyChanged += OnItemChanged;
                        break;
                }

                Update();
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            int itemNumber = 0;
            if (_item != null)
            {
                switch (_item.Type)
                {
                    case ItemType.Bow:
                    case ItemType.SilverArrows:
                    case ItemType.Boomerang:
                    case ItemType.RedBoomerang:
                    case ItemType.SmallKey:
                    case ItemType.BigKey:
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/visible-" +
                            _item.Type.ToString().ToLower() + ".png";
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
                        itemNumber = Math.Min(_item.Current + 1, _item.Maximum);
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _item.Type.ToString().ToLower() + itemNumber.ToString() + ".png";
                        break;
                    case ItemType.Mushroom:
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/mushroom1.png";
                        break;
                    case ItemType.Bombos:
                    case ItemType.Ether:
                    case ItemType.Quake:
                        itemNumber = 1 + (_game.Items[_item.Type + 1].Current * 2);
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _item.Type.ToString().ToLower() + itemNumber.ToString() + ".png";
                        break;
                }
            }
        }

        public void SelectItem()
        {
            if (_item != null)
                _section.ChangeVisibleItem(_item);
        }
    }
}
