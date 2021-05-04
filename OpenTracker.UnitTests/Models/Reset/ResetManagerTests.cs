using Autofac;
using NSubstitute;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Locations.Map.Connections;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Reset;
using OpenTracker.Models.UndoRedo;
using Xunit;

namespace OpenTracker.UnitTests.Models.Reset
{
    public class ResetManagerTests
    {
        private readonly IAutoTracker _autoTracker = Substitute.For<IAutoTracker>();
        private readonly IBossPlacementDictionary _bossPlacements = Substitute.For<IBossPlacementDictionary>();
        private readonly IConnectionCollection _connections = Substitute.For<IConnectionCollection>();
        private readonly IDropdownDictionary _dropdowns = Substitute.For<IDropdownDictionary>();
        private readonly IItemDictionary _items = Substitute.For<IItemDictionary>();
        private readonly ILocationDictionary _locations = Substitute.For<ILocationDictionary>();
        private readonly IPinnedLocationCollection _pinnedLocations = Substitute.For<IPinnedLocationCollection>();
        private readonly IPrizePlacementDictionary _prizePlacements = Substitute.For<IPrizePlacementDictionary>();
        private readonly IUndoRedoManager _undoRedoManager = Substitute.For<IUndoRedoManager>();

        private readonly ResetManager _sut;

        public ResetManagerTests()
        {
            _sut = new ResetManager(
                _autoTracker, _bossPlacements, _connections, _dropdowns, _items, _locations, _pinnedLocations,
                _prizePlacements, _undoRedoManager);
        }

        [Fact]
        public void Reset_ShouldCallResetOnUndoRedoManager()
        {
            _sut.Reset();
            
            _undoRedoManager.Received(1).Reset();
        }

        [Fact]
        public void Reset_ShouldCallClearOnPinnedLocations()
        {
            _sut.Reset();
            
            _pinnedLocations.Received(1).Clear();
        }

        [Fact]
        public async void Reset_ShouldCallDisconnectOnAutoTracker()
        {
            _sut.Reset();
            
            await _autoTracker.Received(1).Disconnect();
        }

        [Fact]
        public void Reset_ShouldCallResetOnBossPlacements()
        {
            _sut.Reset();
            
            _bossPlacements.Received(1).Reset();
        }

        [Fact]
        public void Reset_ShouldCallResetOnLocations()
        {
            _sut.Reset();
            
            _locations.Received(1).Reset();
        }

        [Fact]
        public void Reset_ShouldCallResetOnPrizePlacements()
        {
            _sut.Reset();
            
            _prizePlacements.Received(1).Reset();
        }

        [Fact]
        public void Reset_ShouldCallResetOnItems()
        {
            _sut.Reset();
            
            _items.Received(1).Reset();
        }

        [Fact]
        public void Reset_ShouldCallResetOnDropdowns()
        {
            _sut.Reset();
            
            _dropdowns.Received(1).Reset();
        }

        [Fact]
        public void Reset_ShouldCallClearOnConnections()
        {
            _sut.Reset();
            
            _connections.Received(1).Clear();
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IResetManager>();
            
            Assert.NotNull(sut as ResetManager);
        }

        [Fact]
        public void AutofacSingleInstanceTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var value1 = scope.Resolve<IResetManager>();
            var value2 = scope.Resolve<IResetManager>();
            
            Assert.Equal(value1, value2);
        }
    }
}