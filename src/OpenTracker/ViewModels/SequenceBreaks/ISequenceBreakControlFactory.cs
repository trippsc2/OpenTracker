using System.Collections.Generic;

namespace OpenTracker.ViewModels.SequenceBreaks
{
    public interface ISequenceBreakControlFactory
    {
        List<ISequenceBreakControlVM> GetBombDuplicationSequenceBreaks();
        List<ISequenceBreakControlVM> GetBombJumpsSequenceBreaks();
        List<ISequenceBreakControlVM> GetDarkRoomsSequenceBreaks();
        List<ISequenceBreakControlVM> GetFakeFlippersWaterWalkSequenceBreaks();
        List<ISequenceBreakControlVM> GetOtherSequenceBreaks();
        List<ISequenceBreakControlVM> GetSuperBunnySequenceBreaks();
    }
}