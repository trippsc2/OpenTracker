using OpenTracker.Models.Items;
using OpenTracker.Models.Markings;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This is the ViewModel for the marking select button control.
    /// </summary>
    public class MarkingSelectButtonVM : ViewModelBase
    {
        public MarkType? Marking { get; }

        public string ImageSource
        {
            get
            {
                if (Marking != null)
                {
                    int itemNumber;

                    switch (Marking)
                    {
                        case MarkType.Bow:
                        case MarkType.SilverArrows:
                        case MarkType.Boomerang:
                        case MarkType.RedBoomerang:
                        case MarkType.SmallKey:
                        case MarkType.BigKey:
                            {
                                return "avares://OpenTracker/Assets/Images/Items/visible-" +
                                    $"{Marking.ToString().ToLowerInvariant()}.png";
                            }
                        case MarkType.Hookshot:
                        case MarkType.Bomb:
                        case MarkType.Mushroom:
                        case MarkType.Powder:
                        case MarkType.FireRod:
                        case MarkType.IceRod:
                        case MarkType.Bombos:
                        case MarkType.Ether:
                        case MarkType.Quake:
                        case MarkType.Shovel:
                        case MarkType.Lamp:
                        case MarkType.Hammer:
                        case MarkType.Flute:
                        case MarkType.Net:
                        case MarkType.Book:
                        case MarkType.MoonPearl:
                        case MarkType.CaneOfSomaria:
                        case MarkType.CaneOfByrna:
                        case MarkType.Cape:
                        case MarkType.Mirror:
                        case MarkType.Boots:
                        case MarkType.Flippers:
                        case MarkType.HalfMagic:
                        case MarkType.Aga:
                            {
                                return "avares://OpenTracker/Assets/Images/Items/" +
                                    $"{ Marking.ToString().ToLowerInvariant() }1.png";
                            }
                        case MarkType.Bottle:
                        case MarkType.Gloves:
                        case MarkType.Shield:
                        case MarkType.Mail:
                            {
                                var item = ItemDictionary.Instance[Enum.Parse<ItemType>(Marking.ToString())];
                                itemNumber = Math.Min(item.Current + 1, item.Maximum);

                                return "avares://OpenTracker/Assets/Images/Items/" +
                                    Marking.ToString().ToLowerInvariant() +
                                    $"{ itemNumber.ToString(CultureInfo.InvariantCulture) }.png";
                            }
                        case MarkType.Sword:
                            {
                                var sword = ItemDictionary.Instance[ItemType.Sword];

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
                        case MarkType.HCFront:
                        case MarkType.HCLeft:
                        case MarkType.HCRight:
                        case MarkType.EP:
                        case MarkType.SP:
                        case MarkType.SW:
                        case MarkType.DPFront:
                        case MarkType.DPLeft:
                        case MarkType.DPRight:
                        case MarkType.DPBack:
                        case MarkType.TT:
                        case MarkType.IP:
                        case MarkType.MM:
                        case MarkType.TRFront:
                        case MarkType.TRLeft:
                        case MarkType.TRRight:
                        case MarkType.TRBack:
                        case MarkType.GT:
                            {
                                return "avares://OpenTracker/Assets/Images/" +
                                    $"{ Marking.ToString().ToLowerInvariant() }.png";
                            }
                        case MarkType.ToH:
                            {
                                return "avares://OpenTracker/Assets/Images/th.png";
                            }
                        case MarkType.PoD:
                            {
                                return "avares://OpenTracker/Assets/Images/pd.png";
                            }
                        case MarkType.Ganon:
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
        /// <param name="marking">
        /// The marking to be represented by this button.
        /// </param>
        public MarkingSelectButtonVM(MarkType? marking)
        {
            Marking = marking;

            if (Marking != null)
            {
                if (Enum.TryParse(Marking.ToString(), out ItemType itemType))
                {
                    ItemDictionary.Instance[itemType].PropertyChanged += OnItemChanged;

                    switch (itemType)
                    {
                        case ItemType.Bombos:
                        case ItemType.Ether:
                        case ItemType.Quake:
                            {
                                ItemDictionary.Instance[itemType + 1].PropertyChanged += OnItemChanged;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
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
