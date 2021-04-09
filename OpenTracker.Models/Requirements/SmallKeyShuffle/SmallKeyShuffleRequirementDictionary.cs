using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.SmallKeyShuffle
{
    /// <summary>
    ///     This class contains the dictionary container for small key shuffle requirements.
    /// </summary>
    public class SmallKeyShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, ISmallKeyShuffleRequirementDictionary
    {
        private readonly ISmallKeyShuffleRequirement.Factory _factory;
        
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new small key shuffle requirements.
        /// </param>
        public SmallKeyShuffleRequirementDictionary(ISmallKeyShuffleRequirement.Factory factory)
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