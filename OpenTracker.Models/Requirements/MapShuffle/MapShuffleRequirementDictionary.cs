using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.MapShuffle
{
    /// <summary>
    ///     This class contains the dictionary container for map shuffle 
    /// </summary>
    public class MapShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>, IMapShuffleRequirementDictionary
    {
        private readonly IMapShuffleRequirement.Factory _factory;
        
        public MapShuffleRequirementDictionary(IMapShuffleRequirement.Factory factory)
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