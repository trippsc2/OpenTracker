using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using OpenTracker.Models.AutoTracking.Memory;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.Memory;

[ExcludeFromCodeCoverage]
public sealed class MemoryAddressProviderTests
{
    private readonly MemoryAddressProvider _sut = new();

    [Fact]
    public void Reset_ShouldCallResetOnMemoryAddresses()
    {
        var memoryAddress = _sut.MemoryAddresses[0x7ef000];
        memoryAddress.Value = 1;
            
        _sut.Reset();

        memoryAddress.Value.Should().BeNull();
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToSingleInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut1 = scope.Resolve<IMemoryAddressProvider>();

        sut1.Should().BeOfType<MemoryAddressProvider>();
        
        var sut2 = scope.Resolve<IMemoryAddressProvider>();
        
        sut1.Should().BeSameAs(sut2);
    }
}