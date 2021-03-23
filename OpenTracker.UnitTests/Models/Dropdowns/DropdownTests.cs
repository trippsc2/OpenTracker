using System.ComponentModel;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo.Dropdowns;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dropdowns
{
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

        [Fact]
        public void RequirementMet_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IDropdown.RequirementMet),
                () => _requirement.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                    _requirement, new PropertyChangedEventArgs(nameof(IRequirement.Met))));
        }
        
        [Fact]
        public void Reset_ShouldChangeCheckedToFalse()
        {
            _sut.Checked = true;
            
            _sut.Reset();
            
            Assert.False(_sut.Checked);
        }

        [Fact]
        public void Load_LoadingNullDataShouldDoNothing()
        {
            _sut.Checked = true;

            _sut.Load(null);

            Assert.True(_sut.Checked);
        }

        [Fact]
        public void Load_SaveDataCheckedShouldBeEqualToCheckedProperty()
        {
            var saveData = new DropdownSaveData()
            {
                Checked = true
            };
            
            _sut.Load(saveData);
            
            Assert.True(_sut.Checked);
        }

        [Fact]
        public void Save_SaveDataCheckedShouldMatch()
        {
            _sut.Checked = true;
            
            var saveData = _sut.Save();
            
            Assert.True(saveData.Checked);
        }
    }
}