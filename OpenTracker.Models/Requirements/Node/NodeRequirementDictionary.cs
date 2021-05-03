using System.Collections.Generic;
using OpenTracker.Models.Nodes;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Node
{
    /// <summary>
    ///     This class contains the dictionary container for node requirements.
    /// </summary>
    public class NodeRequirementDictionary : LazyDictionary<OverworldNodeID, IRequirement>, INodeRequirementDictionary
    {
        private readonly IOverworldNodeDictionary _overworldNodes;
        
        private readonly INodeRequirement.Factory _factory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="overworldNodes">
        ///     The overworld node dictionary.
        /// </param>
        /// <param name="factory">
        ///     An Autofac factory for creating new node requirements.
        /// </param>
        public NodeRequirementDictionary(IOverworldNodeDictionary overworldNodes, INodeRequirement.Factory factory)
            : base(new Dictionary<OverworldNodeID, IRequirement>())
        {
            _overworldNodes = overworldNodes;
            
            _factory = factory;
        }

        protected override IRequirement Create(OverworldNodeID key)
        {
            return _factory(_overworldNodes[key]);
        }
    }
}