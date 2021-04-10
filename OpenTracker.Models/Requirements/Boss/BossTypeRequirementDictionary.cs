using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Boss
{
    public class BossTypeRequirementDictionary : LazyDictionary<BossType, IRequirement>
    {
        public BossTypeRequirementDictionary()
            : base(new Dictionary<BossType, IRequirement>())
        {
        }

        protected override IRequirement Create(BossType key)
        {
            throw new System.NotImplementedException();
        }
    }
}