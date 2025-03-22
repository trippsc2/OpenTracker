using OpenTracker.Models.Markings;
using OpenTracker.Models.Sections;

namespace OpenTracker.Models.UndoRedo.Sections
{
    /// <summary>
    /// This class contains the <see cref="IUndoable"/> action to collect a <see cref="ISection"/>.
    /// </summary>
    public class CollectSection : ICollectSection
    {
        private readonly ISection _section;
        private readonly bool _force;
        private MarkType? _previousMarking;
        private bool _previousUserManipulated;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        ///     The <see cref="ISection"/>.
        /// </param>
        /// <param name="force">
        ///     A <see cref="bool"/> representing whether the accessibility logic should be obeyed.
        /// </param>
        public CollectSection(ISection section, bool force)
        {
            _section = section;
            _force = force;
        }

        public bool CanExecute()
        {
            return _section.CanBeCleared(_force);
        }

        public void ExecuteDo()
        {
            _previousMarking = _section.Marking?.Mark;

            _previousUserManipulated = _section.UserManipulated;
            _section.UserManipulated = true;
            _section.Available--;
        }

        public void ExecuteUndo()
        {
            _section.Available++;

            if (_previousMarking is not null && _section.Marking is not null)
            {
                _section.Marking.Mark = _previousMarking.Value;
            }

            _section.UserManipulated = _previousUserManipulated;
        }
    }
}
