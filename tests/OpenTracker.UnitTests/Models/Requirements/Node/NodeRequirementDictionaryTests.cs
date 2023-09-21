using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Nodes;
using OpenTracker.Models.Requirements.Node;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Node;

[ExcludeFromCodeCoverage]
public sealed class NodeRequirementDictionaryTests
{
    private readonly IOverworldNodeDictionary _overworldNodes = Substitute.For<IOverworldNodeDictionary>();

    private readonly NodeRequirementDictionary _sut;

    public NodeRequirementDictionaryTests()
    {
        _sut = new NodeRequirementDictionary(_overworldNodes, node => new NodeRequirement(node));
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[OverworldNodeID.LightWorld];
        var requirement2 = _sut[OverworldNodeID.LightWorld];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnDifferentInstances()
    {
        var requirement1 = _sut[OverworldNodeID.LightWorld];
        var requirement2 = _sut[OverworldNodeID.LightWorldInverted];
            
        Assert.NotEqual(requirement1, requirement2);
    }
        
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<INodeRequirementDictionary>();
            
        Assert.NotNull(sut as NodeRequirementDictionary);
    }
}