using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using OpenTracker.Models.AutoTracking.SNESConnectors.Requests;
using OpenTracker.Models.AutoTracking.SNESConnectors.Socket;
using OpenTracker.Models.Logging;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors.Requests;

[ExcludeFromCodeCoverage]
public sealed class ReadMemoryRequestTests
{
    private const ulong Address = 0x7ef010;
    private const int BytesToRead = 2;
        
    private readonly IAutoTrackerLogger _logger = Substitute.For<IAutoTrackerLogger>();

    private readonly ReadMemoryRequest _sut;

    public ReadMemoryRequestTests()
    {
        _sut = new ReadMemoryRequest(_logger, Address, BytesToRead);
    }

    [Fact]
    public void Description_ShouldReturnExpected()
    {
        var expected = $"Read {BytesToRead} byte(s) at {Address:X}";

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
        var addressOperand = AddressTranslator.TranslateAddress((uint) Address).ToString(
            "X", CultureInfo.InvariantCulture);
        var bytesToReadOperand = BytesToRead.ToString("X", CultureInfo.InvariantCulture);
        var expected = $"{{\"Opcode\":\"GetAddress\",\"Space\":\"SNES\"," +
                       $"\"Operands\":[\"{addressOperand}\",\"{bytesToReadOperand}\"]}}";
            
        _sut.ToJsonString().Should().Be(expected);
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldReturnExpected()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.IsBinary.Returns(true);
        var expected = new byte[] {0, 1};
        messageEventArgs.RawData.Returns(expected);
        
        _sut.ProcessResponseAndReturnResults(
                messageEventArgs,
                new ManualResetEvent(false))
            .Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenIsNotBinary()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        
        _sut.Invoking(x =>
                x.ProcessResponseAndReturnResults(
                    messageEventArgs,
                    new ManualResetEvent(false)))
            .Should().Throw<Exception>();
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenRawDataIsNull()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.IsBinary.Returns(true);
        
        _sut.Invoking(x =>
                x.ProcessResponseAndReturnResults(
                    messageEventArgs,
                    new ManualResetEvent(false)))
            .Should().Throw<Exception>();
    }
        
    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenRawDataLengthIsInvalid()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.IsBinary.Returns(true);
        var expected = new byte[] {0};
        messageEventArgs.RawData.Returns(expected);
        
        _sut.Invoking(x =>
                x.ProcessResponseAndReturnResults(
                    messageEventArgs,
                    new ManualResetEvent(false)))
            .Should().Throw<Exception>();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IReadMemoryRequest.Factory>();
        var sut1 = factory(Address, BytesToRead);
            
        sut1.Should().BeOfType<ReadMemoryRequest>();
        
        var sut2 = factory(Address, BytesToRead);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}