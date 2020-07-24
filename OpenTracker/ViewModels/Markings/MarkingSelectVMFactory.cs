using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Markings
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
        /// marking.
        /// </summary>
        /// <param name="marking">
        /// The marking.
        /// </param>
        /// <returns>
        /// A new marking select popup control ViewModel instance.
        /// </returns>
        private static MarkingSelectVM GetNonEntranceMarkingSelectPopupVM(
            IMarking marking)
        {
            if (NonEntranceMarkingSelect.Count == 0)
            {
                PopulateNonEntranceMarkingSelect();
            }

            return new MarkingSelectVM(marking, NonEntranceMarkingSelect, 238.0, 200.0);
        }

        /// <summary>
        /// Returns a new marking select popup control ViewModel instance for the specified
        /// marking.
        /// </summary>
        /// <param name="marking">
        /// The marking.
        /// </param>
        /// <returns>
        /// A new marking select popup control ViewModel instance.
        /// </returns>
        private static MarkingSelectVM GetEntranceMarkingSelectPopupVM(
            IMarking marking)
        {
            if (EntranceMarkingSelect.Count == 0)
            {
                PopulateEntranceMarkingSelect();
            }

            return new MarkingSelectVM(marking, EntranceMarkingSelect, 272.0, 280.0);
        }

        /// <summary>
        /// Returns a new marking select popup control ViewModel instance for the specified
        /// section.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// A new marking select popup control ViewModel instance.
        /// </returns>
        internal static MarkingSelectVM GetMarkingSelectVM(IMarkableSection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            if (section is IEntranceSection)
            {
                return GetEntranceMarkingSelectPopupVM(section.Marking);
            }

            return GetNonEntranceMarkingSelectPopupVM(section.Marking);
        }

        /// <summary>
        /// Returns a new marking select popup control ViewModel instance.
        /// </summary>
        /// <param name="marking">
        /// The marking.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <returns>
        /// A new marking select popup control ViewModel instance.
        /// </returns>
        internal static NoteMarkingSelectVM GetNoteMarkingSelectVM(
            IMarking marking, ILocation location)
        {
            if (marking == null)
            {
                throw new ArgumentNullException(nameof(marking));
            }

            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new NoteMarkingSelectVM(marking, NonEntranceMarkingSelect, location);
        }
    }
}
