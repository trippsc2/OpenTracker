using System;
using System.ComponentModel;
using Autofac;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking;

public class AutoTrackerTests
{
    private readonly ISNESConnector _snesConnector;
    private readonly IMemoryAddressProvider _memoryAddressProvider = new MemoryAddressProvider();
        
    private readonly AutoTracker _sut;

    public AutoTrackerTests()
    {
        _snesConnector = Substitute.For<ISNESConnector>();
        _sut = new AutoTracker(_memoryAddressProvider, _snesConnector);
    }
        
    [Theory]
    [InlineData(ConnectionStatus.NotConnected, ConnectionStatus.NotConnected)]
    [InlineData(ConnectionStatus.Connecting, ConnectionStatus.Connecting)]
    [InlineData(ConnectionStatus.SelectDevice, ConnectionStatus.SelectDevice)]
    [InlineData(ConnectionStatus.Attaching, ConnectionStatus.Attaching)]
    [InlineData(ConnectionStatus.Connected, ConnectionStatus.Connected)]
    [InlineData(ConnectionStatus.Error, ConnectionStatus.Error)]
    public void StatusChanged_ShouldBeEqualToExpected(ConnectionStatus expected, ConnectionStatus connectorStatus)
    {
        _snesConnector.Status.Returns(connectorStatus);
            
        Assert.Equal(expected, _sut.Status);
    }
        
    [Fact]
    public async void Devices_ShouldReturnEmptyList_WhenNullReceivedFromConnector()
    {
        _snesConnector.GetDevicesAsync().ReturnsNull();

        await _sut.GetDevices();
            
        Assert.Empty(_sut.Devices);
    }

    [Fact]
    public async void Devices_ShouldReturnReceivedList()
    {
        _snesConnector.GetDevicesAsync().Returns(new[]
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

    [Fact]
    public async void Devices_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        _snesConnector.GetDevicesAsync().Returns(new[]
        {
            "Test 1",
            "Test 2",
            "Test 3"
        });

        await Assert.PropertyChangedAsync(_sut, nameof(IAutoTracker.Devices), () => _sut.GetDevices());
    }

    [Fact]
    public void RaceIllegalTracking_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        Assert.PropertyChanged(
            _sut, nameof(IAutoTracker.RaceIllegalTracking), () => _sut.RaceIllegalTracking = true);
    }

    [Fact]
    public void Status_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        Assert.PropertyChanged(
            _sut, nameof(IAutoTracker.Status), () =>
                _snesConnector.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _snesConnector, new PropertyChangedEventArgs(nameof(ISNESConnector.Status))));
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
        _snesConnector.Status.Returns(status);
            
        Assert.Equal(expected, _sut.CanConnect());
    }

    [Fact]
    public async void Connect_ShouldCallSetUriOnSNESConnector()
    {
        const string uriString = "Test";
        await _sut.Connect(uriString);
            
        _snesConnector.Received().SetURI(uriString);
    }

    [Fact]
    public async void Connect_ShouldCallConnectOnSNESConnector()
    {
        const string uriString = "Test";
        await _sut.Connect(uriString);

        await _snesConnector.Received().ConnectAsync();
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
        _snesConnector.Status.Returns(status);
            
        Assert.Equal(expected, _sut.CanGetDevices());
    }

    [Fact]
    public async void GetDevices_ShouldCallGetDevicesOnSNESConnector()
    {
        await _sut.GetDevices();

        await _snesConnector.Received().GetDevicesAsync();
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
        _snesConnector.Status.Returns(status);
            
        Assert.Equal(expected, _sut.CanDisconnect());
    }

    [Fact]
    public async void Disconnect_ShouldCallDisconnectOnSNESConnector()
    {
        await _sut.Disconnect();

        await _snesConnector.Received().DisconnectAsync();
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
        _snesConnector.Status.Returns(status);
            
        Assert.Equal(expected, _sut.CanStart());
    }

    [Fact]
    public async void Start_ShouldCallSetDeviceOnSNESConnector()
    {
        const string device = "Test";
        await _sut.Start(device);

        await _snesConnector.Received().AttachDeviceAsync(device);
    }

    [Fact]
    public async void InGameCheck_ShouldDoNothing_WhenCanReadMemoryReturnsFalse()
    {
        await _sut.InGameCheck();
            
        await _snesConnector.DidNotReceive().ReadMemoryAsync(Arg.Any<ulong>());
    }

    [Fact]
    public async void InGameCheck_ShouldCallRead_WhenCanReadMemoryReturnsTrue()
    {
        _snesConnector.Status.Returns(ConnectionStatus.Connected);
        _snesConnector.ReadMemoryAsync(0x7e0010).ReturnsNull();
        await _sut.InGameCheck();

        await _snesConnector.Received().ReadMemoryAsync(0x7e0010);
    }

    [Fact]
    public async void InGameCheck_ShouldSetInGameStatusValue_WhenReadMemoryReturnsTrue()
    {
        const byte value = 0x07;
        _snesConnector.Status.Returns(ConnectionStatus.Connected);
        _snesConnector.ReadMemoryAsync(0x7e0010).Returns(new[] {value});
        await _sut.InGameCheck();
            
        Assert.Equal(value, _memoryAddressProvider.MemoryAddresses[0x7e0010].Value);
    }

    [Fact]
    public async void MemoryCheck_ShouldDoNothing_WhenCanReadMemoryReturnsFalse()
    {
        await _sut.InGameCheck();
            
        await _snesConnector.DidNotReceive().ReadMemoryAsync(Arg.Any<ulong>(), Arg.Any<int>());
    }

    [Fact]
    public async void MemoryCheck_ShouldDoNothing_WhenIsInGameReturnsFalse()
    {
        _snesConnector.Status.Returns(ConnectionStatus.Connected);
        _snesConnector.ReadMemoryAsync(0x7e0010).ReturnsNull();
        await _sut.InGameCheck();
        await _sut.MemoryCheck();
            
        await _snesConnector.Received(1).ReadMemoryAsync(Arg.Any<ulong>(), Arg.Any<int>());
    }

    [Fact]
    public async void MemoryCheck_ShouldCallReadOnSNESConnector_WhenCanReadMemoryAndIsInGameReturnTrue()
    {
        _snesConnector.Status.Returns(ConnectionStatus.Connected);
        _snesConnector.ReadMemoryAsync(Arg.Any<ulong>(), Arg.Any<int>()).Returns(x =>
        {
            var bytesToRead = (int)x[1];
                
            var result = new byte[bytesToRead];

            for (var i = 0; i < bytesToRead; i++)
            {
                result[i] = 0x7;
            }

            return result;
        });
        await _sut.InGameCheck();
        await _sut.MemoryCheck();

        await _snesConnector.Received(Enum.GetValues(typeof(MemorySegmentType)).Length + 1).ReadMemoryAsync(
            Arg.Any<ulong>(), Arg.Any<int>());
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IAutoTracker>();
            
        Assert.NotNull(sut as AutoTracker);
    }

    [Fact]
    public void AutofacSingleInstanceTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var value1 = scope.Resolve<IAutoTracker>();
        var value2 = scope.Resolve<IAutoTracker>();
            
        Assert.Equal(value1, value2);
    }
}