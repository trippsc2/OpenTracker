using System.Collections.Generic;
using System.Linq;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Alternative
{
    /// <summary>
    /// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IAlternativeRequirement"/> objects indexed by <see cref="HashSet{T}"/> of <see cref="IRequirement"/>.
    /// </summary>
    public class AlternativeRequirementDictionary : LazyDictionary<HashSet<IRequirement>, IRequirement>,
        IAlternativeRequirementDictionary
    {
        private readonly IAlternativeRequirement.Factory _factory;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        ///     An Autofac factory for creating new <see cref="IAlternativeRequirement"/> objects.
        /// </param>
        public AlternativeRequirementDictionary(IAlternativeRequirement.Factory factory)
            : base(new Dictionary<HashSet<IRequirement>, IRequirement>(HashSet<IRequirement>.CreateSetComparer()))
        {
            _factory = factory;
        }

        protected override IRequirement Create(HashSet<IRequirement> key)
        {
            return _factory(key.ToList());
        }
    }
}