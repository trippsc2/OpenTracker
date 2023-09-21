using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.Modes;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Models.SaveLoad;

[ExcludeFromCodeCoverage]
public sealed class SaveLoadManagerTests
{
    private readonly IJsonConverter _jsonConverter = Substitute.For<IJsonConverter>();

    private readonly IMode _mode = Substitute.For<IMode>();
    private readonly IItemDictionary _items = Substitute.For<IItemDictionary>();
    private readonly ILocationDictionary _locations = Substitute.For<ILocationDictionary>();
    private readonly IBossPlacementDictionary _bossPlacements = Substitute.For<IBossPlacementDictionary>();
    private readonly IPrizePlacementDictionary _prizePlacements = Substitute.For<IPrizePlacementDictionary>();
    private readonly IMapConnectionCollection _connections = Substitute.For<IMapConnectionCollection>();
    private readonly IDropdownDictionary _dropdowns = Substitute.For<IDropdownDictionary>();
    private readonly IPinnedLocationCollection _pinnedLocations = Substitute.For<IPinnedLocationCollection>();
    private readonly ISequenceBreakDictionary _sequenceBreaks = Substitute.For<ISequenceBreakDictionary>();

    private readonly SaveLoadManager _sut;

    public SaveLoadManagerTests()
    {
        _sut = new SaveLoadManager(
            _jsonConverter, _mode, _items, _locations, _bossPlacements, _prizePlacements,
            () => _connections, _dropdowns, _pinnedLocations, _sequenceBreaks);
    }

    [Fact]
    public void CurrentFilePath_ShouldRaisePropertyChanged()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
            
        Assert.PropertyChanged(_sut, nameof(ISaveLoadManager.CurrentFilePath), () => 
            _sut.Open(path));
    }

    [Fact]
    public void Open_ShouldThrowException_WhenSaveDataIsNull()
    {
        var path = string.Empty;
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).ReturnsNull();

        Assert.Throws<NullReferenceException>(() => _sut.Open(path));
    }

    [Fact]
    public void Open_ShouldCallLoadOnMode()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _mode.Received().Load(Arg.Any<ModeSaveData?>());
    }

    [Fact]
    public void Open_ShouldCallLoadOnItems()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _items.Received().Load(Arg.Any<IDictionary<ItemType, ItemSaveData>?>());
    }

    [Fact]
    public void Open_ShouldCallLoadOnLocations()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _locations.Received().Load(Arg.Any<IDictionary<LocationID, LocationSaveData>?>());
    }

    [Fact]
    public void Open_ShouldCallLoadOnBossPlacements()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _bossPlacements.Received().Load(Arg.Any<IDictionary<BossPlacementID, BossPlacementSaveData>?>());
    }

    [Fact]
    public void Open_ShouldCallLoadOnPrizePlacements()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _prizePlacements.Received().Load(Arg.Any<IDictionary<PrizePlacementID, PrizePlacementSaveData>?>());
    }

    [Fact]
    public void Open_ShouldCallLoadOnConnections()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _connections.Received().Load(Arg.Any<IList<ConnectionSaveData>?>());
    }

    [Fact]
    public void Open_ShouldCallLoadOnDropdowns()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _dropdowns.Received().Load(Arg.Any<IDictionary<DropdownID, DropdownSaveData>?>());
    }

    [Fact]
    public void Open_ShouldCallLoadOnPinnedLocations()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        _pinnedLocations.Received().Load(Arg.Any<IList<LocationID>?>());
    }

    [Fact]
    public void Open_ShouldSetCurrentFilePath()
    {
        const string? path = "Test";
        SaveData saveData = new();
        _jsonConverter.Load<SaveData>(Arg.Any<string>()).Returns(saveData);
        _sut.Open(path);
            
        Assert.Equal(path, _sut.CurrentFilePath);
    }

    [Fact]
    public void Save_ShouldCallSaveOnMode()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _mode.Received().Save();
    }

    [Fact]
    public void Save_ShouldCallSaveOnItems()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _items.Received().Save();
    }

    [Fact]
    public void Save_ShouldCallSaveOnLocations()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _locations.Received().Save();
    }

    [Fact]
    public void Save_ShouldCallSaveOnBossPlacements()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _bossPlacements.Received().Save();
    }

    [Fact]
    public void Save_ShouldCallSaveOnPrizePlacements()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _prizePlacements.Received().Save();
    }

    [Fact]
    public void Save_ShouldCallSaveOnConnections()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _connections.Received().Save();
    }

    [Fact]
    public void Save_ShouldCallSaveOnDropdowns()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _dropdowns.Received().Save();
    }

    [Fact]
    public void Save_ShouldCallSaveOnPinnedLocations()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        _pinnedLocations.Received().Save();
    }

    [Fact]
    public void Save_ShouldSetCurrentFilePath()
    {
        const string? path = "Test";
        _sut.Save(path);
            
        Assert.Equal(path, _sut.CurrentFilePath);
    }

    [Fact]
    public void OpenSequenceBreaks_ShouldCallLoadOnSequenceBreaks()
    {
        const string? path = "Test";
        Dictionary<SequenceBreakType, SequenceBreakSaveData> saveData = new();
        _jsonConverter.Load<Dictionary<SequenceBreakType, SequenceBreakSaveData>>(Arg.Any<string>()).Returns(saveData);
        _sut.OpenSequenceBreaks(path);
            
        _sequenceBreaks.Received().Load(Arg.Any<Dictionary<SequenceBreakType, SequenceBreakSaveData>?>());
    }

    [Fact]
    public void SaveSequenceBreaks_ShouldCallSaveOnSequenceBreaks()
    {
        const string? path = "Test";
        _sut.SaveSequenceBreaks(path);
            
        _sequenceBreaks.Received().Save();
    }
}