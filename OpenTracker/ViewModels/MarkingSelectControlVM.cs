using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels
{
    public class MarkingSelectControlVM : ViewModelBase
    {
        private readonly Game _game;

        public MarkingType? Marking { get; }

        public string ImageSource
        {
            get
            {
                if (Marking != null)
                {
                    int itemNumber;
                    switch (Marking)
                    {
                        case MarkingType.Bow:
                        case MarkingType.SilverArrows:
                        case MarkingType.Boomerang:
                        case MarkingType.RedBoomerang:
                        case MarkingType.SmallKey:
                        case MarkingType.BigKey:
                            {
                                return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                    $"{ Marking.ToString().ToLowerInvariant() }.png";
                            }
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
                            {
                                return "avares://OpenTracker/Assets/Images/Items/" +
                                    $"{ Marking.ToString().ToLowerInvariant() }1.png";
                            }
                        case MarkingType.Bottle:
                        case MarkingType.Gloves:
                        case MarkingType.Shield:
                        case MarkingType.Mail:
                            {
                                var item = _game.Items[Enum.Parse<ItemType>(Marking.ToString())];
                                itemNumber = Math.Min(item.Current + 1, item.Maximum);

                                return "avares://OpenTracker/Assets/Images/Items/" +
                                    Marking.ToString().ToLowerInvariant() +
                                    $"{ itemNumber.ToString(CultureInfo.InvariantCulture) }.png";
                            }
                        case MarkingType.Sword:
                            {
                                var sword = _game.Items[ItemType.Sword];

                                if (sword.Current == 0)
                                {
                                    itemNumber = 0;
                                }
                                else
                                {
                                    itemNumber = Math.Min(sword.Current + 1, sword.Maximum);
                                }

                                return "avares://OpenTracker/Assets/Images/Items/" +
                                    Marking.ToString().ToLowerInvariant() +
                                    $"{ itemNumber.ToString(CultureInfo.InvariantCulture) }.png";
                            }
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
                            {
                                return "avares://OpenTracker/Assets/Images/" +
                                    $"{ Marking.ToString().ToLowerInvariant() }.png";
                            }
                        case MarkingType.ToH:
                            {
                                return "avares://OpenTracker/Assets/Images/th.png";
                            }
                        case MarkingType.PoD:
                            {
                                return "avares://OpenTracker/Assets/Images/pd.png";
                            }
                        case MarkingType.Ganon:
                            {
                                return "avares://OpenTracker/Assets/Images/ganon.png";
                            }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="marking">
        /// The marking to be represented by this option.
        /// </param>
        public MarkingSelectControlVM(Game game, MarkingType? marking)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            Marking = marking;

            if (Marking != null)
            {
                if (Enum.TryParse(Marking.ToString(), out ItemType itemType))
                {
                    _game.Items[itemType].PropertyChanged += OnItemChanged;

                    switch (itemType)
                    {
                        case ItemType.Bombos:
                        case ItemType.Ether:
                        case ItemType.Quake:
                            {
                                _game.Items[itemType + 1].PropertyChanged += OnItemChanged;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Item class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }
    }
}
