using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.BossShuffle
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IBossShuffleRequirement"/> objects indexed by <see cref="bool"/>.
    /// </summary>
    public class BossShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, IBossShuffleRequirementDictionary
    {
        private readonly IBossShuffleRequirement.Factory _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IBossShuffleRequirement"/> objects.
        /// </param>
        public BossShuffleRequirementDictionary(IBossShuffleRequirement.Factory factory)
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