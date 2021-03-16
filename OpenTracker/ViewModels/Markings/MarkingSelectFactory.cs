using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Markings
{
    /// <summary>
    /// This is the class for creating marking select control ViewModel classes.
    /// </summary>
    public class MarkingSelectFactory : IMarkingSelectFactory
    {
        private readonly MarkingSelectButtonVM.Factory _buttonFactory;
        private readonly IMarkingSelectVM.Factory _selectFactory;
        private readonly INoteMarkingSelectVM.Factory _noteSelectFactory;

        private List<IMarkingSelectItemVMBase> NonEntranceMarkingSelect { get; } =
            new List<IMarkingSelectItemVMBase>();
        private List<IMarkingSelectItemVMBase> EntranceMarkingSelect { get; } =
            new List<IMarkingSelectItemVMBase>();

        public MarkingSelectFactory(
            MarkingSelectButtonVM.Factory buttonFactory, IMarkingSelectVM.Factory selectFactory,
            INoteMarkingSelectVM.Factory noteSelectFactory)
        {
            _buttonFactory = buttonFactory;
            _selectFactory = selectFactory;
            _noteSelectFactory = noteSelectFactory;

            PopulateNonEntranceMarkingSelect();
            PopulateEntranceMarkingSelect();
        }

        /// <summary>
        /// Populates the observable collection of marking select button control ViewModel
        /// instances for non-entrance markings.
        /// </summary>
        private void PopulateNonEntranceMarkingSelect()
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
                            NonEntranceMarkingSelect.Add(_buttonFactory((MarkType)i));
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Populates the observable collection of marking select button control ViewModel
        /// instances for entrance markings.
        /// </summary>
        private void PopulateEntranceMarkingSelect()
        {
            for (int i = 1; i < Enum.GetValues(typeof(MarkType)).Length; i++)
            {
                EntranceMarkingSelect.Add(_buttonFactory((MarkType)i));
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
        private IMarkingSelectVM GetNonEntranceMarkingSelectPopupVM(
            IMarking marking)
        {
            return _selectFactory(marking, NonEntranceMarkingSelect, 238.0, 200.0);
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
        private IMarkingSelectVM GetEntranceMarkingSelectPopupVM(
            IMarking marking)
        {
            return _selectFactory(marking, EntranceMarkingSelect, 374.0, 320.0);
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
        public IMarkingSelectVM GetMarkingSelectVM(IMarkableSection section)
        {
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
        public INoteMarkingSelectVM GetNoteMarkingSelectVM(
            IMarking marking, ILocation location)
        {
            return _noteSelectFactory(marking, NonEntranceMarkingSelect, location);
        }
    }
}
