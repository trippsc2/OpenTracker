using System.Collections.Generic;
using Autofac;
using OpenTracker.Models.AutoTracking.SNESConnectors;
using Xunit;

namespace OpenTracker.UnitTests.Models.AutoTracking.SNESConnectors
{
    public class RequestTests
    {
        [Theory]
        [InlineData("DeviceList","DeviceList")]
        [InlineData("Attach","Attach")]
        [InlineData("Name","Name")]
        [InlineData("Info","Info")]
        [InlineData("GetAddress","GetAddress")]
        public void Ctor_ShouldSetOpcodeToExpected(string expected, string opcode)
        {
            var sut = new Request(opcode);
            
            Assert.Equal(expected, sut.Opcode);
        }

        [Fact]
        public void Ctor_ShouldSetSpaceToExpected()
        {
            const string testSpace = "TestSpace";
            var sut = new Request("Test", testSpace);
            
            Assert.Equal(testSpace, sut.Space);
        }

        [Fact]
        public void Ctor_ShouldSetFlagsToEmptyList_WhenParameterIsNull()
        {
            var sut = new Request("Test", flags: null);

            Assert.Empty(sut.Flags);
        }

        [Fact]
        public void Ctor_ShouldSetOperandsToEmptyList_WhenParameterIsNull()
        {
            var sut = new Request("Test");
            
            Assert.Empty(sut.Operands);
        }

        [Fact]
        public void Ctor_ShouldSetOperandsToExpected()
        {
            var sut = new Request(
                OpcodeType.GetAddress.ToString(),
                operands: new List<string>(2)
                {
                    "0xF5F010",
                    "0x1"
                });
            
            Assert.Equal(2, sut.Operands.Count);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var factory = scope.Resolve<IRequest.Factory>();
            var sut = factory("Test");
            
            Assert.NotNull(sut as Request);
        }
    }
}