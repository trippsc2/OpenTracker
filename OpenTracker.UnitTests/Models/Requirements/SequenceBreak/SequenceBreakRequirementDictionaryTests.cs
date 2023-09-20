using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.SequenceBreak;

[ExcludeFromCodeCoverage]
public sealed class SequenceBreakRequirementDictionaryTests
{
    private readonly ISequenceBreakDictionary _sequenceBreaks = Substitute.For<ISequenceBreakDictionary>();

    private readonly SequenceBreakRequirementDictionary _sut;

    public SequenceBreakRequirementDictionaryTests()
    {
        _sut = new SequenceBreakRequirementDictionary(
            _sequenceBreaks, _ => Substitute.For<ISequenceBreakRequirement>());
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[SequenceBreakType.BlindPedestal];
        var requirement2 = _sut[SequenceBreakType.BlindPedestal];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnDifferentInstances()
    {
        var requirement1 = _sut[SequenceBreakType.BlindPedestal];
        var requirement2 = _sut[SequenceBreakType.BonkOverLedge];
            
        Assert.NotEqual(requirement1, requirement2);
    }
        
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<ISequenceBreakRequirementDictionary>();
            
        Assert.NotNull(sut as SequenceBreakRequirementDictionary);
    }
}