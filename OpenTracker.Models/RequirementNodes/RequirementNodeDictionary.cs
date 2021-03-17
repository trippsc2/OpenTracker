using System;
using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This class contains the dictionary container for requirement node data.
    /// </summary>
    public class RequirementNodeDictionary : LazyDictionary<RequirementNodeID, IRequirementNode>,
        IRequirementNodeDictionary
    {
        private readonly Lazy<IRequirementNodeFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The requirement node factory.
        /// </param>
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
