using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenTracker.Actions
{
    public class ClearLocation : IUndoable
    {
        private readonly Game _game;
        private readonly Location _location;
        private readonly List<int?> _previousLocationCounts;
        private readonly List<MarkingType?> _previousMarkings;
        private readonly List<bool?> _previousUserManipulated;
        private readonly List<Item> _markedItems;

        public ClearLocation(Game game, Location location)
        {
            _game = game;
            _location = location;
            _previousLocationCounts = new List<int?>();
            _previousMarkings = new List<MarkingType?>();
            _previousUserManipulated = new List<bool?>();
            _markedItems = new List<Item>();
        }

        public void Execute()
        {
            _previousLocationCounts.Clear();
            _previousMarkings.Clear();
            _previousUserManipulated.Clear();

            foreach (ISection section in _location.Sections)
            {
                if (section.IsAvailable() &&
                    (section.Accessibility >= AccessibilityLevel.Inspect ||
                    section is EntranceSection))
                {
                    _previousLocationCounts.Add(section.Available);
                    _previousMarkings.Add(section.Marking);
                    _previousUserManipulated.Add(section.UserManipulated);

                    section.UserManipulated = true;

                    if (section is ItemSection && section.Marking.HasValue)
                    {
                        if (Enum.TryParse(section.Marking.Value.ToString(), out ItemType itemType))
                        {
                            Item item = _game.Items[itemType];

                            if (item.Current < item.Maximum)
                                _markedItems.Add(item);
                            else
                                _markedItems.Add(null);
                        }
                        else
                            _markedItems.Add(null);
                    }
                    else
                        _markedItems.Add(null);

                    section.Clear();

                    if (section.IsAvailable())
                        _markedItems[_markedItems.Count - 1] = null;
                }
                else
                {
                    _previousLocationCounts.Add(null);
                    _previousMarkings.Add(null);
                    _previousUserManipulated.Add(null);
                    _markedItems.Add(null);
                }
            }
        }

        public void Undo()
        {
            for (int i = 0; i < _previousLocationCounts.Count; i++)
            {
                if (_previousLocationCounts[i] != null)
                    _location.Sections[i].Available = _previousLocationCounts[i].Value;

                if (_previousMarkings[i] != null)
                    _location.Sections[i].Marking = _previousMarkings[i];

                if (_previousUserManipulated[i] != null)
                    _location.Sections[i].UserManipulated = _previousUserManipulated[i].Value;

                if (_markedItems[i] != null)
                    _markedItems[i].Change(-1);
            }
        }
    }
}
