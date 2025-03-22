using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.CompassShuffle
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="ICompassShuffleRequirement"/> objects indexed by <see cref="bool"/>.
    /// </summary>
    public class CompassShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>,
        ICompassShuffleRequirementDictionary
    {
        private readonly ICompassShuffleRequirement.Factory _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="ICompassShuffleRequirement"/> objects.
        /// </param>
        public CompassShuffleRequirementDictionary(ICompassShuffleRequirement.Factory factory)
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