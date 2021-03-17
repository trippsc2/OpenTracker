using System;
using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains the dictionary container for requirement data.
    /// </summary>
    public class RequirementDictionary : LazyDictionary<RequirementType, IRequirement>,
        IRequirementDictionary
    {
        private readonly Lazy<IRequirementFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The requirement factory.
        /// </param>
        public RequirementDictionary(IRequirementFactory.Factory factory)
            : base(new Dictionary<RequirementType, IRequirement>())
        {
            _factory = new Lazy<IRequirementFactory>(() => factory());
        }

        protected override IRequirement Create(RequirementType key)
        {
            return _factory.Value.GetRequirement(key);
        }
    }
}
