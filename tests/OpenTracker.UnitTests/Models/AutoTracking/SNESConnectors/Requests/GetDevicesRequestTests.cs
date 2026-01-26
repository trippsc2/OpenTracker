using System;
using System.Threading;
using Autofac;
using Newtonsoft.Json;
using NSubstitute;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Requests;

public class GetDevicesRequestTests
{
    private readonly IAutoTrackerLogger _logger = Substitute.For<IAutoTrackerLogger>();

    private readonly GetDevicesRequest _sut;

    public GetDevicesRequestTests()
    {
        _sut = new GetDevicesRequest(_logger);
    }

    [Fact]
    public void Description_ShouldReturnExpected()
    {
        const string expected = "Get device list";
            
        Assert.Equal(expected, _sut.Description);
    }

    [Fact]
    public void StatusRequired_ShouldReturnExpected()
    {
        const ConnectionStatus expected = ConnectionStatus.SelectDevice;
            
        Assert.Equal(expected, _sut.StatusRequired);
    }
        
    [Fact]
    public void ToJsonString_ShouldReturnExpected()
    {
        const string expected = "{\"Opcode\":\"DeviceList\",\"Space\":\"SNES\"}";
            
        Assert.Equal(expected, _sut.ToJsonString());
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldReturnExpected()
    {
        string[] expected = {"Test"};
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.Data.Returns($"{{\"Results\":[\"{expected[0]}\"]}}");
            
        Assert.Equal(expected, _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenDataIsNotJSON()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.Data.Returns("Test");
            
        Assert.Throws<JsonReaderException>(() => _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenDataDoesNotContainResults()
    {
        string[] expected = {"Test"};
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.Data.Returns($"{{\"Test\":[\"{expected[0]}\"]}}");
            
        Assert.Throws<Exception>(() => _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }
        
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IGetDevicesRequest.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as GetDevicesRequest);
    }
}