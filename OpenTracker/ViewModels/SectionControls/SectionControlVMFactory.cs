using OpenTracker.Models.Sections;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.SectionControls
{
    /// <summary>
    /// This is the class containing creation logic for section control ViewModel classes.
    /// </summary>
    internal static class SectionControlVMFactory
    {
        /// <summary>
        /// Returns the observable collection of section icon control ViewModel instances for the
        /// specified section.
        /// </summary>
        /// <param name="section">
        /// The represented section.
        /// </param>
        /// <returns>
        /// The observable collection of section icon control ViewModel instances.
        /// </returns>
        private static ObservableCollection<SectionIconControlVMBase> GetSectionIconControls(
            ISection section)
        {
            var icons = new ObservableCollection<SectionIconControlVMBase>();

            if (section is IMarkableSection markableSection)
            {
                icons.Add(new MarkingSectionIconControlVM(markableSection));
            }

            if (section is IEntranceSection entranceSection)
            {
                icons.Add(new EntranceSectionIconControlVM(entranceSection));
            }

            if (section is IItemSection itemSection)
            {
                icons.Add(new ItemSectionIconControlVM(itemSection));
            }

            if (section is ITakeAnySection takeAnySection)
            {
                icons.Add(new TakeAnySectionIconControlVM(takeAnySection));
            }

            if (section is IPrizeSection prizeSection)
            {
                icons.Add(new PrizeSectionIconControlVM(prizeSection));
            }

            if (section is IBossSection bossSection)
            {
                icons.Add(new BossSectionIconControlVM(bossSection));
            }

            return icons;
        }

        /// <summary>
        /// Returns a new section control ViewModel instance representing the specified section.
        /// </summary>
        /// <param name="section">
        /// The section to be represented.
        /// </param>
        /// <returns>
        /// A new section control ViewModel instance.
        /// </returns>
        internal static SectionControlVM GetSectionControlVM(ISection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new SectionControlVM(section, GetSectionIconControls(section));
        }
    }
}
