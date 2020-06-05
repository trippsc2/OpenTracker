using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using OpenTracker.Models.Sections;
using System;

namespace OpenTracker.Models.Actions
{
    public class CollectSection : IUndoable
    {
        private readonly Game _game;
        private readonly ISection _section;
        private MarkingType? _previousMarking;
        private bool _previousUserManipulated;
        private Item _markedItem;

        public CollectSection(Game game, ISection section)
        {
            _game = game;
            _section = section;
        }

        public void Execute()
        {
            _previousMarking = _section.Marking;
            _previousUserManipulated = _section.UserManipulated;

            _section.UserManipulated = true;

            if (_section is ItemSection && _section.Available == 1 &&
                _section.Marking.HasValue)
            {
                if (Enum.TryParse(_section.Marking.ToString(), out ItemType itemType))
                {
                    Item item = _game.Items[itemType];

                    if (item.Current < item.Maximum)
                        _markedItem = item;
                }
            }

            _section.Available--;
        }

        public void Undo()
        {
            _section.Available++;
            _section.Marking = _previousMarking;
            _section.UserManipulated = _previousUserManipulated;

            if (_markedItem != null)
                _markedItem.Change(-1);
        }
    }
}
