using NSubstitute;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo.Markings;
using Xunit;

namespace OpenTracker.UnitTests.Models.Markings
{
    public class MarkingTests
    {
        private readonly IChangeMarking.Factory _changeMarkingFactory =
            (marking, newMarking) => Substitute.For<IChangeMarking>();
        
        private readonly Marking _sut;

        public MarkingTests()
        {
            _sut = new Marking(_changeMarkingFactory) { Mark = MarkType.Unknown };
        }

        [Fact]
        public void Mark_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMarking.Mark), () => _sut.Mark = MarkType.HCLeft);
        }

        [Fact]
        public void Mark_ShouldReturnSetValue()
        {
            _sut.Mark = MarkType.HCLeft;
            
            Assert.Equal(MarkType.HCLeft, _sut.Mark);
        }
    }
}