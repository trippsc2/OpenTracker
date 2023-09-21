using System.Diagnostics.CodeAnalysis;
using Autofac;
using OpenTracker.Utils;
using Xunit;

namespace OpenTracker.UnitTests.Utils;

[ExcludeFromCodeCoverage]
public sealed class FileManagerTests
{
    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var sut = scope.Resolve<IFileManager>();
            
        Assert.NotNull(sut as FileManager);
    }

    [Fact]
    public void AutofacSingleInstanceTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var value1 = scope.Resolve<IFileManager>();
        var value2 = scope.Resolve<IFileManager>();
            
        Assert.Equal(value1, value2);
    }
}