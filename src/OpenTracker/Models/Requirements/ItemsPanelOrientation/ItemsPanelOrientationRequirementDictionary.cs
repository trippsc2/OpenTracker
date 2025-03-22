using System.Collections.Generic;
using Avalonia.Layout;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.ItemsPanelOrientation
{
    /// <summary>
    ///     This class contains the dictionary container for items panel orientation requirements.
    /// </summary>
    public class ItemsPanelOrientationRequirementDictionary : LazyDictionary<Orientation, IRequirement>,
        IItemsPanelOrientationRequirementDictionary
    {
        private readonly IItemsPanelOrientationRequirement.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new items panel orientation requirements.
        /// </param>
        public ItemsPanelOrientationRequirementDictionary(IItemsPanelOrientationRequirement.Factory factory)
            : base(new Dictionary<Orientation, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(Orientation key)
        {
            return _factory(key);
        }
    }
}