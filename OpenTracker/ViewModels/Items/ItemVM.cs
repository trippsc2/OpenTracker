using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using OpenTracker.ViewModels.Items.Adapters;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items
{
    /// <summary>
    /// This class contains the item control ViewModel data.
    /// </summary>
    public class ItemVM : ViewModelBase, IItemVM
    {
        private readonly IItemAdapter _item;
        private readonly IRequirement _requirement;

        public bool Visible => _requirement.Met;
        public string ImageSource => _item.ImageSource;
        public bool LabelVisible => !(_item.Label is null);
        public string? Label => _item.Label;
        public string LabelColor => _item.LabelColor;

        public IBossSelectPopupVM? BossSelect => _item.BossSelect;

        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item to be represented.
        /// </param>
        /// <param name="requirement">
        /// The visibility requirement for the item.
        /// </param>
        public ItemVM(IItemAdapter item, IRequirement requirement)
        {
            _item = item;
            _requirement = requirement;
            
            HandleClick = _item.HandleClick;

            _item.PropertyChanged += OnAdapterChanged;
            _requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItemAdapter interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnAdapterChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IItemAdapter.ImageSource):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
                    break;
                case nameof(IItemAdapter.Label):
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        this.RaisePropertyChanged(nameof(LabelVisible));
                        this.RaisePropertyChanged(nameof(Label));
                    });
                    break;
                case nameof(IItemAdapter.LabelColor):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(LabelColor)));
                    break;
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Met))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
            }
        }
    }
}