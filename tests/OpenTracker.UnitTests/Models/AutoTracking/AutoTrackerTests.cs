using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using OpenTracker.Models.AutoTracking;
using OpenTracker.Models.AutoTracking.Memory;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking;

[ExcludeFromCodeCoverage]
public sealed class AutoTrackerTests
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

        _sut.Status.Should().Be(expected);
    }
        
    [Fact]
    public async void Devices_ShouldReturnEmptyList_WhenNullReceivedFromConnector()
    {
        _snesConnector.GetDevicesAsync().ReturnsNull();

        await _sut.GetDevices();

        _sut.Devices.Should().BeEmpty();
    }

    [Fact]
    public async void Devices_ShouldReturnReceivedList()
    {
        var expected = new[]
        {
            "Test 1",
            "Test 2",
            "Test 3"
        };
        
        _snesConnector.GetDevicesAsync().Returns(expected);

        await _sut.GetDevices();
        
        _sut.Devices.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async void Devices_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        using var monitor = _sut.Monitor();
        
        _snesConnector.GetDevicesAsync().Returns(new[]
        {
            "Test 1",
            "Test 2",
            "Test 3"
        });
        
        await _sut.GetDevices();
        
        monitor.Should().RaisePropertyChangeFor(x => x.Devices);
    }

    [Fact]
    public void RaceIllegalTracking_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        using var monitor = _sut.Monitor();
        
        _sut.RaceIllegalTracking = true;

        monitor.Should().RaisePropertyChangeFor(x => x.RaceIllegalTracking);
    }

    [Fact]
    public void Status_ShouldRaisePropertyChanged_WhenValueChanges()
    {
        using var monitor = _sut.Monitor();
        
        _snesConnector.Status.Returns(ConnectionStatus.Connecting);
        _snesConnector.PropertyChanged += Raise
            .Event<PropertyChangedEventHandler>(
                _snesConnector,
                new PropertyChangedEventArgs(nameof(ISNESConnector.Status)));
        
        monitor.Should().RaisePropertyChangeFor(x => x.Status);
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

        _sut.CanConnect().Should().Be(expected);
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

        _sut.CanGetDevices().Should().Be(expected);
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

        _sut.CanDisconnect().Should().Be(expected);
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

        _sut.CanStart().Should().Be(expected);
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
            
        _memoryAddressProvider.MemoryAddresses[0x7e0010].Value.Should().Be(value);
    }

    [Fact]
    public async void MemoryCheck_ShouldDoNothing_WhenCanReadMemoryReturnsFalse()
    {
        await _sut.InGameCheck();
            
        await _snesConnector
            .DidNotReceive()
            .ReadMemoryAsync(Arg.Any<ulong>(), Arg.Any<int>());
    }

    [Fact]
    public async void MemoryCheck_ShouldDoNothing_WhenIsInGameReturnsFalse()
    {
        _snesConnector.Status.Returns(ConnectionStatus.Connected);
        _snesConnector.ReadMemoryAsync(0x7e0010).ReturnsNull();
        await _sut.InGameCheck();
        await _sut.MemoryCheck();
            
        await _snesConnector
            .Received(1)
            .ReadMemoryAsync(Arg.Any<ulong>(), Arg.Any<int>());
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

        await _snesConnector
            .Received(Enum.GetValues<MemorySegmentType>().Length + 1)
            .ReadMemoryAsync(
                Arg.Any<ulong>(),
                Arg.Any<int>());
    }

    [Fact]
    public void AutofacResolve_ShouldResolveAsInterfaceToSingleInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<IAutoTracker>();

        sut1.Should().BeOfType<AutoTracker>();
        
        var sut2 = scope.Resolve<IAutoTracker>();

        sut1.Should().BeSameAs(sut2);
    }
}