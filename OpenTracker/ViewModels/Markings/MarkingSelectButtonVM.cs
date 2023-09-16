using OpenTracker.Autofac;
using OpenTracker.Models.Markings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Markings.Images;

namespace OpenTracker.ViewModels.Markings;

/// <summary>
/// This class contains the marking select button control ViewModel data.
/// </summary>
[DependencyInjection]
public sealed class MarkingSelectButtonVM : ViewModel, IMarkingSelectItemVMBase
{
    public MarkType? Marking { get; }
    public IMarkingImageVMBase? Image { get; }

    public delegate MarkingSelectButtonVM Factory(MarkType? marking);

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="markingImages">
    /// The marking image control dictionary.
    /// </param>
    /// <param name="marking">
    /// The marking to be represented by this button.
    /// </param>
    public MarkingSelectButtonVM(IMarkingImageDictionary markingImages, MarkType? marking)
    {
        Marking = marking;

        if (!(Marking is null))
        {
            Image = markingImages[Marking.Value];
        }
    }
}