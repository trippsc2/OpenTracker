using Autofac;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo.Dropdowns;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Dropdowns;

public class UncheckDropdownTests
{
    private readonly IDropdown _dropdown = Substitute.For<IDropdown>();
    private readonly UncheckDropdown _sut;

    public UncheckDropdownTests()
    {
        _sut = new UncheckDropdown(_dropdown);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void CanExecute_ShouldReturnTrue_WhenCheckedEqualsTrue(bool expected, bool isChecked)
    {
        _dropdown.Checked.Returns(isChecked);
            
        Assert.Equal(expected, _sut.CanExecute());
    }

    [Fact]
    public void ExecuteDo_ShouldSetCheckedToFalse()
    {
        _dropdown.Checked.Returns(true);
        _sut.ExecuteDo();
            
        Assert.False(_dropdown.Checked);
    }

    [Fact]
    public void ExecuteUndo_ShouldSetCheckedToTrue()
    {
        _dropdown.Checked.Returns(true);
        _sut.ExecuteDo();
        _sut.ExecuteUndo();
            
        Assert.True(_dropdown.Checked);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IUncheckDropdown.Factory>();
        var sut = factory(_dropdown);
            
        Assert.NotNull(sut as UncheckDropdown);
    }
        
}