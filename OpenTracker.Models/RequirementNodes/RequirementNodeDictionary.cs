using OpenTracker.Utils;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This is the dictionary containing all requirement node data
    /// </summary>
    public class RequirementNodeDictionary : LazyDictionary<RequirementNodeID, IRequirementNode>,
        IRequirementNodeDictionary
    {
        private readonly Lazy<IRequirementNodeFactory> _factory;

        public RequirementNodeDictionary(IRequirementNodeFactory.Factory factory)
            : base(new Dictionary<RequirementNodeID, IRequirementNode>())
        {
            _factory = new Lazy<IRequirementNodeFactory>(() => factory());
        }

        protected override IRequirementNode Create(RequirementNodeID key)
        {
            return _factory.Value.GetRequirementNode(key);
        }
    }
}
