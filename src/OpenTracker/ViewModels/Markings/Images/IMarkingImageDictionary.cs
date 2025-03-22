using System.Collections.Generic;
using OpenTracker.Models.Markings;

namespace OpenTracker.ViewModels.Markings.Images
{
    public interface IMarkingImageDictionary : IDictionary<MarkType, IMarkingImageVMBase>
    {
    }
}
