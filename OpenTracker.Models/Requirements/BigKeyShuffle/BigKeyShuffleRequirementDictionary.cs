using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.BigKeyShuffle
{
    /// <summary>
    ///     This class contains the dictionary container for big key shuffle requirements.
    /// </summary>
    public class BigKeyShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, IBigKeyShuffleRequirementDictionary
    {
        private readonly IBigKeyShuffleRequirement.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new big key shuffle requirements.
        /// </param>
        public BigKeyShuffleRequirementDictionary(IBigKeyShuffleRequirement.Factory factory)
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