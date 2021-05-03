using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Static
{
    /// <summary>
    ///     This class contains the dictionary container for static requirements.
    /// </summary>
    public class StaticRequirementDictionary : LazyDictionary<AccessibilityLevel, IRequirement>, IStaticRequirementDictionary
    {
        private readonly IStaticRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new static requirements.
        /// </param>
        public StaticRequirementDictionary(IStaticRequirement.Factory factory)
            : base(new Dictionary<AccessibilityLevel, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(AccessibilityLevel key)
        {
            return _factory(key);
        }
    }
}