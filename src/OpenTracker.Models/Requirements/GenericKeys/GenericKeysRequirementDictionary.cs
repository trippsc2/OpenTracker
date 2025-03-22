using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.GenericKeys
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IGenericKeysRequirement"/> objects indexed by <see cref="bool"/>.
    /// </summary>
    public class GenericKeysRequirementDictionary : LazyDictionary<bool, IRequirement>,
        IGenericKeysRequirementDictionary
    {
        private readonly IGenericKeysRequirement.Factory _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IGenericKeysRequirement"/> objects.
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