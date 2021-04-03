using System;
using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Nodes
{
    /// <summary>
    /// This class contains the dictionary container for requirement node data.
    /// </summary>
    public class OverworldNodeDictionary : LazyDictionary<OverworldNodeID, INode>,
        IOverworldNodeDictionary
    {
        private readonly Lazy<IOverworldNodeFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// The requirement node factory.
        /// </param>
        public OverworldNodeDictionary(IOverworldNodeFactory.Factory factory)
            : base(new Dictionary<OverworldNodeID, INode>())
        {
            _factory = new Lazy<IOverworldNodeFactory>(() => factory());
        }

        protected override INode Create(OverworldNodeID key)
        {
            return _factory.Value.GetOverworldNode(key);
        }
    }
}
