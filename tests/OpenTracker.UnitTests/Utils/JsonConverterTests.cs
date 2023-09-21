using System.Diagnostics.CodeAnalysis;
using Autofac;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Utils;

[ExcludeFromCodeCoverage]
public sealed class JsonConverterTests
{
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IJsonConverter>();
            
        Assert.NotNull(sut as JsonConverter);
    }
        
    [Fact]
    public void AutofacSingleInstanceTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var value1 = scope.Resolve<IJsonConverter>();
        var value2 = scope.Resolve<IJsonConverter>();
            
        Assert.Equal(value1, value2);
    }
}