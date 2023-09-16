using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory;

[ExcludeFromCodeCoverage]
public sealed class MemoryFlagTests
{
    private readonly MemoryAddress _memoryAddress = new();
        
    [Theory]
    [InlineData(null, null, 0x1)]
    [InlineData(false, (byte)0, 0x1)]
    [InlineData(true, (byte)1, 0x1)]
    [InlineData(false, (byte)254, 0x1)]
    [InlineData(true, (byte)255, 0x1)]
    [InlineData(null, null, 0x2)]
    [InlineData(false, (byte)0, 0x2)]
    [InlineData(false, (byte)1, 0x2)]
    [InlineData(true, (byte)2, 0x2)]
    [InlineData(false, (byte)253, 0x2)]
    [InlineData(true, (byte)254, 0x2)]
    [InlineData(true, (byte)255, 0x2)]
    [InlineData(null, null, 0x4)]
    [InlineData(false, (byte)0, 0x4)]
    [InlineData(false, (byte)1, 0x4)]
    [InlineData(false, (byte)2, 0x4)]
    [InlineData(false, (byte)3, 0x4)]
    [InlineData(true, (byte)4, 0x4)]
    [InlineData(false, (byte)251, 0x4)]
    [InlineData(true, (byte)252, 0x4)]
    [InlineData(true, (byte)253, 0x4)]
    [InlineData(true, (byte)254, 0x4)]
    [InlineData(true, (byte)255, 0x4)]
    public void Status_ShouldReturnExpected(bool? expected, byte? value, byte flag)
    {
        _memoryAddress.Value = value;
        var sut = new MemoryFlag(_memoryAddress, flag);

        sut.Status.Should().Be(expected);
    }

    [Fact]
    public void Status_ShouldRaisePropertyChanged()
    {
        _memoryAddress.Value = null;
            
        var sut = new MemoryFlag(_memoryAddress, 0x1);

        using var monitor = sut.Monitor();
            
        _memoryAddress.Value = 0;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Status);
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IMemoryFlag.Factory>();
        
        var sut1 = factory(_memoryAddress, 0x1);

        sut1.Should().BeOfType<MemoryFlag>();
        
        var sut2 = factory(_memoryAddress, 0x1);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}