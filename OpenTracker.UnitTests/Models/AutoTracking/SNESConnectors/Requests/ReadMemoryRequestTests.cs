using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Autofac;
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
        string expected = $"Read {BytesToRead} byte(s) at {Address:X}";

        Assert.Equal(expected, _sut.Description);
    }

    [Fact]
    public void StatusRequired_ShouldReturnExpected()
    {
        const ConnectionStatus expected = ConnectionStatus.Connected;
            
        Assert.Equal(expected, _sut.StatusRequired);
    }
        
    [Fact]
    public void ToJsonString_ShouldReturnExpected()
    {
        var addressOperand = AddressTranslator.TranslateAddress((uint) Address).ToString(
            "X", CultureInfo.InvariantCulture);
        var bytesToReadOperand = BytesToRead.ToString("X", CultureInfo.InvariantCulture);
        var expected = $"{{\"Opcode\":\"GetAddress\",\"Space\":\"SNES\"," +
                       $"\"Operands\":[\"{addressOperand}\",\"{bytesToReadOperand}\"]}}";
            
        Assert.Equal(expected, _sut.ToJsonString());
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldReturnExpected()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.IsBinary.Returns(true);
        var expected = new byte[] {0, 1};
        messageEventArgs.RawData.Returns(expected);
            
        Assert.Equal(expected, _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenIsNotBinary()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
            
        Assert.Throws<Exception>(() => _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }

    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenRawDataIsNull()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.IsBinary.Returns(true);
            
        Assert.Throws<Exception>(() => _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }
        
    [Fact]
    public void ProcessResponseAndReturnResults_ShouldThrowException_WhenRawDataLengthIsInvalid()
    {
        var messageEventArgs = Substitute.For<IMessageEventArgsWrapper>();
        messageEventArgs.IsBinary.Returns(true);
        var expected = new byte[] {0};
        messageEventArgs.RawData.Returns(expected);
            
        Assert.Throws<Exception>(() => _sut.ProcessResponseAndReturnResults(
            messageEventArgs, new ManualResetEvent(false)));
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IReadMemoryRequest.Factory>();
        var sut = factory(Address, BytesToRead);
            
        Assert.NotNull(sut as ReadMemoryRequest);
    }
}