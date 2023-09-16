using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Dropdowns;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dropdowns;

public class DropdownTests
{
    private readonly IRequirement _requirement = Substitute.For<IRequirement>();

    private readonly ICheckDropdown.Factory _checkDropdownFactory = _ => Substitute.For<ICheckDropdown>();
    private readonly IUncheckDropdown.Factory _uncheckDropdownFactory = _ => Substitute.For<IUncheckDropdown>();
        
    private readonly Dropdown _sut;

    public DropdownTests()
    {
        _sut = new Dropdown(_checkDropdownFactory, _uncheckDropdownFactory, _requirement);
    }
        
    [Fact]
    public void Checked_ShouldRaisePropertyChanged()
    {
        Assert.PropertyChanged(_sut, nameof(IDropdown.Checked), () => _sut.Checked = true);
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, true)]
    public void RequirementMet_ShouldReturnTrue_WhenRequirementIsMet(bool expected, bool met)
    {
        _requirement.Met.Returns(met);
            
        Assert.Equal(expected, _sut.RequirementMet);
    }

    [Fact]
    public void RequirementMet_ShouldRaisePropertyChanged()
    {
        Assert.PropertyChanged(_sut, nameof(IDropdown.RequirementMet),
            () => _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
    }
        
    [Fact]
    public void CreateCheckDropdownAction_ShouldReturnNewAction()
    {
        var checkDropdown = _sut.CreateCheckDropdownAction();
            
        Assert.NotNull(checkDropdown);
    }

    [Fact]
    public void CreateUncheckDropdownAction_ShouldReturnNewAction()
    {
        var uncheckDropdown = _sut.CreateUncheckDropdownAction();
            
        Assert.NotNull(uncheckDropdown);
    }
        
    [Fact]
    public void Reset_ShouldChangeCheckedToFalse()
    {
        _sut.Checked = true;
            
        _sut.Reset();
            
        Assert.False(_sut.Checked);
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        _sut.Checked = true;

        _sut.Load(null);

        Assert.True(_sut.Checked);
    }

    [Fact]
    public void Load_ShouldSetCheckedToSaveDataValue_WhenSaveDataIsNotNull()
    {
        var saveData = new DropdownSaveData()
        {
            Checked = true
        };
            
        _sut.Load(saveData);
            
        Assert.True(_sut.Checked);
    }

    [Fact]
    public void Save_ShouldSetSaveDataCheckedToTrue_WhenCheckedIsTrue()
    {
        _sut.Checked = true;
            
        var saveData = _sut.Save();
            
        Assert.True(saveData.Checked);
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDropdown.Factory>();
        var sut = factory(_requirement);
            
        Assert.NotNull((sut as Dropdown));
    }
}