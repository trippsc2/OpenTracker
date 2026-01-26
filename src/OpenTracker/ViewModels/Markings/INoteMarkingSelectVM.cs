using System.Collections.Generic;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.ViewModels.Markings;

public interface INoteMarkingSelectVM
{
    bool PopupOpen { get; set; }

    delegate INoteMarkingSelectVM Factory(
        IMarking marking, List<IMarkingSelectItemVMBase> buttons, ILocation location);
}