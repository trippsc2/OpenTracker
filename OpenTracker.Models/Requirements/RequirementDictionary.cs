using OpenTracker.Utils;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the dictionary containing requirement data
    /// </summary>
    public class RequirementDictionary : LazyDictionary<RequirementType, IRequirement>,
        IRequirementDictionary
    {
        private readonly Lazy<IRequirementFactory> _factory;

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
