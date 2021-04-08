using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons.KeyLayouts.Factories
{
    /// <summary>
    ///     This class contains creation logic for key layout data.
    /// </summary>
    public class KeyLayoutFactory : IKeyLayoutFactory
    {
        private readonly IHCKeyLayoutFactory _hcFactory;
        private readonly IATKeyLayoutFactory _atFactory;
        private readonly IEPKeyLayoutFactory _epFactory;
        private readonly IDPKeyLayoutFactory _dpFactory;
        private readonly IToHKeyLayoutFactory _tohFactory;
        private readonly IPoDKeyLayoutFactory _podFactory;
        private readonly ISPKeyLayoutFactory _spFactory;
        private readonly ISWKeyLayoutFactory _swFactory;
        private readonly ITTKeyLayoutFactory _ttFactory;
        private readonly IIPKeyLayoutFactory _ipFactory;
        private readonly IMMKeyLayoutFactory _mmFactory;
        private readonly ITRKeyLayoutFactory _trFactory;
        private readonly IGTKeyLayoutFactory _gtFactory;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="hcFactory">
        ///     A factory for creating Hyrule Castle key layouts.
        /// </param>
        /// <param name="atFactory">
        ///     A factory for creating Agahnim's Tower key layouts.
        /// </param>
        /// <param name="epFactory">
        ///     A factory for creating Eastern Palace key layouts.
        /// </param>
        /// <param name="dpFactory">
        ///     A factory for creating Desert Palace key layouts.
        /// </param>
        /// <param name="tohFactory">
        ///     A factory for creating Tower of Hera key layouts.
        /// </param>
        /// <param name="podFactory">
        ///     A factory for creating Palace of Darkness key layouts.
        /// </param>
        /// <param name="spFactory">
        ///     A factory for creating Swamp Palace key layouts.
        /// </param>
        /// <param name="swFactory">
        ///     A factory for creating Skull Woods key layouts.
        /// </param>
        /// <param name="ttFactory">
        ///     A factory for creating Thieves' Town key layouts.
        /// </param>
        /// <param name="ipFactory">
        ///     A factory for creating Ice Palace key layouts.
        /// </param>
        /// <param name="mmFactory">
        ///     A factory for creating Misery Mire key layouts.
        /// </param>
        /// <param name="trFactory">
        ///     A factory for creating Turtle Rock key layouts.
        /// </param>
        /// <param name="gtFactory">
        ///     A factory for creating Ganon's Tower key layouts.
        /// </param>
        public KeyLayoutFactory(
            IHCKeyLayoutFactory hcFactory, IATKeyLayoutFactory atFactory, IEPKeyLayoutFactory epFactory,
            IDPKeyLayoutFactory dpFactory, IToHKeyLayoutFactory tohFactory, IPoDKeyLayoutFactory podFactory,
            ISPKeyLayoutFactory spFactory, ISWKeyLayoutFactory swFactory, ITTKeyLayoutFactory ttFactory,
            IIPKeyLayoutFactory ipFactory, IMMKeyLayoutFactory mmFactory, ITRKeyLayoutFactory trFactory,
            IGTKeyLayoutFactory gtFactory)
        {
            _hcFactory = hcFactory;
            _atFactory = atFactory;
            _epFactory = epFactory;
            _dpFactory = dpFactory;
            _tohFactory = tohFactory;
            _podFactory = podFactory;
            _spFactory = spFactory;
            _swFactory = swFactory;
            _ttFactory = ttFactory;
            _ipFactory = ipFactory;
            _mmFactory = mmFactory;
            _trFactory = trFactory;
            _gtFactory = gtFactory;
        }

        public IList<IKeyLayout> GetDungeonKeyLayouts(IDungeon dungeon)
        {
            return dungeon.ID switch
            {
                DungeonID.HyruleCastle => _hcFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.AgahnimTower => _atFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.EasternPalace => _epFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.DesertPalace => _dpFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.TowerOfHera => _tohFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.PalaceOfDarkness => _podFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.SwampPalace => _spFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.SkullWoods => _swFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.ThievesTown => _ttFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.IcePalace => _ipFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.MiseryMire => _mmFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.TurtleRock => _trFactory.GetDungeonKeyLayouts(dungeon),
                DungeonID.GanonsTower => _gtFactory.GetDungeonKeyLayouts(dungeon),
                _ => throw new ArgumentOutOfRangeException(nameof(dungeon))
            };
        }
    }
}
