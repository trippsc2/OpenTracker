using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dropdowns
{
    public class DropdownDictionaryTests
    {
        private readonly IDropdownFactory _factory = Substitute.For<IDropdownFactory>();
        private readonly IDropdown _dropdown = Substitute.For<IDropdown>();
        private readonly DropdownSaveData _dropdownSaveData = new();
        
        private readonly DropdownDictionary _sut;

        public DropdownDictionaryTests()
        {
            _factory.GetDropdown(Arg.Any<DropdownID>()).Returns(_dropdown);
            _dropdown.Save().Returns(_dropdownSaveData);
            _sut = new DropdownDictionary(() => _factory);
            _ = _sut[DropdownID.LumberjackCave];
        }

        [Fact]
        public void Reset_ShouldCallResetOnMembers()
        {
            _sut.Reset();
            
            _dropdown.Received().Reset();
        }

        [Fact]
        public void Save_ShouldCallSaveOnMembers()
        {
            _ = _sut.Save();

            _dropdown.Received().Save();
        }

        [Fact]
        public void Save_ShouldReturnSaveDataContainingMemberSaveData()
        {
            var saveData = _sut.Save();

            Assert.Contains(_dropdownSaveData, saveData.Values);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            _sut.Load(null);
            
            _dropdown.DidNotReceive().Load(Arg.Any<DropdownSaveData?>());
        }

        [Fact]
        public void Load_ShouldCallLoadOnMembers()
        {
            var saveData = new Dictionary<DropdownID, DropdownSaveData>
            {
                {DropdownID.LumberjackCave, _dropdownSaveData}
            };
            _sut.Load(saveData);
            
            _dropdown.Received().Load(_dropdownSaveData);
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<IDropdownDictionary>();
            
            Assert.NotNull(sut as DropdownDictionary);
        }

        [Fact]
        public void AutofacSingleInstanceTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var value1 = scope.Resolve<IDropdownDictionary>();
            var value2 = scope.Resolve<IDropdownDictionary>();
            
            Assert.Equal(value1, value2);
        }
    }
}