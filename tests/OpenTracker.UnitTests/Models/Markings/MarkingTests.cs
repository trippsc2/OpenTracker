using Autofac;
using NSubstitute;
using OpenTracker.Models.Markings;
using OpenTracker.Models.UndoRedo.Markings;
using Xunit;

namespace OpenTracker.UnitTests.Models.Markings
{
    public class MarkingTests
    {
        private readonly IChangeMarking.Factory _changeMarkingFactory = (_, _) => Substitute.For<IChangeMarking>();
        
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

        [Fact]
        public void CreateChangeMarkingAction_ShouldReturnNewAction()
        {
            Assert.NotNull(_sut.CreateChangeMarkingAction(MarkType.Aga));
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IMarking.Factory>();
            var sut = factory();
            
            Assert.NotNull(sut as Marking);
        }
    }
}