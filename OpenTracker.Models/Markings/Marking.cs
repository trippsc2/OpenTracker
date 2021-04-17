using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Markings;
using ReactiveUI;

namespace OpenTracker.Models.Markings
{
    /// <summary>
    ///     This class contains marking data.
    /// </summary>
    public class Marking : ReactiveObject, IMarking
    {
        private readonly IChangeMarking.Factory _changeMarkingFactory;
        
        private MarkType _mark;
        public MarkType Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="changeMarkingFactory">
        ///     An Autofac factory for creating undoable actions to change the marking.
        /// </param>
        public Marking(IChangeMarking.Factory changeMarkingFactory)
        {
            _changeMarkingFactory = changeMarkingFactory;
        }

        public IUndoable CreateChangeMarkingAction(MarkType newMarking)
        {
            return _changeMarkingFactory(this, newMarking);
        }
    }
}
