using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing a prize section.
    /// </summary>
    public class PrizeSectionIconVM : ViewModelBase, ISectionIconVMBase, IClickHandler
    {
        private readonly IPrizeDictionary _prizes;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IPrizeSection _section;

        public string ImageSource
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/Prizes/");

                if (_section.PrizePlacement.Prize == null)
                {
                    sb.Append("unknown");
                }
                else
                {
                    sb.Append(
                        _prizes.FirstOrDefault(
                            x => x.Value == _section.PrizePlacement.Prize).Key.ToString()
                                .ToLowerInvariant());
                }

                sb.Append(_section.IsAvailable() ? "0.png" : "1.png");

                return sb.ToString();
            }
        }

        public delegate PrizeSectionIconVM Factory(IPrizeSection section);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The prize section to be presented.
        /// </param>
        public PrizeSectionIconVM(
            IPrizeDictionary prizes, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, IPrizeSection section)
        {
            _prizes = prizes;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;

            _section.PropertyChanged += OnSectionChanged;
            _section.PrizePlacement.PropertyChanged += OnPrizeChanged; 
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                UpdateImage();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPrizeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize))
            {
                UpdateImage();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Handles left clicks and toggles the prize section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetTogglePrize(_section, force));
        }

        /// <summary>
        /// Handles right clicks and changes the prize.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetChangePrize(_section.PrizePlacement));
        }
    }
}
