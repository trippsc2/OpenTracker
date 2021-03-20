using Autofac;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo.Dropdowns;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Dropdowns
{
    public class CheckDropdownTests
    {
        private readonly IDropdown _dropdown = Substitute.For<IDropdown>();
        private readonly CheckDropdown _sut;

        public CheckDropdownTests()
        {
            _sut = new CheckDropdown(_dropdown);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public void CanExecute_ShouldReturnTrue_WhenCheckedEqualsFalse(bool expected, bool isChecked)
        {
            _dropdown.Checked.Returns(isChecked);
            
            Assert.Equal(expected, _sut.CanExecute());
        }

        [Fact]
        public void ExecuteDo_ShouldSetCheckedToTrue()
        {
            _dropdown.Checked.Returns(false);
            _sut.ExecuteDo();
            
            Assert.True(_dropdown.Checked);
        }

        [Fact]
        public void ExecuteUndo_ShouldSetCheckedToFalse()
        {
            _dropdown.Checked.Returns(false);
            _sut.ExecuteDo();
            _sut.ExecuteUndo();
            
            Assert.False(_dropdown.Checked);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<CheckDropdown.Factory>();
            var sut = factory(_dropdown);
            
            Assert.NotNull(sut);
        }
    }
}