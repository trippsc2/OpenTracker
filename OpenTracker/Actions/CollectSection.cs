using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;

namespace OpenTracker.Actions
{
    public class CollectSection : IUndoable
    {
        private readonly Game _game;
        private readonly ISection _section;
        private MarkingType? _previousMarking;
        private Item _markedItem;

        public CollectSection(Game game, ISection section)
        {
            _game = game;
            _section = section;
        }

        public void Execute()
        {
            switch (_section)
            {
                case BossSection bossSection:
                    bossSection.Available = false;
                    break;
                case EntranceSection entranceSection:
                    entranceSection.Available = false;
                    break;
                case ItemSection itemSection:

                    itemSection.Available--;

                    if (itemSection.Available == 0 && itemSection.Marking != null)
                    {
                        _previousMarking = itemSection.Marking;
                        itemSection.Marking = null;
                        Item item = _game.Items[Enum.Parse<ItemType>(_previousMarking.Value.ToString())];

                        if (item.Current < item.Maximum)
                        {
                            _markedItem = item;
                            _markedItem.Change(1);
                        }
                    }

                    break;
            }
        }

        public void Undo()
        {
            switch (_section)
            {
                case BossSection bossSection:
                    bossSection.Available = true;
                    break;
                case EntranceSection entranceSection:
                    entranceSection.Available = true;
                    break;
                case ItemSection itemSection:

                    itemSection.Available++;

                    if (_previousMarking != null)
                        itemSection.Marking = _previousMarking;

                    if (_markedItem != null)
                        _markedItem.Change(-1);

                    break;
            }
        }
    }
}
