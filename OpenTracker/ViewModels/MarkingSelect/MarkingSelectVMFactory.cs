using OpenTracker.Models.Sections;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.MarkingSelect
{
    /// <summary>
    /// This is the class for creating marking select control ViewModel classes.
    /// </summary>
    internal static class MarkingSelectVMFactory
    {
        internal static ObservableCollection<MarkingSelectButtonVM> NonEntranceMarkingSelect { get; } =
            new ObservableCollection<MarkingSelectButtonVM>();
        internal static ObservableCollection<MarkingSelectButtonVM> EntranceMarkingSelect { get; } =
            new ObservableCollection<MarkingSelectButtonVM>();

        /// <summary>
        /// Populates the observable collection of marking select button control ViewModel
        /// instances for non-entrance markings.
        /// </summary>
        private static void PopulateNonEntranceMarkingSelect()
        {
            for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
            {
                switch ((MarkingType)i)
                {
                    case MarkingType.Sword:
                    case MarkingType.Shield:
                    case MarkingType.Mail:
                    case MarkingType.Boots:
                    case MarkingType.Gloves:
                    case MarkingType.Flippers:
                    case MarkingType.MoonPearl:
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
                    case MarkingType.Powder:
                    case MarkingType.Lamp:
                    case MarkingType.Hammer:
                    case MarkingType.Flute:
                    case MarkingType.Net:
                    case MarkingType.Book:
                    case MarkingType.Shovel:
                    case MarkingType.SmallKey:
                    case MarkingType.Bottle:
                    case MarkingType.CaneOfSomaria:
                    case MarkingType.CaneOfByrna:
                    case MarkingType.Cape:
                    case MarkingType.Mirror:
                    case MarkingType.HalfMagic:
                    case MarkingType.BigKey:
                        {
                            NonEntranceMarkingSelect.Add(new MarkingSelectButtonVM((MarkingType)i));
                        }
                        break;
                    case MarkingType.Quake:
                        {
                            NonEntranceMarkingSelect.Add(new MarkingSelectButtonVM((MarkingType)i));
                            NonEntranceMarkingSelect.Add(new MarkingSelectButtonVM(null));
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Populates the observable collection of marking select button control ViewModel
        /// instances for entrance markings.
        /// </summary>
        private static void PopulateEntranceMarkingSelect()
        {
            for (int i = 0; i < Enum.GetValues(typeof(MarkingType)).Length; i++)
            {
                EntranceMarkingSelect.Add(new MarkingSelectButtonVM((MarkingType)i));
            }
        }

        /// <summary>
        /// Returns a new marking select popup control ViewModel instance for the specified
        /// non-entrance section and parent class.
        /// </summary>
        /// <param name="section">
        /// The non-entrance section.
        /// </param>
        /// <returns>
        /// A new marking select popup control ViewModel instance.
        /// </returns>
        private static MarkingSelectPopupVM GetNonEntranceMarkingSelectPopupVM(
            IMarkableSection section)
        {
            if (NonEntranceMarkingSelect.Count == 0)
            {
                PopulateNonEntranceMarkingSelect();
            }

            return new MarkingSelectPopupVM(section, NonEntranceMarkingSelect, 238.0, 200.0);
        }

        /// <summary>
        /// Returns a new marking select popup control ViewModel instance for the specified
        /// entrance section and parent class.
        /// </summary>
        /// <param name="section">
        /// The entrance section.
        /// </param>
        /// <returns>
        /// A new marking select popup control ViewModel instance.
        /// </returns>
        private static MarkingSelectPopupVM GetEntranceMarkingSelectPopupVM(
            IMarkableSection section)
        {
            if (EntranceMarkingSelect.Count == 0)
            {
                PopulateEntranceMarkingSelect();
            }

            return new MarkingSelectPopupVM(section, EntranceMarkingSelect, 272.0, 280.0);
        }

        /// <summary>
        /// Returns a new marking select popup control ViewModel instance for the specified
        /// section and parent class.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// A new marking select popup control ViewModel instance.
        /// </returns>
        internal static MarkingSelectPopupVM GetMarkingSelectPopupVM(IMarkableSection section)
        {
            if (section is IEntranceSection)
            {
                return GetEntranceMarkingSelectPopupVM(section);
            }

            return GetNonEntranceMarkingSelectPopupVM(section);
        }
    }
}
