using Autofac;
using OpenTracker.Models.Modes;
using OpenTracker.Models.UndoRedo.Mode;
using Xunit;

namespace OpenTracker.UnitTests.Models.UndoRedo.Mode
{
    public class ChangeTakeAnyLocationsTests
    {
        private readonly IMode _mode = new OpenTracker.Models.Modes.Mode();

        [Fact]
        public void CanExecute_ShouldReturnTrueAlways()
        {
            var sut = new ChangeTakeAnyLocations(_mode, false);
            
            Assert.True(sut.CanExecute());
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteDo_ShouldSetTakeAnyLocationsToNewValue(bool expected, bool newValue)
        {
            var sut = new ChangeTakeAnyLocations(_mode, newValue);
            sut.ExecuteDo();
            
            Assert.Equal(expected, _mode.TakeAnyLocations);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void ExecuteUndo_ShouldSetTakeAnyLocationsToPreviousValue(bool expected, bool previousValue)
        {
            _mode.TakeAnyLocations = previousValue;
            var sut = new ChangeTakeAnyLocations(_mode, !previousValue);
            sut.ExecuteDo();
            sut.ExecuteUndo();
            
            Assert.Equal(expected, _mode.TakeAnyLocations);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IChangeTakeAnyLocations.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as ChangeTakeAnyLocations);
        }
    }
}