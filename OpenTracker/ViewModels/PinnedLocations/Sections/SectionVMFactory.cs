using System;
using System.Collections.Generic;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boolean;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.Sections.Entrance;
using OpenTracker.Models.Sections.Item;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This is the class containing creation logic for section control ViewModel classes.
    /// </summary>
    public class SectionVMFactory : ISectionVMFactory
    {
        private readonly MarkingSectionIconVM.Factory _markingFactory;
        private readonly ISectionIconVM.Factory _iconFactory;
        private readonly PrizeSectionIconVM.Factory _prizeFactory;
        private readonly BossSectionIconVM.Factory _bossFactory;
        private readonly ISectionIconImageProvider.Factory _imageFactory;

        private readonly ISectionVM.Factory _factory;

        public SectionVMFactory(
            MarkingSectionIconVM.Factory markingFactory, ISectionIconVM.Factory iconFactory,
            PrizeSectionIconVM.Factory prizeFactory, BossSectionIconVM.Factory bossFactory,
            ISectionIconImageProvider.Factory imageFactory, ISectionVM.Factory factory)
        {
            _markingFactory = markingFactory;
            _iconFactory = iconFactory;
            _prizeFactory = prizeFactory;
            _bossFactory = bossFactory;
            _imageFactory = imageFactory;

            _factory = factory;
        }

        /// <summary>
        /// Returns the start of the image source string. 
        /// </summary>
        /// <param name="section">
        /// The section data.
        /// </param>
        /// <returns>
        /// A string representing the start of the image source.
        /// </returns>
        private static string GetImageSourceBase(ISection section)
        {
            switch (section)
            {
                case IEntranceSection _:
                    return "avares://OpenTracker/Assets/Images/door";
                case IItemSection _:
                case IShopSection _:
                case ITakeAnySection _:
                    return "avares://OpenTracker/Assets/Images/chest";
                default:
                    throw new ArgumentOutOfRangeException(nameof(section));
            }
        }

        /// <summary>
        /// Returns whether the section label should be visible.
        /// </summary>
        /// <param name="section">
        /// The section data.
        /// </param>
        /// <returns>
        /// A boolean representing whether the section label should be visible.
        /// </returns>
        private static bool GetLabelVisible(ISection section)
        {
            switch (section)
            {
                case IItemSection _:
                    return true;
                case IEntranceSection _:
                case IShopSection _:
                case ITakeAnySection _:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(section));
            }
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
        private List<ISectionIconVM> GetSectionIcons(ISection section)
        {
            var icons = new List<ISectionIconVM>();

            switch (section)
            {
                case IPrizeSection prizeSection:
                    icons.Add(_prizeFactory(prizeSection));
                    icons.Add(_bossFactory(prizeSection.BossPlacement));
                    break;
                case IBossSection bossSection:
                    icons.Add(_bossFactory(bossSection.BossPlacement));
                    break;
                default:
                    if (section.Marking is not null)
                    {
                        icons.Add(_markingFactory(section));
                    }
                    
                    icons.Add(_iconFactory(
                        _imageFactory(section, GetImageSourceBase(section)), section,
                        GetLabelVisible(section)));
                    break;
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
