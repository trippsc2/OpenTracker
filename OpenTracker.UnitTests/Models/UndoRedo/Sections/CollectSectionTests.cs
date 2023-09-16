using Autofac;
using NSubstitute;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo.Sections;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Sections;

public class CollectSectionTests
{
    private readonly ISection _section = Substitute.For<ISection>();

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CanExecute_ShouldCallCanBeCleared(bool force)
    {
        var sut = new CollectSection(_section, force);
        _ = sut.CanExecute();

        _section.Received().CanBeCleared(force);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void CanExecute_ShouldReturnTrue_WhenCanBeClearedReturnsTrue(bool expected, bool canBeCleared)
    {
        var sut = new CollectSection(_section, false);
        _section.CanBeCleared(Arg.Any<bool>()).Returns(canBeCleared);
            
        Assert.Equal(expected, sut.CanExecute());
    }

    [Theory]
    [InlineData(2, 3)]
    [InlineData(1, 2)]
    [InlineData(0, 1)]
    public void ExecuteDo_ShouldSubtractAvailableBy1(int expected, int starting)
    {
        var sut = new CollectSection(_section, false);
        _section.Available.Returns(starting);
        sut.ExecuteDo();
            
        Assert.Equal(expected, _section.Available);
    }

    [Fact]
    public void ExecuteDo_ShouldSetUserManipulatedToTrue()
    {
        var sut = new CollectSection(_section, false);
        sut.ExecuteDo();
            
        Assert.True(_section.UserManipulated);
    }

    [Fact]
    public void ExecuteUndo_ShouldRestoreMarkingToPreviousValue()
    {
        var marking = Substitute.For<IMarking>();
        _section.Marking.Returns(marking);
        var sut = new CollectSection(_section, false);
        marking.Mark = MarkType.HCLeft;
            
        sut.ExecuteDo();

        marking.Mark = MarkType.Unknown;
            
        sut.ExecuteUndo();
            
        Assert.Equal(MarkType.HCLeft, marking.Mark);
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(2, 2)]
    [InlineData(1, 1)]
    public void ExecuteUndo_ShouldRestorePreviousAvailableValue(int expected, int starting)
    {
        _section.Available.Returns(starting);
        var sut = new CollectSection(_section, false);
            
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _section.Available);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void ExecuteUndo_ShouldRestorePreviousUserManipulated(bool expected, bool starting)
    {
        _section.UserManipulated.Returns(starting);
        var sut = new CollectSection(_section, false);
            
        sut.ExecuteDo();
        sut.ExecuteUndo();
            
        Assert.Equal(expected, _section.UserManipulated);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<ICollectSection.Factory>();
        var sut = factory(_section, false);
            
        Assert.NotNull(sut as CollectSection);
    }
}