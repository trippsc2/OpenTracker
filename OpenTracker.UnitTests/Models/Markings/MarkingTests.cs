using OpenTracker.Models.Markings;
using Xunit;

namespace OpenTracker.UnitTests.Models.Markings
{
    public class MarkingTests
    {
        private readonly Marking _sut;

        public MarkingTests()
        {
            _sut = new Marking() { Mark = MarkType.Unknown };
        }

        [Fact]
        public void Mark_ShouldRaisePropertyChanged()
        {
            Assert.PropertyChanged(_sut, nameof(IMarking.Mark), () => _sut.Mark = MarkType.HCLeft);
        }
    }
}