using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Items.Adapters;
using ReactiveUI;

namespace OpenTracker.ViewModels.Items
{
    public class ItemVM : ViewModelBase, IItemVM
    {
        private readonly IItemAdapter _item;
        private readonly IRequirement _requirement;

        public bool Visible => _requirement.Met;
        public string ImageSource => _item.ImageSource;
        public bool LabelVisible => !(_item.Label is null);
        public string? Label => _item.Label;
        public string LabelColor => _item.LabelColor;

        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public ItemVM(IItemAdapter item, IRequirement requirement)
        {
            _item = item;
            _requirement = requirement;
            
            HandleClick = _item.HandleClick;

            _item.PropertyChanged += OnAdapterChanged;
        }

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
            }
        }
    }
}