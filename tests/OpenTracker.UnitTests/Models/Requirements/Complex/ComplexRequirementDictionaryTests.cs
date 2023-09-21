using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Requirements.Complex;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Complex;

[ExcludeFromCodeCoverage]
public sealed class ComplexRequirementDictionaryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly ComplexRequirementDictionary _sut;

    public ComplexRequirementDictionaryTests()
    {
        _sut = new ComplexRequirementDictionary(() => Substitute.For<IComplexRequirementFactory>());
    }

    [Fact]
    public void Indexer_ShouldReturnTheSameInstance()
    {
        var requirement1 = _sut[ComplexRequirementType.AllMedallions];
        var requirement2 = _sut[ComplexRequirementType.AllMedallions];
            
        Assert.Equal(requirement1, requirement2);
    }

    [Fact]
    public void Indexer_ShouldReturnDifferentInstances()
    {
        var requirement1 = _sut[ComplexRequirementType.AllMedallions];
        var requirement2 = _sut[ComplexRequirementType.ExtendMagic1];
            
        Assert.NotEqual(requirement1, requirement2);
    }
        
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IComplexRequirementDictionary>();
            
        Assert.NotNull(sut as ComplexRequirementDictionary);
    }
}