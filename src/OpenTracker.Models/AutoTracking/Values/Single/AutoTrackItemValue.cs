using System.Reactive.Linq;
using OpenTracker.Models.Items;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.Models.AutoTracking.Values.Single;

/// <summary>
/// This class represents an auto-tracking result value from an item count.
/// </summary>
public sealed class AutoTrackItemValue : ReactiveObject, IAutoTrackValue
{
    private IItem Item { get; }

    [ObservableAsProperty]
    public int? CurrentValue { get; }
        
    /// <summary>
    /// Initializes a new <see cref="AutoTrackItemValue"/> object with the specified item.
    /// </summary>
    /// <param name="item">
    ///     An <see cref="IItem"/> representing the item to monitor.
    /// </param>
    public AutoTrackItemValue(IItem item)
    {
        Item = item;

        this.WhenAnyValue(x => x.Item.Current)
            .Select(x => (int?)x)
            .ToPropertyEx(this, x => x.CurrentValue);
    }
}