using System;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking
{
    public class AutoTrackerTests
    {
        private readonly ISNESConnector _snesConnector;
        private readonly AutoTracker _sut;

        public AutoTrackerTests()
        {
            _snesConnector = Substitute.For<ISNESConnector>();
            _sut = new AutoTracker(new MemoryAddressProvider(() => new MemoryAddress()), _snesConnector);
        }
        
        [Theory]
        [InlineData(ConnectionStatus.NotConnected, ConnectionStatus.NotConnected)]
        [InlineData(ConnectionStatus.Connecting, ConnectionStatus.Connecting)]
        [InlineData(ConnectionStatus.SelectDevice, ConnectionStatus.SelectDevice)]
        [InlineData(ConnectionStatus.Attaching, ConnectionStatus.Attaching)]
        [InlineData(ConnectionStatus.Connected, ConnectionStatus.Connected)]
        [InlineData(ConnectionStatus.Error, ConnectionStatus.Error)]
        public void StatusChanged_ShouldBeEqualToExpected(ConnectionStatus expected, ConnectionStatus eventValue)
        {
            _snesConnector.StatusChanged += Raise.Event<EventHandler<ConnectionStatus>>(
                _snesConnector, eventValue);
            
            Assert.Equal(expected, _sut.Status);
        }

        [Fact]
        public async void Devices_ShouldReturnEmptyList_WhenNullReceivedFromConnector()
        {
            _snesConnector.GetDevices().ReturnsNull();

            await _sut.GetDevices();
            
            Assert.Empty(_sut.Devices);
        }

        [Fact]
        public async void Devices_ShouldReturnReceivedList()
        {
            _snesConnector.GetDevices().Returns(new[]
            {
                "Test 1",
                "Test 2",
                "Test 3"
            });

            await _sut.GetDevices();
            
            Assert.Collection(_sut.Devices,
                item => Assert.Equal("Test 1", item),
                item => Assert.Equal("Test 2", item),
                item => Assert.Equal("Test 3", item));
        }

        [Theory]
        [InlineData(true, ConnectionStatus.NotConnected)]
        [InlineData(false, ConnectionStatus.Connecting)]
        [InlineData(false, ConnectionStatus.SelectDevice)]
        [InlineData(false, ConnectionStatus.Attaching)]
        [InlineData(false, ConnectionStatus.Connected)]
        [InlineData(false, ConnectionStatus.Error)]
        public void CanConnect_ShouldReturnExpected(bool expected, ConnectionStatus status)
        {
            _snesConnector.StatusChanged += Raise.Event<EventHandler<ConnectionStatus>>(
                _snesConnector, status);
            
            Assert.Equal(expected, _sut.CanConnect());
        }

        [Theory]
        [InlineData(false, ConnectionStatus.NotConnected)]
        [InlineData(false, ConnectionStatus.Connecting)]
        [InlineData(true, ConnectionStatus.SelectDevice)]
        [InlineData(false, ConnectionStatus.Attaching)]
        [InlineData(false, ConnectionStatus.Connected)]
        [InlineData(false, ConnectionStatus.Error)]
        public void CanGetDevices_ShouldReturnExpected(bool expected, ConnectionStatus status)
        {
            _snesConnector.StatusChanged += Raise.Event<EventHandler<ConnectionStatus>>(
                _snesConnector, status);
            
            Assert.Equal(expected, _sut.CanGetDevices());
        }

        [Theory]
        [InlineData(false, ConnectionStatus.NotConnected)]
        [InlineData(true, ConnectionStatus.Connecting)]
        [InlineData(true, ConnectionStatus.SelectDevice)]
        [InlineData(true, ConnectionStatus.Attaching)]
        [InlineData(true, ConnectionStatus.Connected)]
        [InlineData(true, ConnectionStatus.Error)]
        public void CanDisconnect_ShouldReturnExpected(bool expected, ConnectionStatus status)
        {
            _snesConnector.StatusChanged += Raise.Event<EventHandler<ConnectionStatus>>(
                _snesConnector, status);
            
            Assert.Equal(expected, _sut.CanDisconnect());
        }

        [Theory]
        [InlineData(false, ConnectionStatus.NotConnected)]
        [InlineData(false, ConnectionStatus.Connecting)]
        [InlineData(true, ConnectionStatus.SelectDevice)]
        [InlineData(false, ConnectionStatus.Attaching)]
        [InlineData(false, ConnectionStatus.Connected)]
        [InlineData(false, ConnectionStatus.Error)]
        public void CanStart_ShouldReturnExpected(bool expected, ConnectionStatus status)
        {
            _snesConnector.StatusChanged += Raise.Event<EventHandler<ConnectionStatus>>(
                _snesConnector, status);
            
            Assert.Equal(expected, _sut.CanStart());
        }
    }
}