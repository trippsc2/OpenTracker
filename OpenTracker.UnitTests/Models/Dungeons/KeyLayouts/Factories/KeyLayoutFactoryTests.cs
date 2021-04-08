using System;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories
{
    public class KeyLayoutFactoryTests
    {
        private readonly IHCKeyLayoutFactory _hcFactory = Substitute.For<IHCKeyLayoutFactory>();
        private readonly IATKeyLayoutFactory _atFactory = Substitute.For<IATKeyLayoutFactory>();
        private readonly IEPKeyLayoutFactory _epFactory = Substitute.For<IEPKeyLayoutFactory>();
        private readonly IDPKeyLayoutFactory _dpFactory = Substitute.For<IDPKeyLayoutFactory>();
        private readonly IToHKeyLayoutFactory _tohFactory = Substitute.For<IToHKeyLayoutFactory>();
        private readonly IPoDKeyLayoutFactory _podFactory = Substitute.For<IPoDKeyLayoutFactory>();
        private readonly ISPKeyLayoutFactory _spFactory = Substitute.For<ISPKeyLayoutFactory>();
        private readonly ISWKeyLayoutFactory _swFactory = Substitute.For<ISWKeyLayoutFactory>();
        private readonly ITTKeyLayoutFactory _ttFactory = Substitute.For<ITTKeyLayoutFactory>();
        private readonly IIPKeyLayoutFactory _ipFactory = Substitute.For<IIPKeyLayoutFactory>();
        private readonly IMMKeyLayoutFactory _mmFactory = Substitute.For<IMMKeyLayoutFactory>();
        private readonly ITRKeyLayoutFactory _trFactory = Substitute.For<ITRKeyLayoutFactory>();
        private readonly IGTKeyLayoutFactory _gtFactory = Substitute.For<IGTKeyLayoutFactory>();

        private readonly KeyLayoutFactory _sut;

        public KeyLayoutFactoryTests()
        {
            _sut = new KeyLayoutFactory(
                _hcFactory, _atFactory, _epFactory, _dpFactory, _tohFactory, _podFactory, _spFactory, _swFactory,
                _ttFactory, _ipFactory, _mmFactory, _trFactory, _gtFactory);
        }

        [Fact]
        public void GetDungeonKeyLayouts_ShouldThrowException_WhenDungeonIDIsUnexpected()
        {
            var dungeon = Substitute.For<IDungeon>();
            dungeon.ID.Returns((DungeonID)int.MaxValue);

            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetDungeonKeyLayouts(dungeon));

        }

        [Theory]
        [InlineData(DungeonID.HyruleCastle)]
        [InlineData(DungeonID.AgahnimTower)]
        [InlineData(DungeonID.EasternPalace)]
        [InlineData(DungeonID.DesertPalace)]
        [InlineData(DungeonID.TowerOfHera)]
        [InlineData(DungeonID.PalaceOfDarkness)]
        [InlineData(DungeonID.SwampPalace)]
        [InlineData(DungeonID.SkullWoods)]
        [InlineData(DungeonID.ThievesTown)]
        [InlineData(DungeonID.IcePalace)]
        [InlineData(DungeonID.MiseryMire)]
        [InlineData(DungeonID.TurtleRock)]
        [InlineData(DungeonID.GanonsTower)]
        public void GetDungeonKeyLayouts_ShouldCallExpectedFactory(DungeonID id)
        {
            var dungeon = Substitute.For<IDungeon>();
            dungeon.ID.Returns(id);
            
            IKeyLayoutFactory factory = id switch
            {
                DungeonID.HyruleCastle => _hcFactory,
                DungeonID.AgahnimTower => _atFactory,
                DungeonID.EasternPalace => _epFactory,
                DungeonID.DesertPalace => _dpFactory,
                DungeonID.TowerOfHera => _tohFactory,
                DungeonID.PalaceOfDarkness => _podFactory,
                DungeonID.SwampPalace => _spFactory,
                DungeonID.SkullWoods => _swFactory,
                DungeonID.ThievesTown => _ttFactory,
                DungeonID.IcePalace => _ipFactory,
                DungeonID.MiseryMire => _mmFactory,
                DungeonID.TurtleRock => _trFactory,
                DungeonID.GanonsTower => _gtFactory,
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };

            _sut.GetDungeonKeyLayouts(dungeon);

            factory.Received().GetDungeonKeyLayouts(dungeon);
        }
    }
}