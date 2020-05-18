using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class MarkingSelectControlVM : ViewModelBase, ISelectItem
    {
        private readonly Game _game;
        private readonly IChangeMarking _section;
        private readonly MarkingType? _marking;

        public string ImageSource
        {
            get
            {
                if (_marking != null)
                {
                    int itemNumber;
                    switch (_marking)
                    {
                        case MarkingType.Bow:
                        case MarkingType.SilverArrows:
                        case MarkingType.Boomerang:
                        case MarkingType.RedBoomerang:
                        case MarkingType.SmallKey:
                        case MarkingType.BigKey:
                            return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                _marking.ToString().ToLowerInvariant() + ".png";
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
                                _marking.ToString().ToLowerInvariant() + "1.png";
                        case MarkingType.Bottle:
                        case MarkingType.Gloves:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                            Item item = _game.Items[Enum.Parse<ItemType>(_marking.ToString())];
                            itemNumber = Math.Min(item.Current + 1, item.Maximum);
                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _marking.ToString().ToLowerInvariant() + 
                                itemNumber.ToString(CultureInfo.InvariantCulture) + ".png";
                        case MarkingType.Sword:

                            Item sword = _game.Items[ItemType.Sword];

                            if (sword.Current == 0)
                                itemNumber = 0;
                            else
                                itemNumber = Math.Min(sword.Current + 1, sword.Maximum);

                            return "avares://OpenTracker/Assets/Images/Items/" +
                                _marking.ToString().ToLowerInvariant() + itemNumber.ToString(CultureInfo.InvariantCulture) + ".png";

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
                                _marking.ToString().ToLowerInvariant() + ".png";
                        case MarkingType.ToH:
                            return "avares://OpenTracker/Assets/Images/th.png";
                        case MarkingType.PoD:
                            return "avares://OpenTracker/Assets/Images/pd.png";
                        case MarkingType.Ganon:
                            return "avares://OpenTracker/Assets/Images/ganon.png";
                    }
                }

                return null;
            }
        }

        public MarkingSelectControlVM(Game game, IChangeMarking section, MarkingType? marking)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
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
            }
        }

        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        public void SelectItem()
        {
            if (_marking != null)
                _section.ChangeMarking(_marking);
        }
    }
}
