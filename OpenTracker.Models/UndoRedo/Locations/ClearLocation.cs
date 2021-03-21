using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Locations
{
    /// <summary>
    /// This class contains undoable action data to clear a location.
    /// </summary>
    public class ClearLocation : IClearLocation
    {
        private readonly ILocation _location;
        private readonly bool _force;
        private readonly List<int?> _previousLocationCounts = new List<int?>();
        private readonly List<MarkType?> _previousMarkings = new List<MarkType?>();
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
            _location = location;
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
        public void ExecuteDo()
        {
            _previousLocationCounts.Clear();
            _previousMarkings.Clear();
            _previousUserManipulated.Clear();

            foreach (ISection section in _location.Sections)
            {
                if (section.CanBeCleared(_force))
                {
                    _previousMarkings.Add(section is IMarkableSection markableSection ?
                        (MarkType?)markableSection.Marking.Mark : null);

                    _previousLocationCounts.Add(section.Available);
                    _previousUserManipulated.Add(section.UserManipulated);
                    section.Clear(_force);
                    section.UserManipulated = true;
                    continue;
                }

                _previousLocationCounts.Add(null);
                _previousMarkings.Add(null);
                _previousUserManipulated.Add(null);
            }
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            for (var i = 0; i < _previousLocationCounts.Count; i++)
            {
                if (_previousLocationCounts[i].HasValue)
                {
                    _location.Sections[i].Available = _previousLocationCounts[i]!.Value;
                }

                if (_previousMarkings[i].HasValue)
                {
                    if (_location.Sections[i] is IMarkableSection markableSection)
                    {
                        markableSection.Marking.Mark = _previousMarkings[i]!.Value;
                    }
                }

                if (_previousUserManipulated[i].HasValue)
                {
                    _location.Sections[i].UserManipulated = _previousUserManipulated[i]!.Value;
                }
            }
        }
    }
}
