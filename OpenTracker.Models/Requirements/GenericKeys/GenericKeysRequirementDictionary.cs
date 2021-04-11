using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.GenericKeys
{
    /// <summary>
    ///     This class contains the dictionary container for generic keys requirements.
    /// </summary>
    public class GenericKeysRequirementDictionary : LazyDictionary<bool, IRequirement>, IGenericKeysRequirementDictionary
    {
        private readonly IGenericKeysRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating generic keys requirements.
        /// </param>
        public GenericKeysRequirementDictionary(IGenericKeysRequirement.Factory factory)
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