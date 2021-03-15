using System.ComponentModel;
using ReactiveUI;

namespace OpenTracker.Models.Markings
{
    /// <summary>
    /// This class contains marking data.
    /// </summary>
    public class Marking : ReactiveObject, IMarking
    {
        private MarkType _mark;
        public MarkType Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
    }
}
