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
        private readonly List<Item> _markedItems;

        public ClearLocation(Game game, Location location)
        {
            _game = game;
            _location = location;
            _previousLocationCounts = new List<int?>();
            _previousMarkings = new List<MarkingType?>();
            _markedItems = new List<Item>();
        }

        public void Execute()
        {
            _previousLocationCounts.Clear();
            _previousMarkings.Clear();
            _markedItems.Clear();

            foreach (ISection section in _location.Sections)
            {
                if (section.IsAvailable() &&
                    (section.Accessibility >= AccessibilityLevel.Inspect ||
                    section is EntranceSection))
                {
                    switch (section)
                    {
                        case BossSection bossSection:
                            _previousLocationCounts.Add(1);
                            _previousMarkings.Add(null);
                            _markedItems.Add(null);
                            bossSection.Available = false;
                            break;
                        case EntranceSection entranceSection:
                            _previousLocationCounts.Add(1);
                            _previousMarkings.Add(null);
                            _markedItems.Add(null);
                            entranceSection.Available = false;
                            break;
                        case ItemSection itemSection:

                            _previousLocationCounts.Add(itemSection.Available);
                            _previousMarkings.Add(itemSection.Marking);

                            if (itemSection.Marking != null)
                            {
                                Item item = _game.Items[Enum.Parse<ItemType>(itemSection.Marking.Value.ToString())];
                                itemSection.Marking = null;

                                if (item.Current < item.Maximum)
                                {
                                    _markedItems.Add(item);
                                    item.Change(1);
                                }
                                else
                                    _markedItems.Add(null);
                            }
                            else
                                _markedItems.Add(null);

                            itemSection.Available = 0;

                            break;
                    }
                }
                else
                {
                    _previousLocationCounts.Add(null);
                    _previousMarkings.Add(null);
                    _markedItems.Add(null);
                }
            }
        }

        public void Undo()
        {
            for (int i = 0; i < _previousLocationCounts.Count; i++)
            {
                if (_previousLocationCounts[i] != null)
                {
                    switch (_location.Sections[i])
                    {
                        case BossSection bossSection:
                            bossSection.Available = true;
                            break;
                        case EntranceSection entranceSection:
                            entranceSection.Available = true;
                            break;
                        case ItemSection itemSection:
                            itemSection.Available = _previousLocationCounts[i].Value;
                            break;
                    }
                }

                if (_previousMarkings[i] != null)
                    _location.Sections[i].Marking = _previousMarkings[i];

                if (_markedItems[i] != null)
                    _markedItems[i].Change(-1);
            }
        }
    }
}
