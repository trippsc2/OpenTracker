using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the dictionary containing requirement data
    /// </summary>
    public class RequirementDictionary : LazyDictionary<RequirementType, IRequirement>,
        IRequirementDictionary
    {
        private readonly IRequirementFactory.Factory _factory;

        public RequirementDictionary(IRequirementFactory.Factory factory)
            : base(new Dictionary<RequirementType, IRequirement>())
        {
            _factory = factory;
        }

        protected override IRequirement Create(RequirementType key)
        {
            return _factory().GetRequirement(key);
        }
    }
}
