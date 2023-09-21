using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Aggregate;

[ExcludeFromCodeCoverage]
public sealed class AggregateRequirementDictionaryTests
{
    private readonly List<IRequirement> _testRequirements = new()
    {
        Substitute.For<IRequirement>(),
        Substitute.For<IRequirement>(),
        Substitute.For<IRequirement>()
    };

    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly AggregateRequirementDictionary _sut;

    public AggregateRequirementDictionaryTests()
    {
        AggregateRequirement Factory(IEnumerable<IRequirement> requirements)
        {
            return new AggregateRequirement(requirements);
        }

        _sut = new AggregateRequirementDictionary(Factory);
    }

    [Theory]
    [InlineData(true, new[] {0, 1}, new[] {0, 1})]
    [InlineData(false, new[] {0, 1}, new[] {0, 2})]
    [InlineData(true, new[] {0, 1}, new[] {1, 0})]
    [InlineData(false, new[] {0, 1}, new[] {1, 2})]
    [InlineData(false, new[] {0, 1}, new[] {2, 0})]
    [InlineData(false, new[] {0, 1}, new[] {2, 1})]
    [InlineData(false, new[] {0, 1}, new[] {0, 1, 2})]
    [InlineData(false, new[] {0, 1}, new[] {0, 2, 1})]
    [InlineData(false, new[] {0, 1}, new[] {1, 0, 2})]
    [InlineData(false, new[] {0, 1}, new[] {1, 2, 0})]
    [InlineData(false, new[] {0, 1}, new[] {2, 0, 1})]
    [InlineData(false, new[] {0, 1}, new[] {2, 1, 0})]
    [InlineData(false, new[] {0, 1, 2}, new[] {0, 1})]
    [InlineData(false, new[] {0, 1, 2}, new[] {0, 2})]
    [InlineData(false, new[] {0, 1, 2}, new[] {1, 0})]
    [InlineData(false, new[] {0, 1, 2}, new[] {1, 2})]
    [InlineData(false, new[] {0, 1, 2}, new[] {2, 0})]
    [InlineData(false, new[] {0, 1, 2}, new[] {2, 1})]
    [InlineData(true, new[] {0, 1, 2}, new[] {0, 1, 2})]
    [InlineData(true, new[] {0, 1, 2}, new[] {0, 2, 1})]
    [InlineData(true, new[] {0, 1, 2}, new[] {1, 0, 2})]
    [InlineData(true, new[] {0, 1, 2}, new[] {1, 2, 0})]
    [InlineData(true, new[] {0, 1, 2}, new[] {2, 0, 1})]
    [InlineData(true, new[] {0, 1, 2}, new[] {2, 1, 0})]
    public void Indexer_ShouldReturnTheExpectedInstance(
        bool expected, int[] hashSetOrder1, int[] hashSetOrder2)
    {
        var hashSet1 = new HashSet<IRequirement>();
        var hashSet2 = new HashSet<IRequirement>();
            
        foreach (var index in hashSetOrder1)
        {
            hashSet1.Add(_testRequirements[index]);
        }
            
        foreach (var index in hashSetOrder2)
        {
            hashSet2.Add(_testRequirements[index]);
        }

        var requirement1 = _sut[hashSet1];
        var requirement2 = _sut[hashSet2];

        if (expected)
        {
            Assert.Equal(requirement1, requirement2);
            return;
        }
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToSingleInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<IAggregateRequirementDictionary>();

        sut1.Should().BeOfType<AggregateRequirementDictionary>();
        
        var sut2 = scope.Resolve<IAggregateRequirementDictionary>();

        sut1.Should().BeSameAs(sut2);
    }
}