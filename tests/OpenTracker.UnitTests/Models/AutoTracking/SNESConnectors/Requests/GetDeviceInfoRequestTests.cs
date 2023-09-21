using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Autofac;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Requests;

[ExcludeFromCodeCoverage]
public sealed class GetDeviceInfoRequestTests
{
    private readonly IAutoTrackerLogger _logger = Substitute.For<IAutoTrackerLogger>();

    private readonly GetDeviceInfoRequest _sut;

    public GetDeviceInfoRequestTests()
    {
        _sut = new GetDeviceInfoRequest(_logger);
    }

    [Fact]
    public void Description_ShouldReturnExpected()
    {
        const string expected = "Get device info";

        _sut.Description.Should().Be(expected);
    }

    [Fact]
    public void StatusRequired_ShouldReturnExpected()
    {
        const ConnectionStatus expected = ConnectionStatus.Connected;

        _sut.StatusRequired.Should().Be(expected);
    }
        
    [Fact]
    public void ToJsonString_ShouldReturnExpected()
    {
        const string expected = "{\"Opcode\":\"Info\",\"Space\":\"SNES\"}";
            
        _sut.ToJsonString().Should().Be(expected);
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldReturnExpected()
    {
        string[] expected = {"Test"};
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.Data.Returns($"{{\"Results\":[\"{expected[0]}\"]}}");

        _sut.ProcessResponseAndReturnResults(
                messageEventArgs,
                new ManualResetEvent(false))
            .Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenDataIsNotJSON()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.Data.Returns("Test");
        
        _sut.Invoking(x =>
                x.ProcessResponseAndReturnResults(
                    messageEventArgs,
                    new ManualResetEvent(false)))
            .Should().Throw<JsonReaderException>();
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenDataDoesNotContainResults()
    {
        string[] expected = {"Test"};
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.Data.Returns($"{{\"Test\":[\"{expected[0]}\"]}}");
        
        _sut.Invoking(x =>
                x.ProcessResponseAndReturnResults(
                    messageEventArgs,
                    new ManualResetEvent(false)))
            .Should().Throw<Exception>();
    }
        
    [Fact]
    public void AutofacResolve_ShouldResolveAsInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IGetDeviceInfoRequest.Factory>();
        var sut1 = factory();

        sut1.Should().BeOfType<GetDeviceInfoRequest>();
        
        var sut2 = factory();
        
        sut1.Should().NotBeSameAs(sut2);
    }
}