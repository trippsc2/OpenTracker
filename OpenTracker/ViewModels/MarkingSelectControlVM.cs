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
                
                UpdateImage();
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateImage();
        }

        public void UpdateImage()
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
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _marking.ToString().ToLower() + "1.png";
                        break;
                    case MarkingType.Bottle:
                    case MarkingType.Gloves:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                        Item item = _game.Items[Enum.Parse<ItemType>(_marking.ToString())];
                        itemNumber = Math.Min(item.Current + 1, item.Maximum);
                        ImageSource = "avares://OpenTracker/Assets/Images/Items/" +
                            _marking.ToString().ToLower() + itemNumber.ToString() + ".png";
                        break;
                    case MarkingType.Sword:

                        Item sword = _game.Items[ItemType.Sword];

                        if (sword.Current == 0)
                            itemNumber = 0;
                        else
                            itemNumber = Math.Min(sword.Current + 1, sword.Maximum);

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
