using OpenTracker.Models.Sections;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This is the class containing creation logic for section control ViewModel classes.
    /// </summary>
    public class SectionVMFactory : ISectionVMFactory
    {
        private readonly MarkingSectionIconVM.Factory _markingFactory;
        private readonly EntranceSectionIconVM.Factory _entranceFactory;
        private readonly ItemSectionIconVM.Factory _itemFactory;
        private readonly ShopSectionIconVM.Factory _shopFactory;
        private readonly TakeAnySectionIconVM.Factory _takeAnyFactory;
        private readonly PrizeSectionIconVM.Factory _prizeFactory;
        private readonly BossSectionIconVM.Factory _bossFactory;

        private readonly ISectionVM.Factory _factory;

        public SectionVMFactory(
            MarkingSectionIconVM.Factory markingFactory,
            EntranceSectionIconVM.Factory entranceFactory,
            ItemSectionIconVM.Factory itemFactory, ShopSectionIconVM.Factory shopFactory,
            TakeAnySectionIconVM.Factory takeAnyFactory, PrizeSectionIconVM.Factory prizeFactory,
            BossSectionIconVM.Factory bossFactory, ISectionVM.Factory factory)
        {
            _markingFactory = markingFactory;
            _entranceFactory = entranceFactory;
            _itemFactory = itemFactory;
            _shopFactory = shopFactory;
            _takeAnyFactory = takeAnyFactory;
            _prizeFactory = prizeFactory;
            _bossFactory = bossFactory;

            _factory = factory;
        }

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
        private List<ISectionIconVMBase> GetSectionIcons(
            ISection section)
        {
            var icons = new List<ISectionIconVMBase>();

            if (section is IMarkableSection markableSection)
            {
                icons.Add(_markingFactory(markableSection));
            }

            if (section is IEntranceSection entranceSection)
            {
                icons.Add(_entranceFactory(entranceSection));
            }

            if (section is IItemSection itemSection)
            {
                icons.Add(_itemFactory(itemSection));
            }

            if (section is IShopSection shopSection)
            {
                icons.Add(_shopFactory(shopSection));
            }

            if (section is ITakeAnySection takeAnySection)
            {
                icons.Add(_takeAnyFactory(takeAnySection));
            }

            if (section is IPrizeSection prizeSection)
            {
                icons.Add(_prizeFactory(prizeSection));
            }

            if (section is IBossSection bossSection)
            {
                icons.Add(_bossFactory(bossSection.BossPlacement));
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
        public ISectionVM GetSectionVM(ISection section)
        {
            return _factory(section, GetSectionIcons(section));
        }
    }
}
