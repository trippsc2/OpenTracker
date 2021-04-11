using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.TakeAnyLocations
{
    /// <summary>
    ///     This class contains the dictionary container for take any locations requirements.
    /// </summary>
    public class TakeAnyLocationsRequirementDictionary : LazyDictionary<bool, IRequirement>, ITakeAnyLocationsRequirementDictionary
    {
        private readonly ITakeAnyLocationsRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new take any locations requirements.
        /// </param>
        public TakeAnyLocationsRequirementDictionary(ITakeAnyLocationsRequirement.Factory factory)
            : base(new Dictionary<bool, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(bool key)
        {
            return _factory(key);
        }
    }
}