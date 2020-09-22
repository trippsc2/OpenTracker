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
        internal static ObservableCollection<MarkingSelectItemVMBase> NonEntranceMarkingSelect { get; } =
            new ObservableCollection<MarkingSelectItemVMBase>();
        internal static ObservableCollection<MarkingSelectItemVMBase> EntranceMarkingSelect { get; } =
            new ObservableCollection<MarkingSelectItemVMBase>();

        /// <summary>
        /// Populates the observable collection of marking select button control ViewModel
        /// instances for non-entrance markings.
        /// </summary>
        private static void PopulateNonEntranceMarkingSelect()
        {
            for (int i = 0; i < Enum.GetValues(typeof(MarkType)).Length; i++)
            {
                switch ((MarkType)i)
                {
                    case MarkType.Sword:
                    case MarkType.Shield:
                    case MarkType.Mail:
                    case MarkType.Boots:
                    case MarkType.Gloves:
                    case MarkType.Flippers:
                    case MarkType.MoonPearl:
                    case MarkType.Bow:
                    case MarkType.SilverArrows:
                    case MarkType.Boomerang:
                    case MarkType.RedBoomerang:
                    case MarkType.Hookshot:
                    case MarkType.Bomb:
                    case MarkType.Mushroom:
                    case MarkType.FireRod:
                    case MarkType.IceRod:
                    case MarkType.Bombos:
                    case MarkType.Ether:
                    case MarkType.Quake:
                    case MarkType.TriforcePiece:
                    case MarkType.Powder:
                    case MarkType.Lamp:
                    case MarkType.Hammer:
                    case MarkType.Flute:
                    case MarkType.Net:
                    case MarkType.Book:
                    case MarkType.Shovel:
                    case MarkType.SmallKey:
                    case MarkType.Bottle:
                    case MarkType.CaneOfSomaria:
                    case MarkType.CaneOfByrna:
                    case MarkType.Cape:
                    case MarkType.Mirror:
                    case MarkType.HalfMagic:
                    case MarkType.BigKey:
                        {
                            NonEntranceMarkingSelect.Add(new MarkingSelectButtonVM((MarkType)i));
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
            for (int i = 1; i < Enum.GetValues(typeof(MarkType)).Length; i++)
            {
                EntranceMarkingSelect.Add(new MarkingSelectButtonVM((MarkType)i));
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

            return new MarkingSelectVM(marking, EntranceMarkingSelect, 374.0, 320.0);
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
