using System.Diagnostics.CodeAnalysis;
using Autofac;
using FluentAssertions;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Dropdowns;
using OpenTracker.UnitTests.Models.Requirements;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dropdowns;

[ExcludeFromCodeCoverage]
public sealed class DropdownTests
{
    private readonly MockRequirement _requirement = new();

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
        using var monitor = _sut.Monitor();
        
        _sut.Checked = true;
        
        monitor.Should().RaisePropertyChangeFor(x => x.Checked);
    }

    [Fact]
    public void Requirement_ShouldReturnExpected()
    {
        _sut.Requirement.Should().Be(_requirement);
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

        _sut.Checked.Should().BeFalse();
    }

    [Fact]
    public void Load_ShouldDoNothing_WhenSaveDataIsNull()
    {
        _sut.Checked = true;

        _sut.Load(null);

        _sut.Checked.Should().BeTrue();
    }

    [Fact]
    public void Load_ShouldSetCheckedToSaveDataValue_WhenSaveDataIsNotNull()
    {
        var saveData = new DropdownSaveData()
        {
            Checked = true
        };
            
        _sut.Load(saveData);

        _sut.Checked.Should().BeTrue();
    }

    [Fact]
    public void Save_ShouldSetSaveDataCheckedToTrue_WhenCheckedIsTrue()
    {
        _sut.Checked = true;
            
        var saveData = _sut.Save();

        saveData.Checked.Should().BeTrue();
    }

    [Fact]
    public void AutofacResolve_ShouldResolveInterfaceToTransientInstance()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IDropdown.Factory>();
        var sut1 = factory(_requirement);
            
        sut1.Should().BeOfType<Dropdown>();
        
        var sut2 = factory(_requirement);
        
        sut1.Should().NotBeSameAs(sut2);
    }
}