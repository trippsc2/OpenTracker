using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Alternative;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Alternative;

public class AlternativeRequirementDictionaryTests
{
    private readonly List<IRequirement> _testRequirements = new()
    {
        Substitute.For<IRequirement>(),
        Substitute.For<IRequirement>(),
        Substitute.For<IRequirement>()
    };

    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly AlternativeRequirementDictionary _sut;

    public AlternativeRequirementDictionaryTests()
    {
        IAlternativeRequirement Factory(IList<IRequirement> requirements)
        {
            return Substitute.For<IAlternativeRequirement>();
        }

        _sut = new AlternativeRequirementDictionary(Factory);
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
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IAlternativeRequirementDictionary>();
            
        Assert.NotNull(sut as AlternativeRequirementDictionary);
    }
}