using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This is the class for an undoable action to clear a location.
    /// </summary>
    public class ClearLocation : IUndoable
    {
        private readonly ILocation _location;
        private readonly bool _force;
        private readonly List<int?> _previousLocationCounts = new List<int?>();
        private readonly List<MarkingType?> _previousMarkings = new List<MarkingType?>();
        private readonly List<bool?> _previousUserManipulated = new List<bool?>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to be cleared.
        /// </param>
        /// <param name="force">
        /// A boolean representing whether to override logic and clear the location.
        /// </param>
        public ClearLocation(ILocation location, bool force = false)
        {
            _location = location ?? throw new ArgumentNullException(nameof(location));
            _force = force;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return _location.CanBeCleared(_force);
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void Execute()
        {
            _previousLocationCounts.Clear();
            _previousMarkings.Clear();
            _previousUserManipulated.Clear();

            foreach (ISection section in _location.Sections)
            {
                if (section.CanBeCleared(_force))
                {
                    if (section is IMarkableSection markableSection)
                    {
                        _previousMarkings.Add(markableSection.Marking.Value);
                    }
                    else
                    {
                        _previousMarkings.Add(null);
                    }

                    _previousLocationCounts.Add(section.Available);
                    _previousUserManipulated.Add(section.UserManipulated);
                    section.Clear(_force);
                    section.UserManipulated = true;
                }
                else
                {
                    _previousLocationCounts.Add(null);
                    _previousMarkings.Add(null);
                    _previousUserManipulated.Add(null);
                }
            }
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void Undo()
        {
            for (int i = 0; i < _previousLocationCounts.Count; i++)
            {
                if (_previousLocationCounts[i] != null)
                {
                    _location.Sections[i].Available = _previousLocationCounts[i].Value;
                }

                if (_previousMarkings[i] != null)
                {
                    (_location.Sections[i] as IMarkableSection).Marking.Value = _previousMarkings[i];
                }

                if (_previousUserManipulated[i] != null)
                {
                    _location.Sections[i].UserManipulated = _previousUserManipulated[i].Value;
                }
            }
        }
    }
}
