using System.ComponentModel;
using System.Reactive;
using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Items;
using ReactiveUI;

namespace OpenTracker.ViewModels.Dungeons;

/// <summary>
/// This class contains the small item control ViewModel data.
/// </summary>
public class DungeonItemVM : ViewModelBase, IDungeonItemVM
{
    private readonly IRequirement? _requirement;

    public bool Visible => _requirement is null || _requirement.Met;
    public IItemVM? Item { get; }
        
    public ReactiveCommand<PointerReleasedEventArgs, Unit>? HandleClick { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="requirement">
    /// The visibility requirement for the control.
    /// </param>
    /// <param name="item">
    /// The item control.
    /// </param>
    public DungeonItemVM(IRequirement? requirement, IItemVM? item)
    {
        _requirement = requirement;
            
        Item = item;

        if (_requirement is not null)
        {
            _requirement.PropertyChanged += OnRequirementChanged;
        }

        if (Item is null)
        {
            return;
        }
            
        HandleClick = Item.HandleClick;
    }

    private async void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IRequirement.Met))
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
        }
    }
}