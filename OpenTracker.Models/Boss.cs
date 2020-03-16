using OpenTracker.Models.Enums;

namespace OpenTracker.Models
{
    public class Boss
    {
        private readonly Game _game;

        public BossType Type { get; }

        public Boss(Game game, BossType type)
        {
            _game = game;
            Type = type;
        }
    }
}
