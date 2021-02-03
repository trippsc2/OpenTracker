using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.PinnedLocations.SectionIcons;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the class containing creation logic for section control ViewModel classes.
    /// </summary>
    internal static class SectionVMFactory
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
        private static ObservableCollection<SectionIconVMBase> GetSectionIcons(
            ISection section)
        {
            var icons = new ObservableCollection<SectionIconVMBase>();

            if (section is IMarkableSection markableSection)
            {
                icons.Add(new MarkingSectionIconVM(markableSection));
            }

            if (section is IEntranceSection entranceSection)
            {
                icons.Add(new EntranceSectionIconVM(entranceSection));
            }

            if (section is IItemSection itemSection)
            {
                icons.Add(new ItemSectionIconVM(itemSection));
            }

            if (section is IShopSection shopSection)
            {
                icons.Add(new ShopSectionIconVM(shopSection));
            }

            if (section is ITakeAnySection takeAnySection)
            {
                icons.Add(new TakeAnySectionIconVM(takeAnySection));
            }

            if (section is IPrizeSection prizeSection)
            {
                icons.Add(new PrizeSectionIconVM(prizeSection));
            }

            if (section is IBossSection bossSection)
            {
                icons.Add(new BossSectionIconVM(bossSection.BossPlacement));
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
        internal static SectionVM GetSectionVM(ISection section)
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            return new SectionVM(section, GetSectionIcons(section));
        }
    }
}
