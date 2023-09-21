using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Mutable;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.Mutable;

[ExcludeFromCodeCoverage]
public sealed class MutableDungeonQueueTests
{
    private readonly IDungeon _dungeon = Substitute.For<IDungeon>();
    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Ctor_ShouldCreateExpectedNumberOfInstances(int expected, int count)
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        var sut = new MutableDungeonQueue(_ => _dungeonData, _dungeon, count);
            
        Assert.Equal(expected, sut.Count);
    }

    [Fact]
    public void Ctor_ShouldCreateProcessorCountMinusOne()
    {
        var expected = Math.Max(1, Environment.ProcessorCount - 1);
        // ReSharper disable once CollectionNeverUpdated.Local
        var sut = new MutableDungeonQueue(_ => _dungeonData, _dungeon);
            
        Assert.Equal(expected, sut.Count);
    }

    [Fact]
    public void AutofacTests()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IMutableDungeonQueue.Factory>();
        var sut = factory(_dungeon);
            
        Assert.NotNull(sut as MutableDungeonQueue);
    }
}