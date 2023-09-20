using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dungeons.KeyDoors;
using OpenTracker.Models.Dungeons.Mutable;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyDoors;

[ExcludeFromCodeCoverage]
public sealed class KeyDoorDictionaryTests
{
    private readonly IKeyDoorFactory _factory = Substitute.For<IKeyDoorFactory>();
    private readonly IMutableDungeon _dungeonData = Substitute.For<IMutableDungeon>();
        
    private readonly KeyDoorDictionary _sut;

    public KeyDoorDictionaryTests()
    {
        _sut = new KeyDoorDictionary(() => _factory, _dungeonData);
    }
        

    [Fact]
    public void PopulateDoors_ShouldCreateSpecifiedDoors()
    {
        var items = new List<KeyDoorID>
        {
            KeyDoorID.ATFirstKeyDoor
        };
            
        _sut.PopulateDoors(items);

        Assert.Contains(KeyDoorID.ATFirstKeyDoor, _sut.Keys);
    }
    
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IKeyDoorDictionary.Factory>();
        var sut = factory(_dungeonData);
            
        Assert.NotNull(sut as KeyDoorDictionary);
    }
}