using System.ComponentModel;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements.GenericKeys;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.GenericKeys
{
    public class GenericKeysRequirementTests
    {
        private readonly IMode _mode = Substitute.For<IMode>();
        
        [Fact]
        public void ModeChanged_ShouldUpdateMetValue()
        {
            var sut = new GenericKeysRequirement(_mode, true);
            _mode.GenericKeys.Returns(true);

            _mode.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(
                _mode, new PropertyChangedEventArgs(nameof(IMode.GenericKeys)));
            
            Assert.True(sut.Met);
        }

        [Theory]
        [InlineData(true, false, false)]
        [InlineData(false, false, true)]
        [InlineData(true, true, true)]
        public void Met_ShouldReturnExpectedValue(bool expected, bool genericKeys, bool requirement)
        {
            _mode.GenericKeys.Returns(genericKeys);
            var sut = new GenericKeysRequirement(_mode, requirement);
            
            Assert.Equal(expected, sut.Met);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IGenericKeysRequirement.Factory>();
            var sut = factory(false);
            
            Assert.NotNull(sut as GenericKeysRequirement);
        }
    }
}