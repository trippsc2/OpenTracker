using System;
using System.Collections.Generic;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using OpenTracker.Models.RequirementNodes;

namespace OpenTracker.UnitTests.Models.RequirementNodes
{
    public class RequirementNodeTests
    {
        private readonly IMode _mode = new Mode();
        private readonly List<INodeConnection> _nodeConnections = new List<INodeConnection>
        {
            Substitute.For<INodeConnection>(),
            Substitute.For<INodeConnection>()
        };
        private readonly IRequirementNodeDictionary _requirementNodes = Substitute.For<IRequirementNodeDictionary>();
        private readonly IRequirementNodeFactory _factory = Substitute.For<IRequirementNodeFactory>();

        private RequirementNode BuildRequirementNode(bool start = false)
        {
            var requirementNode = new RequirementNode(_mode, _requirementNodes, _factory, start);
            _factory.GetNodeConnections(
                Arg.Any<RequirementNodeID>(), Arg.Any<IRequirementNode>()).Returns(_nodeConnections);
            _requirementNodes.ItemCreated +=
                Raise.Event<EventHandler<KeyValuePair<RequirementNodeID, IRequirementNode>>>(
                    _requirementNodes, new KeyValuePair<RequirementNodeID, IRequirementNode>(
                        RequirementNodeID.LightWorld, requirementNode));

            return requirementNode;
        }
    }
}