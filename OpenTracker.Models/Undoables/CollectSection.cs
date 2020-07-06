using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using OpenTracker.Models.Sections;
using System;

namespace OpenTracker.Models.Undoables
{
    /// <summary>
    /// This is the class for an undoable action to collect an item/entrance
    /// from a location section.
    /// </summary>
    public class CollectSection : IUndoable
    {
        private readonly ISection _section;
        private MarkingType? _previousMarking;
        private bool _previousUserManipulated;
        private IItem _markedItem;
        private IItem _prize;
        private int _previousPrizeCurrent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="section">
        /// The section to be collected.
        /// </param>
        public CollectSection(ISection section)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
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
                    var item = ItemDictionary.Instance[itemType];

                    if (item.Current < item.Maximum)
                        _markedItem = item;
                }
            }

            if (_section is BossSection bossSection)
            {
                _prize = bossSection.Prize;

                if (_prize != null)
                    _previousPrizeCurrent = _prize.Current;
            }

            _section.Available--;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            _section.Available++;
            _section.Marking = _previousMarking;
            _section.UserManipulated = _previousUserManipulated;

            if (_markedItem != null)
                _markedItem.Change(-1);

            if (_prize != null && _prize.Current != _previousPrizeCurrent)
                _prize.SetCurrent(_previousPrizeCurrent);
        }
    }
}
