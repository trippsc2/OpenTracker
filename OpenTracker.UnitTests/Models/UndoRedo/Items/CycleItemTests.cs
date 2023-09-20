using System.Diagnostics.CodeAnalysis;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo.Items;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Items;

[ExcludeFromCodeCoverage]
public sealed class CycleItemTests
{
    private readonly ICappedItem _item = Substitute.For<ICappedItem>();
    private readonly CycleItem _sut;

    public CycleItemTests()
    {
        _sut = new CycleItem(_item);
    }

    [Fact]
    public void CanExecute_ReturnsTrue()
    {
        Assert.True(_sut.CanExecute());
    }

    [Fact]
    public void ExecuteDo_ShouldCallCycle()
    {
        _sut.ExecuteDo();
            
        _item.Received().Cycle();
    }

    [Fact]
    public void ExecuteUndo_ShouldCallRemove()
    {
        _sut.ExecuteUndo();
            
        _item.Received().Cycle(true);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ICycleItem.Factory>();
        var sut = factory(_item);
            
        Assert.NotNull(sut as CycleItem);
    }
}