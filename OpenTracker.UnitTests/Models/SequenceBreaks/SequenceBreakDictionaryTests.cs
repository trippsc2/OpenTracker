using System.Collections.Generic;
using Autofac;
using NSubstitute;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.SequenceBreaks
{
    public class SequenceBreakDictionaryTests
    {
        // ReSharper disable once CollectionNeverUpdated.Local
        private readonly SequenceBreakDictionary _sut = new(_ => Substitute.For<ISequenceBreak>());
        
        [Fact]
        public void Indexer_ShouldReturnTheSameInstance()
        {
            var sequenceBreak1 = _sut[SequenceBreakType.Hover];
            var sequenceBreak2 = _sut[SequenceBreakType.Hover];
            
            Assert.Equal(sequenceBreak1, sequenceBreak2);
        }

        [Fact]
        public void Indexer_ShouldReturnTheDifferentInstances()
        {
            var sequenceBreak1 = _sut[SequenceBreakType.Hover];
            var sequenceBreak2 = _sut[SequenceBreakType.ArrghusBasic];
            
            Assert.NotEqual(sequenceBreak1, sequenceBreak2);
        }

        [Fact]
        public void Save_ShouldCallSaveOnSequenceBreaks()
        {
            var sequenceBreak = _sut[SequenceBreakType.Hover];
            _ = _sut.Save();
            
            sequenceBreak.Received().Save();
        }

        [Fact]
        public void Save_ShouldReturnDictionaryOfSaveData()
        {
            const SequenceBreakType type = SequenceBreakType.Hover;
            var bossPlacement = _sut[type];
            var bossPlacementSaveData = new SequenceBreakSaveData();
            bossPlacement.Save().Returns(bossPlacementSaveData);
            var saveData = _sut.Save();

            Assert.Equal(bossPlacementSaveData, saveData[type]);
        }

        [Fact]
        public void Load_ShouldDoNothing_WhenSaveDataIsNull()
        {
            const SequenceBreakType type = SequenceBreakType.Hover;
            var sequenceBreak = _sut[type];
            _sut.Load(null);
            
            sequenceBreak.DidNotReceive().Load(Arg.Any<SequenceBreakSaveData>());
        }

        [Fact]
        public void Load_ShouldCallLoadOnBossPlacements()
        {
            const SequenceBreakType type = SequenceBreakType.Hover;
            var bossPlacement = _sut[type];
            var saveData = new Dictionary<SequenceBreakType, SequenceBreakSaveData>
            {
                { type, new SequenceBreakSaveData() }
            };
            _sut.Load(saveData);
            
            bossPlacement.Received().Load(Arg.Any<SequenceBreakSaveData>());
        }

        [Fact]
        public void AutofacTest()
        {
            using var scope = ContainerConfig.Configure().BeginLifetimeScope();
            var sut = scope.Resolve<ISequenceBreakDictionary>();
            
            Assert.NotNull(sut as SequenceBreakDictionary);
        }
    }
}