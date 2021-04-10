using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    ///     This interface contains the dictionary container for boss type requirements. 
    /// </summary>
    public class BossTypeRequirementDictionary : LazyDictionary<BossType, IRequirement>, IBossTypeRequirementDictionary
    {
        private readonly Lazy<IBossTypeRequirementFactory> _factory;
        
        public Lazy<IRequirement> NoBoss { get; }

        public BossTypeRequirementDictionary(IBossTypeRequirementFactory.Factory factory)
            : base(new Dictionary<BossType, IRequirement>())
        {
            _factory = new Lazy<IBossTypeRequirementFactory>(factory());

            NoBoss = new Lazy<IRequirement>(_factory.Value.GetBossTypeRequirement(null));
        }

        protected override IRequirement Create(BossType key)
        {
            return _factory.Value.GetBossTypeRequirement(key);
        }
    }
}