using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;

namespace OpenTracker.ViewModels
{
    public class MarkingSelectControlVM : ViewModelBase, IMarkingSelectControlVM
    {
        private readonly Game _game;
        private readonly SectionControlVM _section;
        private readonly MarkingType? _marking;

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => this.RaiseAndSetIfChanged(ref _imageSource, value);
        }

        public MarkingSelectControlVM(Game game, SectionControlVM section, MarkingType? marking)
        {
            _game = game;
            _section = section;
            _marking = marking;

            if (_marking != null)
            {
                if (Enum.TryParse(_marking.ToString(), out ItemType itemType))
                {
                    _game.Items[itemType].PropertyChanged += OnItemChanged;

                    switch (itemType)
                    {
                        case ItemType.Bombos:
                        case ItemType.Ether:
                        case ItemType.Quake:
                            _game.Items[itemType + 1].PropertyChanged += OnItemChanged;
                            break;
                    }
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
            if (_marking != null)
            {
                switch (_marking)
                {
                    case MarkingType.Bow:
                    case MarkingType.SilverArrows:
                    case MarkingType.Boomerang:
                    case MarkingType.RedBoomerang:
                    case MarkingType.SmallKey:
                    case MarkingType.BigKey:
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/visible-" +
                            _marking.ToString().ToLower() + ".png";
                        break;
                    case MarkingType.Hookshot:
                    case MarkingType.Bomb:
                    case MarkingType.FireRod:
                    case MarkingType.IceRod:
                    case MarkingType.Shovel:
                    case MarkingType.Powder:
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
                        Item item = _game.Items[Enum.Parse<ItemType>(_marking.ToString())];
                        itemNumber = Math.Min(item.Current + 1, item.Maximum);
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _marking.ToString().ToLower() + itemNumber.ToString() + ".png";
                        break;
                    case MarkingType.Mushroom:
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/mushroom1.png";
                        break;
                    case MarkingType.Bombos:
                    case MarkingType.Ether:
                    case MarkingType.Quake:
                        Item medallionDungeons = _game.Items[Enum.Parse<ItemType>(_marking.ToString()) + 1];
                        itemNumber = 1 + (medallionDungeons.Current * 2);
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _marking.ToString().ToLower() + itemNumber.ToString() + ".png";
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
                        ImageSource = "avares://OpenTracker/Assets/Images/" +
                            _marking.ToString().ToLower() + ".png";
                        break;
                    case MarkingType.ToH:
                        ImageSource = "avares://OpenTracker/Assets/Images/th.png";
                        break;
                    case MarkingType.PoD:
                        ImageSource = "avares://OpenTracker/Assets/Images/pd.png";
                        break;
                    case MarkingType.Ganon:
                        ImageSource = "avares://OpenTracker/Assets/Images/ganon.png";
                        break;
                }
            }
        }

        public void SelectItem()
        {
            if (_marking != null)
                _section.ChangeMarking(_marking);
        }
    }
}
