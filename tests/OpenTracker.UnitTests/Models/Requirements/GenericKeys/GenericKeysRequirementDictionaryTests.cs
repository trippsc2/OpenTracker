using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.GenericKeys;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.GenericKeys;

[ExcludeFromCodeCoverage]
public sealed class GenericKeysRequirementDictionaryTests
{
    private readonly IMode _mode = Substitute.For<IMode>();
    
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly GenericKeysRequirementDictionary _sut;

    public GenericKeysRequirementDictionaryTests()
    {
        _sut = new GenericKeysRequirementDictionary(Factory);
        return;

        GenericKeysRequirement Factory(bool expectedValue)
        {
            return new GenericKeysRequirement(_mode, expectedValue);
        }
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[false];
        var requirement2 = _sut[false];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnTheDifferentInstances()
    {
        var requirement1 = _sut[false];
        var requirement2 = _sut[true];
            
        Assert.NotEqual(requirement1, requirement2);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IGenericKeysRequirementDictionary>();
            
        Assert.NotNull(sut as GenericKeysRequirementDictionary);
    }
}