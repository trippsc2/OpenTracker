using System;

namespace OpenTracker.Models.KeyLayouts
{
    [Flags]
    public enum ValidationStatus
    {
        Invalid = 0,
        ValidWithSeqenceBreak = 1,
        ValidWithoutSequenceBreak = 2
    }
}
