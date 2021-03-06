using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Static
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IStaticRequirement"/>
    /// objects indexed by <see cref="AccessibilityLevel"/>.
    /// </summary>
    public class StaticRequirementDictionary : LazyDictionary<AccessibilityLevel, IRequirement>,
        IStaticRequirementDictionary
    {
        private readonly IStaticRequirement.Factory _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IStaticRequirement"/> objects.
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